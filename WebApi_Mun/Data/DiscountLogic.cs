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
        public DiscountModel[] List()
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
                        c.Amount = objDR.GetByte(1);
                        c.CreatedBy= objDR.GetInt32(2);
                        c.State = objDR.GetByte(3) == 1 ? true : false;

                        items.Add(c);
                    }
                    return items.ToArray(); 

                }
                
            }
        
        }

        /// <summary>
        /// Devuelve todos las categorias d eun usuario
        /// </summary>
        /// <returns>Lista de categorias</returns>
        public DiscountModel[] ListActive()
        {
            var items = new List<DiscountModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Discount_List_Active", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();

                    while (objDR.Read())
                    {
                        var c = new DiscountModel();
                        c.DiscountId = objDR.GetInt32(0);
                        c.Amount = objDR.GetByte(1);
                        c.CreatedBy = objDR.GetInt32(2);
                        c.State = objDR.GetByte(3) == 1 ? true : false;

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
        public DiscountModel Get(int discountId)
        {
            var items = new DiscountModel();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Discount_Get", connection))
                {
                    objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = discountId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var objDR = objCmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!objDR.Read())
                        {
                            return null;
                        }

                        items.DiscountId = objDR.GetInt32(0);
                        items.Amount = objDR.GetByte(1);
                        items.CreatedBy = objDR.GetInt32(2);
                        items.State = objDR.GetByte(3) == 1 ? true : false;

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
        public int Save(DiscountModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                if (data.DiscountId.HasValue && data.DiscountId != 0 )
                {
                    store = "Discount_Update";                 
                }
                else
                    store = "Discount_Add";
                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("Discount_Update"))
                        objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = data.DiscountId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Amount", SqlDbType.TinyInt).Value = data.Amount;                 
                    objCmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = 1;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }
           
        }

        public int Desactive(int itemId)
        {

            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";

                store = "Discount_Desactive";
                objCmd = new SqlCommand(store, connection);

                using (objCmd = new SqlCommand(store, connection))
                {
                    objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = itemId;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }
        }

    }
}