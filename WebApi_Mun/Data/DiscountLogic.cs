using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApi_Mun.Models;

namespace WebApi_Mun.Data
{
    public class DiscountLogic : BaseLogic
    {

        /// <summary>
        /// Devuelve todos las categorias d eun usuario
        /// </summary>
        /// <returns>Lista de categorias</returns>
        public DiscountModel[] List()
        {
            var items = new List<DiscountModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using(var objCmd = new SqlCommand("paalerac_colores.[dbo].Discount_List", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();

                    while (objDR.Read())
                    {
                        var c = new DiscountModel();
                        c.DiscountId = objDR.GetInt32(0);
                        c.Amount = objDR.GetInt32(1);
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
                using (var objCmd = new SqlCommand("paalerac_colores.[dbo].Discount_List_Active", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();

                    while (objDR.Read())
                    {
                        var c = new DiscountModel();
                        c.DiscountId = objDR.GetInt32(0);
                        c.Amount = objDR.GetInt32(1);
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
                using (var objCmd = new SqlCommand("paalerac_colores.[dbo].Discount_Get", connection))
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
                        items.Amount = objDR.GetInt32(1);
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
                    store = "paalerac_colores.[dbo].Discount_Update";                 
                }
                else
                    store = "paalerac_colores.[dbo].Discount_Add";
                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("paalerac_colores.[dbo].Discount_Update"))
                        objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = data.DiscountId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Amount", SqlDbType.Int).Value = data.Amount;                 
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

                store = "paalerac_colores.[dbo].Discount_Desactive";

                using (objCmd = new SqlCommand(store, connection))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@DiscountId", SqlDbType.Int).Value = itemId;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }
        }

    }
}