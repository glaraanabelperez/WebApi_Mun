using System;
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
    public class Product
    {
        internal static string connectionString = ConfigurationManager.ConnectionStrings["MenuConnection"].ConnectionString;
      
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
            Name=1

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
            /// Filtro para el campo "Destacados"
            /// </summary>
            public bool? Featured { get; set; }


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
            " ,A.Price, A.DiscountId_FK as DiscountId, dis.[Amount] DiscountAmount, A.State, A.Featured " +
            " ,imp.ProductImageId, imp.ImageId_FK as ImageId, img.[Name] as ImageName" +
            " ,A.CreatedBy_FK as UserId" +
            " FROM Products AS A " +
            " INNER JOIN dbo.Categories O ON O.CategoryId = A.CategoryId_FK " +
            " INNER JOIN [dbo].[Marcas] mar on A.MarcaId_FK= mar.MarcaId" +
            " left JOIN [dbo].[Discounts] dis on A.DiscountId_FK= dis.DiscountId" +
            " left JOIN [dbo].[ProductImage] imp on imp.ProductId_FK= A.ProductId" +
            " left JOIN [dbo].[Images] img on img.ImageId= imp.ImageId_FK" +
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
        public static DataTable List(OrderFields? orderField, bool? orderAscendant, Filter filter, int? from, int? length, out int recordCount){

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
              
              //Agregando parametros
              string strFilter = string.Empty;
              if (filter != null)
                    {
                        if (filter.CategoryId.HasValue)
                        {
                            strFilter += " AND [A].CategoryId_FK=@Id_Category";
                            objSqlCmd.Parameters.Add("@Id_Category", SqlDbType.Int).Value = filter.CategoryId.Value;

                        }
                        if (filter.MarcaId.HasValue)
                        {
                            strFilter += " AND [A].MarcaId_FK=@Id_Marca";
                            objSqlCmd.Parameters.Add("@Id_Marca", SqlDbType.Int).Value = filter.MarcaId.Value;

                        }
                        if (filter.State.HasValue)
                        {
                            strFilter += " AND [A].State = @State";
                            objSqlCmd.Parameters.Add("@State", SqlDbType.Decimal).Value = filter.State.Value;
                        }
                        if (filter.Featured.HasValue)
                        {
                            strFilter += " AND [A].Featured = @Featured";
                            objSqlCmd.Parameters.Add("@Featured", SqlDbType.Bit).Value = filter.Featured.Value;
                        }
                        //if (!string.IsNullOrWhiteSpace(filter.FreeText))
                        //{
                        //    strFilter += " AND CONTAINS(R.*, @FreeText )";
                        //    objSqlCmd.Parameters.Add("@FreeText", SqlDbType.NVarChar, 1000).Value = filter.FreeText;
                        //}


                        if (!string.IsNullOrWhiteSpace(strFilter))
                            strFilter = " WHERE " + strFilter.Substring(5);
                    }

              string strWithParams = string.Format(SELECT_ALL, strOrderField, !orderAscendant.HasValue || orderAscendant.Value ? "ASC" : "DESC", strFilter);
              objSqlCmd.CommandType = CommandType.Text;

              connection.Open();
              
              if (from.HasValue)
                    {
                        strWithParams += string.Format(OFFSET, from, length);

                        objSqlCmd.CommandText = SELECTCOUNT + strFilter + strWithParams;
#if DEBUG
                        System.Diagnostics.Trace.WriteLine(objSqlCmd.CommandText);
#endif

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = objSqlCmd;

                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);
                        
                        recordCount = (int)dataset.Tables[0].Rows[0][0];


                    List<ProductModelDto> productList = new List<ProductModelDto>();
                    foreach (DataRow row in dataset.Tables[1].Rows)
                    {

                        //string valor1 = Convert.ToString(row["columna1"]);
                        int productId = Convert.ToInt32(row["ProductId"]);
                        if (productList.Exists(p => p.ProductId == productId)==true)
                        {
                            ProductImageModel im = new ProductImageModel();
                            im.ProductImageId = Convert.ToInt32(row["ProductImageId"]);

                            productList.Find(p => p.ProductId == productId).images.Add(im);
                        }
                        else
                        {

                            ProductImageModel im = new ProductImageModel();
                            im.ProductImageId = Convert.ToInt32(row["ProductImageId"]);

                            ProductModelDto product = new ProductModelDto();
                            product.ProductId = productId;
                            product.images.Add(im);
                            productList.Add(product);
                        }

                    }


                    //SqlDataReader reader = objSqlCmd.ExecuteReader();

                    //using (reader)
                    //{
                    //    while (reader.Read())
                    //    {
                    //        if (!productList.Exists(p => p.ProductId == (int)reader["ProductId"]))
                    //        {
                    //            ProductImageModel im = new ProductImageModel();
                    //            im.ProductImageId = (int)reader["ProductImageId"];
                    //            im.ImageId = (int)reader["ImageId"];
                    //            im.Name = (string)reader["ImageName"];
                    //            im.ProductId = (int)reader["ProductId"];

                    //            productList.Find(p => p.ProductId == (int)reader["ProductId"]).images.Add(im);
                    //        }
                    //        else
                    //        {
                                
                    //            ProductImageModel im = new ProductImageModel();
                    //                im.ProductImageId = (int)reader["ProductImageId"];
                    //                im.ImageId = (int)reader["ImageId"];
                    //                im.Name = (string)reader["ImageName"];
                    //                im.ProductId = (int)reader["ProductId"];

                    //            ProductModelDto product = new ProductModelDto();
                    //            product.ProductId = (int)reader["ProductImageId"];
                    //            product.images.Add(im);
                    //        }
                            
                 
                    //    }
                    //}


                    connection.Close();
                    return dataset.Tables[1];
                    }
              else
                    {
                        objSqlCmd.CommandText = strWithParams;
                        recordCount = -1;
#if DEBUG
                        System.Diagnostics.Trace.WriteLine(strWithParams);
#endif

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = objSqlCmd;

                        DataSet dataset = new DataSet();
                        adapter.Fill(dataset);

                        return dataset.Tables[0];
                    }


                
            }
             
        }

        /// <summary>
        /// Devuelve los datos de  producto
        /// </summary>
        /// <param name="userId">Identificador del producto</param>
        /// <returns>Datos de producto</returns>
        internal static ProductModel Get(int productId)
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
   
                            //items.ProductId = objDR.GetInt32(0);
                            //items.CategoryId = objDR.GetInt32(1);
                            //items.UserId = objDR.GetInt32(2);
                            //items.State = objDR.GetByte(3) == 0 ? false : true;
                            //items.Title = objDR.GetString(4);
                            //items.Subtitle = objDR.GetString(5);
                            //items.Description = objDR.GetString(6);
                            //items.NameImage = objDR.GetString(7);
                            //items.Price = (double)objDR.GetDecimal(9);
                            //items.Featured = objDR.GetByte(10) == 0 ? false : true;
                            //items.Promotion = objDR.GetString(11);

                        return items;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
               
            }


        }

        /// <summary>
        /// Graba el producto
        /// </summary>
        /// <param name="data">Datos del prodcuto</param>
        /// <param name="userId">Identificador del usuario que graba</param>
        /// <returns><c>true</c> Si se guardaron los datos, en caso contrario quiere decir que el nombre está repetido</returns>
        public static int Save(int? productId, ProductModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                
                var store = "";
                if (productId.HasValue && productId.Value != 0)
                    store = "Product_Update";
                else
                    store = "Product_Add";

                 using(SqlCommand objCmd = new SqlCommand(store, connection))
                {
                    //if (store.Equals("Product_Update"))
                    //    objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                    //objCmd.CommandType = CommandType.StoredProcedure;
                    //objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = data.UserId;
                    //objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = data.CategoryId;
                    //objCmd.Parameters.Add("@Title", SqlDbType.VarChar, 250).Value = data.Title;
                    //objCmd.Parameters.Add("@SubTitle", SqlDbType.VarChar, 250).Value = data.Subtitle;
                    //objCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = data.Description;
                    //objCmd.Parameters.Add("@Featured", SqlDbType.TinyInt).Value = (data.Featured == true ? 1 : 0);
                    //objCmd.Parameters.Add("@NameImage", SqlDbType.VarChar, 250).Value = data.NameImage;
                    //objCmd.Parameters.Add("@Price", SqlDbType.Money).Value = data.Price;
                    //objCmd.Parameters.Add("@Promotion", SqlDbType.VarChar, 250).Value = data.Promotion;
                    //objCmd.Parameters.Add("@State", SqlDbType.TinyInt).Value = (data.State == true ? 1 : 0);

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }
                
            }

        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <returns>Retorna 0</returns>
        public static int Delete(int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                using (SqlCommand objCmd = new SqlCommand("Product_Delete", connection))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }
            }
        }



        //private const string SELECT_ALL =
        //";SELECT A.ProductId, A.[Name],  A.[Description], " +
        //    " A.CategoryId_FK as CategoryId, o.[Name] as CategoryName, A.MarcaId_FK as MarcaId, mar.[Name] " +
        //    " A.Price, A.DiscountId_FK, dis.[Name], A.State, A.Featured, A.ImageId_1, im.[Name] as Image1 " +
        //    " A.ImageId_2, im.[Name]" +
        //    " A.NameImage, A.Price as Price ,A.Promotion, A.State" +
        //    ", U.Business_Name as Buisness,  A.Featured as Featured," +
        //    " FROM Products AS A " +
        //    " INNER JOIN dbo.[Users] U ON U.UserId = A.UserId_FK " +
        //    " INNER JOIN dbo.Categories O ON O.CategoryId = A.CategoryId_FK " +
        //    " INNER JOIN [dbo].[Marcas] mar on A.MarcaId_FK= mar.MarcaId" +
        //    " left JOIN [dbo].[Discounts] dis on A.DiscountId_FK= dis.DiscountId" +
        //    " left JOIN [dbo].[Images] im on A.ImageId= A.ImageId_1" +
        //    " and im.ImageId= A.ImageId_2 and im.ImageId= A.ImageId_3" +
        //    " {2} " +
        //    " ORDER BY {0} {1} ";

    }
}