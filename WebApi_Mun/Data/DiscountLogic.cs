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
    public class DiscountLogic
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MundoConnection"].ConnectionString;

        /// <summary>
        /// Devuelve todos las categorias d eun usuario
        /// </summary>
        /// <returns>Lista de categorias</returns>
        internal static DiscountModel[] List(int userId)
        {
            var items = new List<DiscountModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using(var objCmd = new SqlCommand("Discount_List", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();

                    while (objDR.Read())
                    {
                        var c = new DiscountModel();
                        c.DiscountId = objDR.GetInt32(0);
                        c.Name = objDR.GetString(1);
                        c.State = objDR.GetBoolean(2);

                        items.Add(c);
                    }
                    return items.ToArray(); 

                }
                
            }
        
        }

        /// <summary>
        /// Devuelve los datos de una categoria
        /// </summary>
        /// <param name="userId">Identificador del categoria</param>
        /// <returns>Datos de categoria</returns>
        internal static DiscountModel Get(int userId)
        {
            var items = new DiscountModel();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Discount_Get", connection))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var objDR = objCmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!objDR.Read())
                        {
                            return null;
                        }

                        items.DiscountId = objDR.GetInt32(0);
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
        internal static int Save(int? categoryId, DiscountModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                if (categoryId.HasValue && categoryId != 0)
                {
                    store = "Discount_Update";
                    objCmd = new SqlCommand("Discount_Update", connection);

                }
                else
                    store = "Discount_Add";
                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("Discount_Update"))
                        objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = categoryId;

                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Name", SqlDbType.Char, 5).Value = data.Name;
                    objCmd.Parameters.Add("@State", SqlDbType.Char, 5).Value = data.State;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }
           
        }

      
    }
}