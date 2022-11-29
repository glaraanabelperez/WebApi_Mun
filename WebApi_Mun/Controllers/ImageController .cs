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
        public ImageLogic imagenLogic = new ImageLogic();

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
                
                var list = imagenLogic.ListByProduct(productId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }

        /// <summary>
        /// Borra la imagen
        /// </summary>
        /// <param name="data">Datos de la imagen</param>
        /// <returns><c>1</c> Si se guardaron los datos</returns>
        [Route("api/images/delete")]
        [HttpPost]
        public IHttpActionResult DeleteImage([FromBody] ProductImageDto [] data)
        {
            try
            {
                foreach (ProductImageDto image in data)
                {
                    if (ModelState.IsValid && image.ProductImageId.HasValue)
                    {

                        string ruta = @"C:\Users\Lara\source\repos\Colo\ClientMundoPanal\src\assets";
                        int result = imagenLogic.Delete(image);
                        if (File.Exists(ruta + "\\" + image.Name) && result > 0)
                        {
                            System.IO.File.Delete(System.IO.Path.Combine(ruta, image.Name));
                        }
                    }
                    else
                    {
                        return BadRequest("El modelo de datos esta incorrecto o vacio");
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
            return Ok();
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

        [Route("api/insert_Image/{productId}")]
        [HttpPut]
        public IHttpActionResult InsertImage(int productId)
        {
            string ruta = @"C:\Users\Lara\source\repos\Colo\ClientMundoPanal\src\assets\";
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    for (var i = 0; i < httpRequest.Files.Count; i++)
                    {
                        var postedFile = httpRequest.Files[i];
                        var filePath = System.IO.Path.Combine(ruta, postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);

                        imagenLogic.Save(postedFile.FileName, productId);

                    }
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
          
        }

    }
}

