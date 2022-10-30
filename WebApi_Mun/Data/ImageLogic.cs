using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using WebApi_Mun.Models;

namespace WebApi_Mun.Data
{
    public class ImageLogic
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MundoConnection"].ConnectionString;

 

        /// <summary>
        /// Graba la imagen
        /// </summary>
        /// <param name="data">Datos de la imagen</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        public int Save( ProductImageDto data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                if (data.ImageId.HasValue && data.ImageId != 0)
                {
                    store = "Image_Update";

                }
                if (data.ProductId.HasValue && data.ProductId != 0 && )
                {
                    store = "Image_Add";

                }
                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("Image_Update"))
                        objCmd.Parameters.Add("@ImageId", SqlDbType.Int).Value = data.ImageId;

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