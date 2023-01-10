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
        /// Listado de todas las marcas
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
        /// Listado de todas las marcas activas
        /// </summary>
        [Route("api/marca/listActive/")]
        [HttpGet]
        public IHttpActionResult GetAllActive()
        {
            try
            {
                var list = mar.ListActive();
                if (list == null || list.Length==0 )
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
        /// Devuelve los datos de una marca
        /// </summary>
        /// <param name="categoryId">Identificador de la marca</param>
        /// <returns>Datos de la marca</returns>
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
        /// Graba los datos de la marca
        /// </summary>
        /// <param name="data">Datos de la marca</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody] MarcaModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = mar.Save(data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            
            return BadRequest("El modelo de datos esta incorrecto o vacio");
        }

        /// <summary>
        /// Actualiza la marca
        /// </summary>
        /// <param name="data">Datos de la marca</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [HttpPost]
        public IHttpActionResult Post([FromBody] MarcaModel data)
        {
            if (ModelState.IsValid )
            {
                try
                {
                    var result = mar.Save(data);
                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("El elemento a editar no existe");
                    }

                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                }
            }
            
            return BadRequest("El modelo de datos esta incorrecto o vacio");
        }

        /// <summary>
        /// Cambia el estado de la marca
        /// </summary>
        /// <param name="data">Datos del estado</param>
        /// <returns><c>1</c> Si se guardaron los datos</returns>
        [Route("api/marca/state/{itemId}")]
        [HttpDelete]
        public IHttpActionResult ChangeState(int itemId)
        {

            try
            {
                int result = mar.Desactive(itemId);

                if (result > 0)
                    return Ok();
                else
                    return BadRequest("El elemento no puede eliminarse");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
