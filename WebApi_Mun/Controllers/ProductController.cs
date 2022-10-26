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
                return Ok();
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
            var item = prodLogic.Save(null, data);
            if (item != -1)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [Route("api/Product/{productId}")]
        [HttpPut]
        public IHttpActionResult Update(int productId, [FromBody] ProductModel data)
        {

            var prod = prodLogic.Save(productId, data);
            if (prod == -2)
                return Content(HttpStatusCode.BadRequest, "Los datos solicitados no existen");

            return Ok(prod);

        }

    
  }    

}
