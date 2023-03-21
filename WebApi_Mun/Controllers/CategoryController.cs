using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Mun.Data;
using WebApi_Mun.Models;

namespace WebApi_Mun.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [EnableCors(origins: "https://pañaleracolores.com.ar/", headers: "*", methods: "*")]
    public class CategoryController : ApiController
    {
        public CategoryLogic cat = new CategoryLogic();

        /// <summary>
        /// Listado de todas las categorias segun usuario
        /// </summary>
        [Route("api/category/list/")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                //List<ProductModel> orderDToList;
                
                var list = cat.List();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        /// <summary>
        /// Listado de todas las categorias activas
        /// </summary>
        [Route("api/category/listActive/")]
        [HttpGet]
        public IHttpActionResult GetAllActive()
        {
            try
            {
                var list = cat.ListActive();
                if (list == null || list.Length == 0)
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
        /// Listado de todas las categorias segun marca
        /// </summary>
        [Route("api/category/listByMarca/")]
        [Route("api/category/listByMarca/{marcaId}")]
        [HttpGet]
        public IHttpActionResult ListByMarca(int? marcaId)
        {
            try
            {
                var list = cat.GetCategoriesByMarca(marcaId);
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
        [Route("api/category/{categoryId}/")]
        [HttpGet]
        public IHttpActionResult Get(int categoryId)
        {
            var userItem = cat.Get(categoryId);
            if (userItem == null)
            {
                return NotFound();
            }
            return Ok(userItem);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] CategoryModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = cat.Save(data);
                    if (result <0)
                        return Content(HttpStatusCode.NotFound, "Error en la Accion");

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
        /// Cambia el estado de la marca
        /// </summary>
        /// <param name="data">Datos del estado</param>
        /// <returns><c>1</c> Si se guardaron los datos</returns>
        [Route("api/category/state/{categoryId}")]
        [HttpDelete]
        public IHttpActionResult ChangeState( int categoryId)
        {
         
                try
                {
                    int result = cat.Desactive(categoryId);

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
