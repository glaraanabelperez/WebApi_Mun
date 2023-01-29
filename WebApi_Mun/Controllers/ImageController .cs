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
using System.Drawing.Imaging;
using System.Drawing;

namespace WebApi_Mun.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        public ImageLogic imagenLogic = new ImageLogic();
        string ruta = @"C:\Users\LARA\source\repos\Client_Mundo\src\assets";


        /// <summary>
        /// Listado de todas las imagenes segun producto
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
                    string pathString = System.IO.Path.Combine(this.ruta, image.ProductId.ToString());

                    if (image.ProductImageId!=0 && image.ProductImageId.HasValue && !String.IsNullOrEmpty(image.Name))
                    {

                        int result = imagenLogic.Delete(image);
                        if (File.Exists(pathString + "\\" + image.Name) && result > 0)
                        {
                            System.IO.File.Delete(System.IO.Path.Combine(pathString, image.Name));
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

            if (image.ProductId!=0)
            {
                string pathString = System.IO.Path.Combine(this.ruta, image.ProductId.ToString());

                if (!File.Exists((pathString + "\\" + image.Name)))
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
            if (productId != 0)
            {
                // To create a string that specifies the path to a subfolder under your
                // top-level folder, add a name for the subfolder to folderName.
                string pathString = System.IO.Path.Combine(this.ruta, productId.ToString());
                if (!System.IO.File.Exists(pathString))
                {
                    System.IO.Directory.CreateDirectory(pathString);
                }

                try
                {
                    var httpRequest = HttpContext.Current.Request;
                    if (httpRequest.Files.Count > 0)
                    {
                        for (var i = 0; i < httpRequest.Files.Count; i++)
                        {
                            var postedFile = httpRequest.Files[i];
                            var filePath = System.IO.Path.Combine(pathString, postedFile.FileName);
                            postedFile.SaveAs(filePath);
                            imagenLogic.Save(postedFile.FileName, productId);

                            byte[] datosArchivo = null;
                            using (var binaryReader = new BinaryReader(postedFile.InputStream))
                            {
                                datosArchivo = binaryReader.ReadBytes(postedFile.ContentLength);
                            }

                            System.IO.File.WriteAllBytes(filePath, datosArchivo);

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
            else{
                return BadRequest();
            }
            
        }

    }
}

