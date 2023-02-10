﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WebApi_Mun.Models;

namespace WebApi_Mun.Data
{
    public class ProductLogic
    {
        internal static string connectionString = ConfigurationManager.ConnectionStrings["MundoConnection"].ConnectionString;

        #region consulta

        /// <summary>
        /// Campos de la consulta
        /// </summary>
        public enum OrderFields
        {
            /// <summary>
            /// Precio
            /// </summary>
            Price = 0,
            /// <summary>
            /// Nombre del producto
            /// </summary>
            Name = 1

        }

        /// <summary>
        /// Clase para definir los valores de los filtros
        /// </summary>
        //[Serializable]
        public class Filter
        {


            /// <summary>
            /// Filtro para el campo "Estado"
            /// </summary>
            public bool? State { get; set; }
            /// <summary>
            /// Filtro para el campo "Stock"
            /// </summary>
            public bool? Stock { get; set; }

            /// <summary>
            /// Filtro para el campo "Destacados"
            /// </summary>
            public bool? Featured { get; set; }

            /// <summary>
            /// Filtro para el campo "Descuentos"
            /// </summary>
            public bool? Discount { get; set; }

            /// <summary>
            /// Búsqueda por texto libre
            /// </summary>
            public int? CategoryId { get; set; }

            /// <summary>
            /// Búsqueda por texto libre
            /// </summary>
            public int? MarcaId { get; set; }


        }

        #endregion

        #region Sql

        private const string SELECT_ALL =
        ";SELECT A.ProductId, A.[Name] as ProductName,  A.[Description] " +
            " ,A.CategoryId_FK as CategoryId, o.[Name] as CategoryName, A.MarcaId_FK as MarcaId, mar.[Name] as MarcaName " +
            " ,A.Price, A.DiscountId_FK as DiscountId, dis.[Amount] DiscountAmount, A.Stock, A.Featured ," +
            " (select top 1 Images.Name " +
            " from ProductImage" +
            " inner join Images on Images.ImageId=ProductImage.ImageId_FK " +
            " where ProductId_FK = A.ProductId" +
            " ) as ImageName " +
            " , A.CreatedBy_FK as UserId" +
            " FROM Products AS A " +
            " INNER JOIN dbo.Categories O ON O.CategoryId = A.CategoryId_FK " +
            " INNER JOIN [dbo].[Marcas] mar on A.MarcaId_FK= mar.MarcaId" +
            " left JOIN [dbo].[Discounts] dis on A.DiscountId_FK= dis.DiscountId" +
            " {2} " +
            " ORDER BY {0} {1} ";

        private const string OFFSET = " OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY ";                 //{1} Desde - {2} Hasta

        private const string SELECTCOUNT = "SELECT COUNT(1) AS rows FROM [Products] AS A";
        #endregion

