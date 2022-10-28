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
                        c.State = objDR.GetBoolean(2);

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
                        c.State = objDR.GetByte(2) == 1 ? true : false;

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
                        items.State = objDR.GetBoolean(1);

                    }
                }
                return items;
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
                    objCmd.Parameters.Add("@Name", SqlDbType.Char, 5).Value = data.Name;
                    objCmd.Parameters.Add("@State", SqlDbType.Char, 5).Value = data.State;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }
           
        }


        /// <summary>
        /// Cambia el estado de la entidad
        /// </summary>
        /// <param name="data">Datos de la entidad</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        public int Desactive(StateModel data)
        {
            string queryString = string.Format("update Categories set [State]={0} where CategoryId= {1}", data.State ? 1 : 0, data.ItemId);
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