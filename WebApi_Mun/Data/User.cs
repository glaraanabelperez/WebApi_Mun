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
    public class User
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["MenuConnection"].ConnectionString;

        /// <summary>
        /// Devuelve todos los usuarios sin paginar
        /// </summary>
        /// <returns>Lista de usuarios</returns>
        internal List<UserModel> List()
        {
            string queryString = "SELECT *  FROM dbo.Users;";
            List<UserModel> list = new List<UserModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    using (SqlDataReader objDR = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (!objDR.Read())
                        {
                            return null;
                        }
                        else
                        {
                            while (objDR.Read())
                            {
                                list.Add(new UserModel()
                                {
                                    User_id = objDR.GetInt32(0),
                                    Business_Name = DBNull.Value.Equals(objDR.GetValue(1)) ? null : (objDR.GetString(1)),
                                    Slogan = DBNull.Value.Equals(objDR.GetValue(2)) ? null : (objDR.GetString(2)),
                                    user_email = DBNull.Value.Equals(objDR.GetValue(3)) ? null : (objDR.GetString(3)),
                                    Password = DBNull.Value.Equals(objDR.GetValue(4)) ? null : (objDR.GetString(4)),
                                    Phone = DBNull.Value.Equals(objDR.GetValue(5)) ? 0 : (objDR.GetInt32(5)),
                                    Direction = DBNull.Value.Equals(objDR.GetValue(6)) ? null : (objDR.GetString(6)),
                                    Ig = DBNull.Value.Equals(objDR.GetValue(7)) ? null : (objDR.GetString(7)),
                                    Facebook = DBNull.Value.Equals(objDR.GetValue(8)) ? null : (objDR.GetString(8)),
                                    Logo = DBNull.Value.Equals(objDR.GetValue(9)) ? null : (objDR.GetString(9)),
                                    OrdersWhatsapp = (objDR.GetByte(10) == 0 ? false : true),
                                });
                            }

                            return list;
                        }

                    }



                }
            }
                 
        }

        internal LoginModel Login(LoginModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("Login", connection))
                {
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = data.email;
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
                        item.UserId = objDR.GetInt32(0);
                        item.BusinessName = DBNull.Value.Equals(objDR.GetValue(1)) ? null : (objDR.GetString(1));                   
                        item.email = DBNull.Value.Equals(objDR.GetValue(2)) ? null : (objDR.GetString(2));
                        item.Password = DBNull.Value.Equals(objDR.GetValue(3)) ? null : (objDR.GetString(3));
                      
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
                        item.User_id = objDR.GetInt32(0);
                        item.Business_Name = DBNull.Value.Equals(objDR.GetValue(1)) ? null : (objDR.GetString(1));
                        item.Slogan = DBNull.Value.Equals(objDR.GetValue(2)) ? null : (objDR.GetString(2));
                        item.user_email = DBNull.Value.Equals(objDR.GetValue(3)) ? null : (objDR.GetString(3));
                        item.Password = DBNull.Value.Equals(objDR.GetValue(4)) ? null : (objDR.GetString(4));
                        item.Phone = DBNull.Value.Equals(objDR.GetValue(5)) ? 0 : (objDR.GetInt32(5));
                        item.Direction = DBNull.Value.Equals(objDR.GetValue(6)) ? null : (objDR.GetString(6));
                        item.Ig = DBNull.Value.Equals(objDR.GetValue(7)) ? null : (objDR.GetString(7));
                        item.Facebook = DBNull.Value.Equals(objDR.GetValue(8)) ? null : (objDR.GetString(8));
                        item.Logo = DBNull.Value.Equals(objDR.GetValue(9)) ? null : (objDR.GetString(9));
                        item.OrdersWhatsapp = objDR.GetByte(10) == 0 ? false : true;

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

                    if (data.Business_Name != null)
                        objCmd.Parameters.Add("@Business_Name", SqlDbType.VarChar).Value = data.Business_Name;
                    if (data.Direction != null)
                        objCmd.Parameters.Add("@Direction", SqlDbType.VarChar).Value = data.Direction;
                    if (data.Facebook != null)
                        objCmd.Parameters.Add("@Facebook", SqlDbType.NVarChar, 250).Value = data.Facebook;
                    if (data.Ig != null)
                        objCmd.Parameters.Add("@Ig", SqlDbType.NVarChar, 250).Value = data.Ig;
                    if (data.Logo != null)
                        objCmd.Parameters.Add("@Logo", SqlDbType.NVarChar, 25).Value = data.Logo;
                    if (data.OrdersWhatsapp.ToString().Length > 0)
                        objCmd.Parameters.Add("@OrdersWhatsapp", SqlDbType.TinyInt).Value = data.OrdersWhatsapp;
                    if (data.Password != null)
                        objCmd.Parameters.Add("@Password", SqlDbType.NVarChar, 250).Value = data.Password;
                    if (data.Phone.ToString().Length > 0)
                        objCmd.Parameters.Add("@Phone", SqlDbType.Int).Value = data.Phone;
                    if (data.Slogan != null)
                        objCmd.Parameters.Add("@Slogan", SqlDbType.NVarChar, 250).Value = data.Slogan;
                    if (data.user_email != null)
                        objCmd.Parameters.Add("@User_email", SqlDbType.NVarChar, 250).Value = data.user_email;


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
        internal int Insert(UserInsertModel data)
        {
            using (var connection = new SqlConnection(connectionString))
            {

                using (SqlCommand objCmd = new SqlCommand("User_Add", connection))
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    if (data.Business_Name != null)
                        objCmd.Parameters.Add("@Business_Name", SqlDbType.VarChar).Value = data.Business_Name;
                    objCmd.Parameters.Add("@User_email", SqlDbType.NVarChar, 250).Value = data.user_email;
                    objCmd.Parameters.Add("@Password", SqlDbType.VarChar, 250).Value = data.Password;


                    connection.Open();
                    var result = objCmd.ExecuteNonQuery();

                    return result;
                }
                   
            }

        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <returns>Retorna 0</returns>
        internal static int Delete(int userId)
        {
            using (var connection = new SqlConnection(connectionString))
            {
               using(SqlCommand objCmd = new SqlCommand("User_Delete", connection))
                    {
                        objCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        connection.Open();
                        var result = objCmd.ExecuteNonQuery();

                        return result;
                    }
                    
            }

        }

    }
}