        /// <summary>
        /// Obtiene la lista de registros paginada, ordenada y filtrada
        /// </summary>
        /// <param name="orderField">Campo de orden</param>
        /// <param name="orderAscendant">Orden ascendente</param>
        /// <param name="from">Registro desde el cual traer los datos (en base cero)</param>
        /// <param name="length">Cantidad de registros a obtener</param>
        /// <param name="filter">Filtros a utilizar</param>
        /// <param name="recordCount">Cantidad de registros encontrados con los filtros establecidos</param>
        /// <returns>Registros obtenidos con los filtros y orden seleccionados</returns>
        public DataTableModel List(OrderFields? orderField, bool? orderAscendant, Filter filter, int? from, int? length, out int recordCount)
        {
            length = null; from = null;
            if (from.HasValue != length.HasValue)
                throw new ArgumentOutOfRangeException(nameof(from));

            if (length.HasValue && length > 100)
                throw new ArgumentOutOfRangeException(nameof(length));

            string strOrderField;
            if (orderField.HasValue)
                strOrderField = orderField.ToString();
            else
                strOrderField = OrderFields.Name.ToString();

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objSqlCmd = new SqlCommand("", connection);

                string strFilter = string.Empty;
                strFilter = " WHERE [A].State=1 ";

                if (filter != null)
                {
                    if (filter.CategoryId.HasValue && filter.CategoryId != null)
                    {
                        strFilter += " AND [A].CategoryId_FK=@Id_Category";
                        objSqlCmd.Parameters.Add("@Id_Category", SqlDbType.Int).Value = filter.CategoryId.Value;

                    }
                    if (filter.MarcaId.HasValue && filter.MarcaId != null)
                    {
                        strFilter += " AND [A].MarcaId_FK=@Id_Marca";
                        objSqlCmd.Parameters.Add("@Id_Marca", SqlDbType.Int).Value = filter.MarcaId.Value;

                    }
                    if (filter.Stock.HasValue && filter.Stock==true)
                    {
                        strFilter += " AND [A].Stock = @Stock";
                        objSqlCmd.Parameters.Add("@Stock", SqlDbType.TinyInt).Value = filter.Stock.Value;
                    }
                    if (filter.Featured.HasValue && filter.Featured==true)
                    {
                        strFilter += " AND [A].Featured = @Featured";
                        objSqlCmd.Parameters.Add("@Featured", SqlDbType.TinyInt).Value = filter.Featured.Value;
                    }
                    if (filter.Discount.HasValue && filter.Discount == true)
                    {
                        strFilter += " AND [A].DiscountId_FK is not null";
                    }
                    //if (!string.IsNullOrWhiteSpace(filter.FreeText))
                    //{
                    //    strFilter += " AND CONTAINS(R.*, @FreeText )";
                    //    objSqlCmd.Parameters.Add("@FreeText", SqlDbType.NVarChar, 1000).Value = filter.FreeText;
                    //}

                }

                string strWithParams = string.Format(SELECT_ALL, strOrderField, !orderAscendant.HasValue || orderAscendant.Value ? "ASC" : "DESC", strFilter);
                objSqlCmd.CommandType = CommandType.Text;

                connection.Open();

                if (from.HasValue)
                {
                    strWithParams += string.Format(OFFSET, from, length);
                }

                objSqlCmd.CommandText = SELECTCOUNT + strFilter + strWithParams;
#if DEBUG
                System.Diagnostics.Trace.WriteLine(objSqlCmd.CommandText);
#endif

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = objSqlCmd;

                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                var tab = dataset.Tables[1].Rows;
                recordCount = (int)dataset.Tables[0].Rows[0][0];
             
                //lista para devolver los datos mapeados ProductModelList
                List<ProductModelDto> productList = new List<ProductModelDto>();
                foreach (DataRow row in tab)
                {
                    var price = Convert.ToDouble(row["Price"]);
                    ProductModelDto product = new ProductModelDto();
                    product.ProductId = Convert.ToInt32(row["ProductId"]);
                    product.Name = Convert.ToString(row["ProductName"]);
                    product.Description = Convert.ToString(row["Description"]);
                    product.CategoryName = Convert.ToString(row["CategoryName"]);
                    product.MarcaName = Convert.ToString(row["MarcaName"]);
                    if (!row.IsNull("DiscountAmount"))
                    {
                        var discount = (Convert.ToInt32(row["DiscountAmount"]));
                        product.DiscountAmount = discount;
                        product.PriceWithDiscount = price - (price * discount / 100);
                    }
                        
                    if (!row.IsNull("Stock"))
                        product.Stock = Convert.ToBoolean(row["Stock"]);
                    product.Featured = Convert.ToBoolean(row["Featured"]);
                    product.Price = Convert.ToDouble(row["Price"]);
                    product.ImageName = Convert.ToString(row["ImageName"]);
                    productList.Add(product);
                }
                connection.Close();
                return (new DataTableModel()
                {
                    RecordsCount = recordCount,
                    Data = productList
                });
            }
        }

