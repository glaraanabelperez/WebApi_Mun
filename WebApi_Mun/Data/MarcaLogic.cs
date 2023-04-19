using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApi_Mun.Models;

namespace WebApi_Mun.Data
{
    public class MarcaLogic : BaseLogic
    {

        /// <summary>
        /// Devuelve todos las masrcas
        /// </summary>
        /// <returns>Lista de marcas</returns>
        public MarcaModel[] List()
        {
            var items = new List<MarcaModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using(var objCmd = new SqlCommand("paalerac_colores.[dbo].Marca_List", connection))
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
                using (var objCmd = new SqlCommand("paalerac_colores.[dbo].Marca_List_Active", connection))
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


        private  string SELECT_ALL_BY_MARCA =
        ";Select cm.MarcaId, m.[Name] " +
        " from paalerac_colores.[dbo].[CategoryMarcas] cm " +
        " inner join paalerac_colores.[dbo].Marcas m on m.MarcaId=cm.MarcaId where cm.CategoryId = {0} and m.[State]=1;";


        private string SELECT_ALL =
        ";Select m.MarcaId, m.[Name] " +
        " from paalerac_colores.[dbo].[Marcas] m where m.[State]=1  ";

        /// <summary>
        /// Devuelve marcas segun la categorias asociada
        /// </summary>
        /// <param name="categoryId">Identificador del categoria</param>
        /// <returns>Lista de marcas</returns>
        public List<MarcaModel> GetMarcasByCategory(int? categoryId)
        {
            var items = new List<MarcaModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objSqlCmd = new SqlCommand("", connection);

                string strFilter = string.Empty;
                string strWithParams= string.Empty;

                if (categoryId.HasValue)
                {
                    strFilter = string.Empty;

                    strFilter += "@CatgeoryId";
                    objSqlCmd.Parameters.Add("@CatgeoryId", SqlDbType.Int).Value = categoryId;

                    strWithParams = string.Format(SELECT_ALL_BY_MARCA, strFilter);
                    objSqlCmd.CommandText = strWithParams;
                }
                else
                {
                    objSqlCmd.CommandText = SELECT_ALL;
                }

                objSqlCmd.CommandType = CommandType.Text;

                connection.Open();

#if DEBUG
                System.Diagnostics.Trace.WriteLine(objSqlCmd.CommandText);
#endif

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = objSqlCmd;

                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                var tab = dataset.Tables[0].Rows;

                //lista para devolver los datos mapeados ProductModelList
                List<MarcaModel> list = new List<MarcaModel>();
                foreach (DataRow row in tab)
                {
                    MarcaModel cat = new MarcaModel();
                    cat.MarcaId = Convert.ToInt32(row["MarcaId"]);
                    cat.Name = Convert.ToString(row["Name"]);

                    list.Add(cat);
                }
                connection.Close();
                return (list);
            }

        }

        /// <summary>
        /// Graba la marca
        /// </summary>
        /// <param name="data">Datos de la marca</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        public int Save(MarcaModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                SqlCommand objCmd;
                var store = "";
                if (data.MarcaId.HasValue && data.MarcaId != 0 )
                {
                    store = "paalerac_colores.[dbo].Marca_Update";
                    objCmd = new SqlCommand("Marca_Update", connection);
                }
                else
                    store = "paalerac_colores.[dbo].Marca_Add";

                using (objCmd = new SqlCommand(store, connection))
                {
                    if (store.Equals("paalerac_colores.[dbo].Marca_Update"))
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
        public int Desactive(int itemId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objCmd;
                var store = "";
                store = "paalerac_colores.[dbo].Marca_Desactive";
                objCmd = new SqlCommand(store, connection);

                using (objCmd)
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@MarcaId", SqlDbType.Int).Value = itemId;

                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

    }
}