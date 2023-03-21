using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Mun.Data;
using WebApi_Mun.Models;

namespace WebApi_Mun.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [EnableCors(origins: "https://pañaleracolores.com.ar/", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public UserLogic user = new UserLogic();

        ///// <summary>
        ///// Listado de todos los useros 
        ///// </summary>
        //[HttpGet]
        //public IHttpActionResult GetAll()
        //{
        //    var result = user.List();
        //    if (result == null)
        //    {
        //        return Content(HttpStatusCode.NotFound, "La solicitud no arroja resultados");
        //    }
        //    return Ok(result);

        //}

        /// <summary>
        /// Devuelve los datos de un usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Datos del usuario</returns>
        [Route("api/User/{userId}")]
        [HttpGet]
        public IHttpActionResult Get(int userId)
        {
                var result = user.Get(userId);
                if (result == null)
                {
                    return Content(HttpStatusCode.NotFound, "La solicitud no arroja resultados");
                }
                return Ok(result);
            
        }

        /// <summary>
        /// Devuelve los datos de un usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Datos del usuario</returns>
        [Route("api/User/login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody] LoginModel data)
        {
            var result = user.Login(data);
            if (result == null)
            {
                return Content(HttpStatusCode.NotFound, "La solicitud no arroja resultados");
            }
            return Ok(result);

        }

        /// <summary>
        /// Graba los datos del usuario
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [Route("api/User/insert")]
        [HttpPut]
        public IHttpActionResult Insert([FromBody] UserModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.Insert(data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            return BadRequest("El modelo de datos esta incorrecto o vacio");

        }

        [Route("api/User/update/{userId}")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] UserModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = user.Update(data);
                    if (result < 0)
                        return Content(HttpStatusCode.NotFound, "El dato a editar no existe");

                    return Ok();
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            return BadRequest("El modelo de datos esta incorrecto o vacio");
        }

        [Route("api/User/email")]
        [HttpGet]
        public IHttpActionResult SendEmail()
        {
          
            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress("glaraanabelperez@gmail.com", "Kyocode", System.Text.Encoding.UTF8);//Correo de salida
                correo.To.Add("glaraanabelperez@gmail.com"); //Correo destino?
                correo.Subject = "Correo de prueba"; //Asunto
                correo.Body = "Este es un correo de prueba desde c#"; //Mensaje del correo
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
                smtp.Port = 25; //Puerto de salida
                smtp.Credentials = new System.Net.NetworkCredential("glaraanabelperez@gmail.com", "30608545jose");//Cuenta de correo
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.EnableSsl = true;//True si el servidor de correo permite ssl
                smtp.Send(correo);

                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
          
        }

    }
}
