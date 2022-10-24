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
    public class UserLogic
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MenuConnection"].ConnectionString;

      
        internal LoginModel Login(LoginModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Login", connection))
                {
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = data.UserName;
                    command.Parameters.Add("@Password", SqlDbType.VarChar).Value = data.Password;

                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var objDR = command.ExecuteReader(CommandBehavior.SingleRow))
                    {

                        if (!objDR.Read())
                        {
                            return null;
                        }
                        var item = new LoginModel();
                        item.UserName = objDR.GetString(0);
                        item.Password = DBNull.Value.Equals(objDR.GetValue(1)) ? null : (objDR.GetString(1));

                        return item;
                    }

                }
            }
        }

        /// <summary>
        /// Devuelve los datos de un usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Datos del usuario</returns>
        internal  UserModel Get(int userId)
        {
            var items = new UserModel();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("User_Get", connection))
                {
                    command.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (var objDR = command.ExecuteReader(CommandBehavior.SingleRow))
                    {

                        if (!objDR.Read())
                        {
                            return null;
                        }
                        var item = new UserModel();
                        item.UserName = objDR.GetString(0);
                        item.Password = DBNull.Value.Equals(objDR.GetValue(1)) ? null : (objDR.GetString(1));
                      

                        return item;
                    }

                }
            }

        }

        /// <summary>
        /// Graba el usuario
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        internal  int Update(int userId, UserModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand objCmd = new SqlCommand("User_Update", connection))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

                    if (data.UserName != null)
                        objCmd.Parameters.Add("@Business_Name", SqlDbType.VarChar).Value = data.UserName;
                    if (data.Password != null)
                        objCmd.Parameters.Add("@Direction", SqlDbType.VarChar).Value = data.Password;
                  


                    connection.Open();


                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }
            }
 
        }

        /// <summary>
        /// Graba el usuario
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        internal int Insert(UserModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                using (SqlCommand objCmd = new SqlCommand("User_Add", connection))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 250).Value = data.UserName;
                    objCmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = data.Password;


                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }

            }

        }

 
    }

    
}