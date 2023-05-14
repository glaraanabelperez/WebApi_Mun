using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Mun.Data;
using WebApi_Mun.Models;
using static WebApi_Mun.Data.ProductLogic;

namespace WebApi_Mun.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        ProductLogic prodLogic = new ProductLogic();
        ImageLogic imagenLogic = new ImageLogic();

        /// <summary>
        /// Listado de todos los productos 
        /// </summary>
        [Route("api/Product/{productId}")]
        [HttpGet]
        public IHttpActionResult Get(int productId)
        {
            try
            {
                ProductModel prod = prodLogic.Get(productId);
                if (prod == null)
                {
                    return Content(HttpStatusCode.NotFound, "La solicitud no arroja resultados");
                }
                return Ok(prod);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
}

        /// <summary>
        /// Listado de todos los productos 
        /// </summary>
        [Route("api/ProductToCard/{productId}")]
        [HttpGet]
        public IHttpActionResult GetProductWithImages(int productId)
        {
            try
            {
                ProductModelDto prod = prodLogic.GetProductWithImages(productId);
                if (prod == null)
                {
                    return Content(HttpStatusCode.NotFound, "La solicitud no arroja resultados");
                }
                return Ok(prod);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
  
        /// <summary>
        /// Listado de todas las marcas
        /// </summary>
        [Route("api/products/list_feature")]
        [HttpGet]
        public IHttpActionResult GetProductsFeatured()
        {
            try
            {
                var list = prodLogic.GetProductsFeatured();
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
        /// Listado de todos los productos paginados y filtrados
        /// </summary>
        /// <param name="queryData">Filtros de la consulta</param>
        /// <returns>Listado de los productos</returns>
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IHttpActionResult List([FromBody] QueryDataModel<Data.ProductLogic.Filter, Data.ProductLogic.OrderFields> queryData)
        
        {
            try
            {
                return Ok(prodLogic.List(queryData.OrderField, queryData.OrderAsc, queryData.Filter, queryData.From, queryData.Length, out int RecordCount)) ;
            }
            catch (Exception ex) {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        /// <summary>
        /// Graba los datos del usuario
        /// </summary>
        /// <param name="data">Datos del usuario</param>
        /// <returns><c>true</c> Si se guardaron los datos</returns>
        [Route("api/Product/insert")]
        [HttpPost]
        public IHttpActionResult Insert([FromBody] ProductModel data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    prodLogic.Save(data);
                    return Ok();
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.InternalServerError, e.Message);
                }
            }

            return BadRequest("El modelo de datos esta incorrecto o vacio");
        }

        //[Route("api/Product/")]
        [HttpPut]
        public IHttpActionResult Update([FromBody] ProductModel data)
        {
            data.DiscountId=data.DiscountId==0 ?null : data.DiscountId;
            if (ModelState.IsValid)
            {
                try
                {
                    prodLogic.Save(data);
                    return Ok();
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.InternalServerError, e.Message);
                }
            }

            return BadRequest("El modelo de datos esta incorrecto o vacio");


        }


        /// <summary>
        /// Cambia el estado de la marca
        /// </summary>
        /// <param name="data">Datos del estado</param>
        /// <returns><c>1</c> Si se guardaron los datos</returns>
        [Route("api/product/state/{itemId}")]
        [HttpDelete]
        public IHttpActionResult ChangeState(int itemId)
        {
            try
            {
                int result = prodLogic.Desactive(itemId);

                if (result > 0)
                {
                    //string pathString = System.IO.Path.Combine(ruta, itemId.ToString());

                    imagenLogic.CleanFolder(itemId);
                    imagenLogic.DeleteFolder(itemId);

                    return Ok();
                }
                else
                    return BadRequest("El elemento no puede eliminarse");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Listado de todos los productos 
        /// </summary>
        [Route("api/Product/price/{percent}/{category}/{marca}")]
        [HttpGet]
        public IHttpActionResult ChangePrice(decimal percent, int category, int marca)
        {
            try
            {
                prodLogic.ChangePrice(percent, category, marca);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