        /// <summary>
        /// Devuelve los datos de  producto
        /// </summary>
        /// <param name="userId">Identificador del producto</param>
        /// <returns>Datos de producto</returns>
        public ProductModel Get(int productId)
        {
            var items = new ProductModel();
            using (var connection = new SqlConnection(connectionString))
            {
                var objCmd = new SqlCommand("Product_Get", connection);
                objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                objCmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    using (var objDR = objCmd.ExecuteReader(CommandBehavior.SingleRow))
                    {

                        items = new ProductModel();
                        if (!objDR.Read())
                        {
                            return null;
                        }

                        items.ProductId = objDR.GetInt32(0);
                        items.Name = objDR.GetString(1);
                        items.Description = objDR.GetString(2);
                        items.CategoryId = objDR.GetInt32(3);
                        items.MarcaId = objDR.GetInt32(4);
                        if(!objDR.IsDBNull(5))
                            items.DiscountId = objDR.GetInt32(5);
                        items.Price = (double)objDR.GetDecimal(6);
                        items.State = objDR.GetByte(7) == 0 ? false : true;
                        items.Stock = objDR.GetByte(8) == 0 ? false : true;
                        items.Featured = objDR.GetByte(9) == 0 ? false : true;

                        return items;
                    }
                }
                catch (Exception e)
                {
                    throw ;
                }

            }


        }

        /// <summary>
        /// Devuelve productos destacados
        /// </summary>
        /// <returns>Datos de producto</returns>
        public ProductModelDto[]  GetProductsFeatured()
        {
            var items = new List<ProductModelDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Product_Featured", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();

                    while (objDR.Read())
                    {
                        var price = (Math.Truncate((double)objDR.GetDecimal(3) * 100) / 100);
                        

                        var c = new ProductModelDto();
                        c.ProductId = objDR.GetInt32(0);
                        c.Name = objDR.GetString(1);
                        c.Description = objDR.GetString(2);
                        c.Price = price ;
                        if (!objDR.IsDBNull(4))
                        {
                            var priceDiscount = ((double)objDR.GetDecimal(3) - ((double)objDR.GetDecimal(3) * (int)objDR.GetByte(4)) / 100);
                            priceDiscount = (Math.Truncate(priceDiscount * 100) / 100);
                            c.DiscountAmount = (int)objDR.GetByte(4);
                            c.PriceWithDiscount = priceDiscount;
                        }
                        else
                        {
                            c.PriceWithDiscount = price;
                        }
                        c.MarcaName = objDR.GetString(5);
                        c.CategoryName = objDR.GetString(6); 
                        c.ImageName = objDR.GetString(7);
                        items.Add(c);
                    }
                    return items.ToArray();

                }

            }

        }


        /// <summary>
        /// Graba el producto
        /// </summary>
        /// <param name="data">Datos del prodcuto</param>
        /// <param name="userId">Identificador del usuario que graba</param>
        /// <returns><c>true</c> Si se guardaron los datos, en caso contrario quiere decir que el nombre está repetido</returns>
        public int Save(ProductModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var store = "";
                if (data.ProductId.HasValue && data.ProductId.Value != 0)
                    store = "Product_Update";
                else
                    store = "Product_Add";
                var objCmd = new SqlCommand(store, connection);

                if (store.Equals("Product_Update"))
                    objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = data.ProductId;

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = data.CategoryId;
                objCmd.Parameters.Add("@MarcaId", SqlDbType.Int).Value = data.MarcaId;
                objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = data.DiscountId;
                objCmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = data.Name;
                objCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = data.Description;
                objCmd.Parameters.Add("@Featured", SqlDbType.TinyInt).Value = (data.Featured == true ? 1 : 0);
                objCmd.Parameters.Add("@State", SqlDbType.TinyInt).Value = 1;
                objCmd.Parameters.Add("@Stock", SqlDbType.TinyInt).Value = (data.Stock == true ? 1 : 0);
                objCmd.Parameters.Add("@Price", SqlDbType.Money).Value = data.Price;


                //var result = objCmd.ExecuteNonQuery();

               var result=0;
               try
               {
                   connection.Open();
                   using (var objDR = objCmd.ExecuteReader(CommandBehavior.SingleRow))
                   {
                       if (!objDR.Read())
                       {
                           return 0;
                       }
                   result = objDR.GetInt32(0);
                   objDR.Close();
                   connection.Close();
                       
                   return result;
                      
                   }
               }
               catch (Exception e)
               {
                  throw e;
               }
            }

        }

        public int Desactive(int itemId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objCmd;
                var store = "";
                store = "Product_Desactive";
                objCmd = new SqlCommand(store, connection);
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = itemId;

                connection.Open();
                var result = objCmd.ExecuteNonQuery();
                return result;
            }
        }



    }
}



