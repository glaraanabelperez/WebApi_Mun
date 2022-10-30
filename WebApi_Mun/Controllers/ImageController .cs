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
        public ImageLogic im = new ImageLogic();

        /// <summary>
        /// Listado de todas las categorias segun usuario
        /// </summary>
        [Route("api/imageByProduct/")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                //List<ProductModel> orderDToList;
                
                var list = im.ListByProduct();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

       

        /// <summary>
        /// Graba los datos de una imagen
        /// </summary>
        /// <param name="data">Datos de la imagen</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [HttpPut]
        public IHttpActionResult Put([FromBody] ProductImageDto data)
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    im.Save(data);
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
        public IHttpActionResult Update([FromBody] ProductImageDto data)
        {
            if (ModelState.IsValid && data.ProductImageId.HasValue)
            {
                try
                {
                    var result = im.Save(data);
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
        /// Borra la imagen
        /// </summary>
        /// <param name="data">Datos de la imagen</param>
        /// <returns><c>1</c> Si se guardaron los datos</returns>
        [Route("api/category/state/")]
        [HttpPost]
        public IHttpActionResult DeleteImage([FromBody] ProductImageDto data)
        {
            if (ModelState.IsValid && data.ProductImageId.HasValue)
            {
                try
                {
                    int result = im.Delete(data);

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
