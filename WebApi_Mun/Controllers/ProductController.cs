using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Mun.Data;
using WebApi_Mun.Models;
using static WebApi_Mun.Data.ProductLogic;

namespace WebApi_Mun.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
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
        public DataTableModel List([FromBody] QueryDataModel<Data.ProductLogic.Filter, Data.ProductLogic.OrderFields> queryData)
        {

            return prodLogic.List(queryData.OrderField, queryData.OrderAsc, queryData.Filter, queryData.From, queryData.Length, out int RecordCount);

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
                    var result = prodLogic.Save(data);
                    return Ok(result);
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

            if (ModelState.IsValid)
            {
                try
                {
                    var prod = prodLogic.Save(data);
                    if (prod <= 0)
                        return Content(HttpStatusCode.BadRequest, "Los datos solicitados no existen");

                    return Ok(prodLogic.Save(data));
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


    }

}
