using MercadoPago.Resource.Common;
using MercadoPago.Resource.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using WebApi_Mun.Models;

namespace WebApi_Mun.Data
{
    public class ImageLogic : BaseLogic
    {
        //string ruta = (System.Web.Hosting.HostingEnvironment.MapPath("assets"));
        string ruta = "D:\\Inetpub\\vhosts\\panaleracolores.com.ar\\httpdocs\\assets";

        /// <summary>
        /// Devuelve todos las imagenes segun el producto
        /// </summary>
        /// <returns>Lista de marcas</returns>
        public ProductImageDto[] ListByProduct(int productId)
        {
            var items = new List<ProductImageDto>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("paalerac_colores.[dbo].Image_GetByProduct", connection))
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
        public int Save(string data, int productId)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                store = "paalerac_colores.[dbo].Image_Add";

                using (objCmd = new SqlCommand(store, connection))
                {

                    if (store.Equals("paalerac_colores.[dbo].Image_Add"))
                        objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = productId;

                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = data;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }
           
        }

        /// <summary>
        /// Borra la imagen
        /// </summary>
        /// <param name="data">.</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        public int Delete(ProductImageDto data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "paalerac_colores.[dbo].Image_Delete";

                using (objCmd = new SqlCommand(store, connection))
                {
                    
                    objCmd.Parameters.Add("@ImageId", SqlDbType.Int).Value = data.ImageId;
                    objCmd.Parameters.Add("@ProductImageId", SqlDbType.Int).Value = data.ProductImageId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();
   
                    return result;
                }
            }
        }

        public void DeleteFolder(int productId)
        {

            try
            {
                string pathString = System.IO.Path.Combine(this.ruta, productId.ToString());
                if (System.IO.Directory.Exists(pathString))
                {            
                    System.IO.Directory.Delete(pathString);
                    //System.IO.File.Delete(pathString);

                }

            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public void CleanFolder(int productId)
        {
            string pathString = System.IO.Path.Combine(this.ruta, productId.ToString());
            if (Directory.Exists(pathString))
            {
                List<string> strFiles = Directory.GetFiles(pathString, "*", SearchOption.AllDirectories).ToList();

                foreach (string fichero in strFiles)
                {
                    System.IO.File.Delete(fichero);
                }
            }
           
        }

    }
}