using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using WebApi_Mun.Models;

namespace WebApi_Mun.Data
{
    public class CategoryLogic
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MundoConnection"].ConnectionString;

        /// <summary>
        /// Devuelve todos las categorias d eun usuario
        /// </summary>
        /// <returns>Lista de categorias</returns>
        public  CategoryModel[] List()
        {
            var items = new List<CategoryModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using(var objCmd = new SqlCommand("Category_List", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();

                    while (objDR.Read())
                    {
                        var c = new CategoryModel();
                        c.CategoryId = objDR.GetInt32(0);
                        c.Name = objDR.GetString(1);

                        items.Add(c);
                    }
                    return items.ToArray(); 

                }
                
            }
        
        }

        /// <summary>
        /// Devuelve las categorias activas
        /// </summary>
        /// <returns>Lista de categorias activas</returns>
        public CategoryModel[] ListActive()
        {
            var items = new List<CategoryModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Category_List_Active", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();
                    while (objDR.Read())
                    {
                        var c = new CategoryModel();
                        c.CategoryId = objDR.GetInt32(0);
                        c.Name = objDR.GetString(1);

                        items.Add(c);
                    }
                    return items.ToArray();

                }

            }

        }

        /// <summary>
        /// Devuelve los datos de una categoria
        /// </summary>
        /// <param name="categoryId">Identificador del categoria</param>
        /// <returns>Datos de categoria</returns>
        public CategoryModel Get(int categoryId)
        {
            var items = new CategoryModel();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Category_Get", connection))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
                    connection.Open();
                    using (var objDR = objCmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!objDR.Read())
                        {
                            return null;
                        }

                        items.CategoryId = objDR.GetInt32(0);
                        items.Name = objDR.GetString(1);

                    }
                }
                return items;
            }

        }


        private const string SELECT_BY =
        ";Select cm.CategoryId, c.[Name] " +
        " from[dbo].[CategoryMarcas] cm " +
        " inner join Categories c on c.CategoryId=cm.CategoryId where cm.MarcaId = {0} and c.[State]=1;" ;

        private const string SELECT_ALL =
        ";Select CategoryId, [Name] " +
        " from[dbo].[Categories] c where c.[State]=1 ";

        /// <summary>
        /// Devuelve categorias segun la marca asociada
        /// </summary>
        /// <param name="marcaId">Identificador del marca</param>
        /// <returns>Lista de Catgeorias</returns>
        public List<CategoryModel> GetCategoriesByMarca(int? marcaId)
        {
            var items = new List<CategoryModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objSqlCmd = new SqlCommand("", connection);

                if (marcaId.HasValue)
                {
                    string strFilter = string.Empty;

                    strFilter += "@MarcaId";
                    objSqlCmd.Parameters.Add("@MarcaId", SqlDbType.Int).Value = marcaId;

                    string strWithParams = string.Format(SELECT_BY, strFilter);
                    objSqlCmd.CommandType = CommandType.Text;
                    connection.Open();
                    objSqlCmd.CommandText = strWithParams;
                }
                else
                {
                    objSqlCmd.CommandText = SELECT_ALL;
                } 

#if DEBUG
                System.Diagnostics.Trace.WriteLine(objSqlCmd.CommandText);
#endif

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = objSqlCmd;

                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                var tab = dataset.Tables[0].Rows;

                //lista para devolver los datos mapeados ProductModelList
                List<CategoryModel> list = new List<CategoryModel>();
                foreach (DataRow row in tab)
                {
                    CategoryModel cat = new CategoryModel();
                    cat.CategoryId = Convert.ToInt32(row["CategoryId"]);
                    cat.Name = Convert.ToString(row["Name"]);

                    list.Add(cat);
                }
                connection.Close();
                return (list);
            }

        }


        /// <summary>
        /// Graba la categoria
        /// </summary>
        /// <param name="data">Datos de la categoria</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        public int Save( CategoryModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                if (data.CategoryId.HasValue && data.CategoryId != 0)
                {
                    store = "Category_Update";
                    objCmd = new SqlCommand("Category_Update", connection);

                }
                else
                    store = "Category_Add";
                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("Category_Update"))
                        objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = data.CategoryId;

                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Name", SqlDbType.VarChar,150).Value = data.Name;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }

            }
           
        }


        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="data">Datos de la entidad</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        public int Desactive(int categoryId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objCmd;
                var store = "";
                store = "Category_Desactive";
                objCmd = new SqlCommand(store, connection);

                using (objCmd)
                {
                   objCmd.CommandType = CommandType.StoredProcedure;
                   objCmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;

                   connection.Open();
                   var result = objCmd.ExecuteNonQuery();
                   connection.Close();
                   return result;
                }
            }
        }

    }
}