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
    public class ImageController : ApiController
    {
        public ImageLogic cat = new ImageLogic();

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

        /// <summary>
        /// Graba los datos del categoria
        /// </summary>
        /// <param name="data">Datos del categoria</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody] CategoryModel data)
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    cat.Save(data);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return Content(HttpStatusCode.InternalServerError, ex.Message);
                }        
            }
            return BadRequest("El modelo de datos esta incorrecto o vacio");
        }


        [HttpPost]
        public IHttpActionResult Update([FromBody] CategoryModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = cat.Save(data);
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


        /// <summary>
        /// Cambia el estado de la marca
        /// </summary>
        /// <param name="data">Datos del estado</param>
        /// <returns><c>1</c> Si se guardaron los datos</returns>
        [Route("api/category/state/")]
        [HttpPost]
        public IHttpActionResult ChangeState([FromBody] StateModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = cat.Desactive(data);

                    if (result > 0)
                        return Ok();
                    else
                        return BadRequest("El elemento a editar no existe");
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
