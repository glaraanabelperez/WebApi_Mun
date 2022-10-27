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
    public class MarcaLogic
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MundoConnection"].ConnectionString;

        /// <summary>
        /// Devuelve todos las masrcas
        /// </summary>
        /// <returns>Lista de marcas</returns>
        public MarcaModel[] List()
        {
            var items = new List<MarcaModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using(var objCmd = new SqlCommand("Marca_List", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();
                    var count = 0;
                    while (objDR.Read())
                    {
                        count = count + 1;
                        var c = new MarcaModel();
                        c.MarcaId = objDR.GetInt32(0);
                        c.Name = objDR.GetString(1);
                        c.State = objDR.GetByte(2)==1 ? true: false;

                        items.Add(c);
                    }
                    return items.ToArray(); 

                }
                
            }
        
        }

        /// <summary>
        /// Devuelve las marcas activas
        /// </summary>
        /// <returns>Lista de marcas activas</returns>
        public MarcaModel[] ListActive()
        {
            var items = new List<MarcaModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Marca_List_Active", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();
                    while (objDR.Read())
                    {
                        var c = new MarcaModel();
                        c.MarcaId = objDR.GetInt32(0);
                        c.Name = objDR.GetString(1);
                        c.State = objDR.GetByte(2) == 1 ? true : false;

                        items.Add(c);
                    }
                    return items.ToArray();

                }

            }

        }

        /// <summary>
        /// Devuelve los datos de una marca
        /// </summary>
        /// <param name="userId">Identificador de la marca</param>
        /// <returns>Datos de marca</returns>
        public MarcaModel Get(int marcaId)
        {
            var items = new MarcaModel();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Marca_Get", connection))
                {
                    objCmd.Parameters.Add("@MarcaId", SqlDbType.Int).Value = marcaId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (var objDR = objCmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!objDR.Read())
                        {
                            return null;
                        }

                        items.MarcaId = objDR.GetInt32(0);
                        items.Name = objDR.GetString(1);
                        items.State = objDR.GetByte(2) == 1 ? true : false;

                    }
                }
                return items;
            }

        }

        /// <summary>
        /// Graba la marca
        /// </summary>
        /// <param name="data">Datos de la marca</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        public int Save(int? marcaId, MarcaModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                if (data.MarcaId.HasValue && data.MarcaId != 0 )
                {
                    store = "Marca_Update";
                    objCmd = new SqlCommand("Marca_Update", connection);
                }
                else
                    store = "Marca_Add";

                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("Marca_Update"))
                        objCmd.Parameters.Add("@MarcaId", SqlDbType.Int).Value = data.MarcaId;
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@Name", SqlDbType.VarChar, 150).Value = data.Name;
                   
                    try
                    {
                        connection.Open();
                        var result = objCmd.ExecuteNonQuery();

                        return result;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    
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
            string queryString = string.Format("update Marcas set [State]={0} where MarcaId= {1}", data.State ? 1 : 0, data.ItemId);
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