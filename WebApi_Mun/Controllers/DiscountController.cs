﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Mun.Data;
using WebApi_Mun.Models;

namespace WebApi_Mun.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DiscountController : ApiController
    {
        public DiscountLogic dis = new DiscountLogic();

        /// <summary>
        /// Listado de todos los descuentos
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
        /// Listado de todos los descuentos activos
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
        /// Devuelve los datos de un descuento
        /// </summary>
        /// <param name="categoryId">Identificador del desc</param>
        /// <returns>Datos del descuento</returns>
        [Route("api/discount/{discountId}")]
        [HttpGet]
        public IHttpActionResult Get(int discountId)
        {
            var userItem = dis.Get(discountId);
            if (userItem == null)
            {
                return NotFound();
            }
            return Ok(userItem);
        }

        /// <summary>
        /// Actualiza los datos del descuento
        /// </summary>
        /// <param name="data">Datos del decsuento</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [Route("api/discount")]
        [HttpPost]
        public  IHttpActionResult Post([FromBody] DiscountModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = dis.Save(data);
                    if (result > 0)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("NO SE PUDO COMPLETAR LA ACCION");
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
        /// Cambia el estado del descuento
        /// </summary>
        /// <param name="data">Datos del descuento</param>
        /// <returns><c>1</c> Si se guardaron los datos</returns>
        [Route("api/discount/state/{disId}")]
        [HttpDelete]
        public IHttpActionResult ChangeState(int disId)
        {

            try
            {
                int result = dis.Desactive(disId);

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
