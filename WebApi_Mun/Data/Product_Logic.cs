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
            " ,img.[Name] as ImageName" +
            " ,A.CreatedBy_FK as UserId" +
            " FROM Products AS A " +
            " INNER JOIN dbo.Categories O ON O.CategoryId = A.CategoryId_FK " +
            " INNER JOIN [dbo].[Marcas] mar on A.MarcaId_FK= mar.MarcaId" +
            " left JOIN [dbo].[Discounts] dis on A.DiscountId_FK= dis.DiscountId" +
            " left JOIN [dbo].[ProductImage] imp on A.ProductId= (" +
            "select top 1 ProductId_FK from [dbo].[ProductImage]" +
            ")" +
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
        public DataTableModel List(OrderFields? orderField, bool? orderAscendant, Filter filter, int? from, int? length, out int recordCount)
        {

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
                }
                else
                {
                    objSqlCmd.CommandText = strWithParams;
                    recordCount = -1;
#if DEBUG
                    System.Diagnostics.Trace.WriteLine(strWithParams);
#endif

                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = objSqlCmd;

                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                recordCount = from.HasValue ? (int)dataset.Tables[0].Rows[0][0] : -1;

                //lista para devolver los datos mapeados ProductModelList
                List<ProductModelDto> productList = new List<ProductModelDto>();


                foreach (DataRow row in dataset.Tables[1].Rows)
                {
                    
                        ProductModelDto product = new ProductModelDto();
                        product.ProductId = Convert.ToInt32(row["ProductId"]);
                        product.Name = Convert.ToString(row["ProductName"]);
                        product.Description = Convert.ToString(row["Description"]);
                        product.CategoryName = Convert.ToString(row["CategoryName"]);
                        product.MarcaName = Convert.ToString(row["MarcaName"]);
                        if(!row.IsNull("DiscountAmount"))
                            product.DiscountAmount = Convert.ToInt32(row["DiscountAmount"]);
                        product.State = Convert.ToBoolean(row["State"]);
                        product.Featured = Convert.ToBoolean(row["Featured"]);
                        product.ImageName= Convert.ToString(row["ImageName"]);
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
                        items.Featured = objDR.GetByte(8) == 0 ? false : true;

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
        public int Save(ProductModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var store = "";
                if (data.ProductId.HasValue && data.ProductId.Value != 0)
                    store = "Product_Update";
                else
                    store = "Product_Add";

                using (SqlCommand objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("Product_Update"))
                        objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = data.ProductId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = data.CategoryId;
                    objCmd.Parameters.Add("@MarcaId", SqlDbType.Int).Value = data.MarcaId;
                    objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = data.DiscountId;
                    objCmd.Parameters.Add("@Name", SqlDbType.VarChar, 250).Value = data.Name;
                    objCmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = data.Description;
                    objCmd.Parameters.Add("@Featured", SqlDbType.TinyInt).Value = (data.Featured == true ? 1 : 0);
                    objCmd.Parameters.Add("@State", SqlDbType.TinyInt).Value = (data.State == true ? 1 : 0);
                    objCmd.Parameters.Add("@Price", SqlDbType.Money).Value = data.Price;


                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }

        }

        public int Desactive(StateModel data)
        {
            string queryString = string.Format("update Products set [State]={0} where ProductId= {1}", data.State ? 1 : 0, data.ItemId);
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }


        }

    }
}