using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
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
        [Route("api/images/{productId}")]
        [HttpGet]
        public IHttpActionResult GetByProduct(int productId)
        {
            try
            {
                //List<ProductModel> orderDToList;
                
                var list = im.ListByProduct(productId);
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
        [Route("api/images/{imageId}")]
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
        public IHttpActionResult Update(int imageId, [FromBody] ProductImageDto data)
        {
            if (ModelState.IsValid && data.ProductImageId.HasValue && imageId == data.ProductImageId)
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
        [Route("api/images/state/")]
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

        [Route("api/images")]
        [HttpPost]
        public IHttpActionResult verifyImageOnserver([FromBody] ProductImageDto image)
        {
            
            if (ModelState.IsValid)
            {
                string ruta = @"C:\Users\Lara\source\repos\Colo\ClientMundoPanal\src\assets";
                if (!File.Exists(ruta + "\\" + image.Name))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
                
            }

            return InternalServerError();
        }

        [Route("api/imagesOnServer")]
        [HttpDelete]
        public IHttpActionResult deleteImageOnserver([FromBody] ProductImageDto image)
        {

            if (ModelState.IsValid)
            {
                string ruta = @"C:\Users\Lara\source\repos\Colo\ClientMundoPanal\src\assets";
                if (!File.Exists(ruta + "\\" + image.Name))
                {
                    System.IO.File.Delete(image.Name);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }

            return InternalServerError();
        }

        [Route("api/insert_Image/{productId}")]
        [HttpPut]
        public IHttpActionResult InsertImage(int productId)
        {
            string ruta = @"C:\Users\Lara\source\repos\Colo\ClientMundoPanal\src\assets\";

            //var httpRequest = HttpContext.Current.Request;
            //var docfiles = new List<string>();
            //var postedFile = httpRequest.Files[0];
            //var filePath = System.IO.Path.Combine(ruta, postedFile.FileName);
            //postedFile.SaveAs(filePath);
            //docfiles.Add(filePath);
            //return Ok();

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[0];
                    var filePath = System.IO.Path.Combine(ruta, postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                    return Ok();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            return InternalServerError();

        }

    }
}


//Maciel Rui13:15
//var files = HttpContext.Request.Form.Files;
//Maciel Rui13:17
//var pathArchivoOriginal = Path.Combine(_configurationManager.PathFotosCredencialesArchivoOriginal, nombreArchivoOriginal);
//File.WriteAllBytes(pathArchivoOriginal, bytes);
//Maciel Rui13:19
//var formFile = files[0];
//using var target = new MemoryStream();
//formFile.CopyTo(target);
//target.ToArray()