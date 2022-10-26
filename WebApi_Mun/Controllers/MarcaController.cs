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
    public class MarcaController : ApiController
    {
        public MarcaLogic mar = new MarcaLogic();

        /// <summary>
        /// Listado de todas las categorias segun usuario
        /// </summary>
        [Route("api/marca/list/")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var list = mar.List();
                if (list == null)
                {
                    return NotFound();
                }
                
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listado de todas las categorias segun usuario
        /// </summary>
        [Route("api/category/listActive/")]
        [HttpGet]
        public IHttpActionResult GetAllActive()
        {
            try
            {
                var list = mar.ListActive();
                if (list == null)
                {
                    return NotFound();
                }

                return Ok(list);

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }


        /// <summary>
        /// Devuelve los datos de un categoria
        /// </summary>
        /// <param name="categoryId">Identificador del categoria</param>
        /// <returns>Datos del categoria</returns>
        [Route("api/marca/{marcaId}")]
        [HttpGet]
        public IHttpActionResult Get(int marcaId)
        {
            var userItem = mar.Get(marcaId);
            if (userItem == null)
            {
                return NotFound();
            }
            return Ok(userItem);
        }

        /// <summary>
        /// Graba los datos del categoria
        /// </summary>
        /// <param name="data">Datos del categoria</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody] MarcaModel data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest("El modelo de datos esta incorrecto o vacio");
            }
            try
            {
                var result = mar.Save(null, data);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
           
        }

        /// <summary>
        /// Graba los datos del categoria
        /// </summary>
        /// <param name="data">Datos del categoria</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [Route("api/marca/{marcaId}")]
        [HttpPost]
        public IHttpActionResult Post(int marcaId, [FromBody] MarcaModel data)
        {
            if (data == null || !ModelState.IsValid )
            {
                return BadRequest("El modelo de datos esta incorrecto o vacio");
            }
            try
            {
                var result = mar.Save(marcaId, data);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

    }
}
