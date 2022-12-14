using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Mun.Data;
using WebApi_Mun.Models;

namespace WebApi_Mun.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
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

    }
}
