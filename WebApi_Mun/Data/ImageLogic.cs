using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using WebApi_Mun.Models;

namespace WebApi_Mun.Data
{
    public class ImageLogic
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MundoConnection"].ConnectionString;


        /// <summary>
        /// Devuelve todos las imagenes segun el producto
        /// </summary>
        /// <returns>Lista de marcas</returns>
        public ProductImageDto[] ListByProduct(int productId)
        {
            var items = new List<ProductImageDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Image_GetByProduct", connection))
                {
                    connection.Open();
                    objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();
                    while (objDR.Read())
                    {
                        var c = new ProductImageDto();
                        c.ProductImageId = objDR.GetInt32(0);
                        c.ProductId = objDR.GetInt32(1);
                        c.ImageId = objDR.GetInt32(2);
                        c.Name = objDR.GetString(3);
                        items.Add(c);
                    }
                    return items.ToArray();

                }

            }

        }



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
                if (data.ImageId != 0)
                {
                    store = "Image_Update";

                }
                else if (data.ProductId != 0 && data.Name.Length>0)
                {
                    store = "Image_Add";

                }
                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("Image_Update"))
                        objCmd.Parameters.Add("@ImageId", SqlDbType.Int).Value = data.ImageId;
                    if (store.Equals("Image_Add"))
                        objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = data.ProductId;

                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Name", SqlDbType.Char, 5).Value = data.Name;

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
        public int Delete(ProductImageDto data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                if (data.ImageId != 0 && data.ProductId!=0)
                {
                    store = "Image_Delete";

                }

                using (objCmd = new SqlCommand(store, connection))
                {
                    
                    objCmd.Parameters.Add("@ImageId", SqlDbType.Int).Value = data.ImageId;
                    objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = data.ProductId;

                    objCmd.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        try{
                            string ruta = @"C:\Users\Lara\source\repos\Colo\ClientMundoPanal\src\assets";
                            if (!File.Exists(ruta + "\\" + data.Name))
                            {
                                System.IO.File.Delete(data.Name);
                                return 1;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                        catch(Exception e){
                            throw e;
                        }
                        
                    }

                    return -2;
                }

            }


        }



    }
}