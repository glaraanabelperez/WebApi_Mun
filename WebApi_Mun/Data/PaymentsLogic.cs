using MercadoPago.Resource.Payment;
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
    public class PaymentsLogic
    {
        internal static string connectionString = ConfigurationManager.ConnectionStrings["MundoConnection"].ConnectionString;


        /// <summary>
        /// Devuelve productos destacados
        /// </summary>
        /// <returns>Datos de producto</returns>
        public PaymentsModel[]  GetPaymentsByDate(DateTime date)
        {
            var items = new List<PaymentsModel>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var objCmd = new SqlCommand("Payments_ByDate", connection))
                {
                    connection.Open();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader objDR = objCmd.ExecuteReader();

                    while (objDR.Read())
                    {
                        
                        var c = new PaymentsModel();
                        c.PaymentId = objDR.GetInt32(0);
                      

                        items.Add(c);
                    }
                    return items.ToArray();

                }

            }

        }


        /// <summary>
        /// Graba el Pago realizado y los datos de la venta
        /// </summary>
        /// <param name="PaymentAdditionalInfo">Datos</param>
        /// <returns><c>.</c></returns>
        public void Save(Payment data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var store = "Payments_ADD";
                var objCmd = new SqlCommand(store, connection);

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.Parameters.Add("@PaymentId", SqlDbType.VarChar, 250).Value = data.Id;
                objCmd.Parameters.Add("@BuyerName", SqlDbType.VarChar, 250).Value = data.Payer!= null ? data.Payer.FirstName : "-";
                objCmd.Parameters.Add("@BuyerLastName", SqlDbType.VarChar, 250).Value = data.Payer != null ? data.Payer.LastName : "-";
                objCmd.Parameters.Add("@Direction", SqlDbType.VarChar, 250).Value = data.AdditionalInfo.Payer != null ? data.AdditionalInfo.Payer.Address.StreetName + data.AdditionalInfo.Payer.Address.StreetNumber : "-";
                objCmd.Parameters.Add("@Phone", SqlDbType.VarChar, 250).Value = data.AdditionalInfo.Payer!=null ? data.AdditionalInfo.Payer.Phone.AreaCode + data.AdditionalInfo.Payer.Phone.Number : "-";
                objCmd.Parameters.Add("@Email", SqlDbType.VarChar, 250).Value = data.Payer!= null ? data.Payer.Email:"-";
                objCmd.Parameters.Add("@Date", SqlDbType.DateTime2).Value = data.DateApproved;
                objCmd.Parameters.Add("@TotalAmount", SqlDbType.Money).Value = data.TransactionAmount;
                objCmd.Parameters.Add("@MethodPayment", SqlDbType.VarChar, 250).Value = data.PaymentMethodId;
                objCmd.Parameters.Add("@PaymentState", SqlDbType.VarChar, 250).Value = data.Status;
                objCmd.Parameters.Add("@Delivered", SqlDbType.TinyInt).Value = 0;

               try
               {
                   connection.Open();

                   var result = objCmd.ExecuteNonQuery();
                   connection.Close();

                   this.InsertProduct(data.Id, data.AdditionalInfo);
                  
               }
               catch (Exception e)
               {
                  throw e;
               }
            }

        }

        private const string INSERT =
        ";INSERT INTO [dbo].[Details] ( PaymentId, ProductId, quantity,Amount)" +
        " VALUES {0} ";

        private void InsertProduct(long? PaymentId, PaymentAdditionalInfo dataPayments)
        {
          using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objSqlCmd = new SqlCommand("", connection);

                string strFilter = string.Empty;

                foreach(PaymentItem it in dataPayments.Items)
                {
                    strFilter += "(" + PaymentId.ToString() + "," + it.Id + "," + it.Quantity + it.UnitPrice + " ),";
                }

                string strWithParams = string.Format(INSERT, strFilter);
                objSqlCmd.CommandType = CommandType.Text;

                connection.Open();

                #if DEBUG
                System.Diagnostics.Trace.WriteLine(objSqlCmd.CommandText);
                #endif

                }

            }

        public int Delivered(int PaymnetId, bool delivered)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand objCmd;
                var store = "";
                store = "dbo.Update_Payments";
                objCmd = new SqlCommand(store, connection);
                objCmd.CommandType = CommandType.StoredProcedure;

                objCmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = PaymnetId;
                objCmd.Parameters.Add("@@Delivered", SqlDbType.TinyInt).Value = delivered==true ? 1 :0;

                connection.Open();
                var result = objCmd.ExecuteNonQuery();
                return result;
            }
        }


    }
}