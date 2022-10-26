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
    public class DiscountController : ApiController
    {
        public DiscountLogic dis = new DiscountLogic();

        /// <summary>
        /// Listado de todas las categorias segun usuario
        /// </summary>
        [Route("api/discount/list/")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                var list = dis.List();
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
        /// Listado de todas las categorias segun usuario
        /// </summary>
        [Route("api/discount/listActive/")]
        [HttpGet]
        public IHttpActionResult GetAllActive()
        {
            try
            {
                var list = dis.ListActive();
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
        [HttpGet]
        public IHttpActionResult Get(int categoryId)
        {
            var userItem = dis.Get(categoryId);
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
        public IHttpActionResult Put([FromBody] DiscountModel data)
        {

            if (data == null || !ModelState.IsValid)
            {
                return BadRequest("El modelo de datos esta incorrecto o vacio");
            }
            try
            {
                var result = dis.Save(null, data);
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
        [Route("api/discount/{discountId}")]
        [HttpPost]
        public IHttpActionResult Post(int discountId, [FromBody] DiscountModel data)
        {

            if (data == null || !ModelState.IsValid || data.DiscountId==null)
            {
                return BadRequest("El modelo de datos esta incorrecto o vacio");
            }
            try
            {
                var result = dis.Save(discountId, data);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
