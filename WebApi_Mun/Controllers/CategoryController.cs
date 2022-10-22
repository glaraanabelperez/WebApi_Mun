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
    public class CategoryController : ApiController
    {
        public Category product = new Category();

        /// <summary>
        /// Listado de todas las categorias segun usuario
        /// </summary>
        [Route("api/category/list/")]
        [HttpGet]
        public IHttpActionResult GetAll(int userId)
        {
            try
            {
                //List<ProductModel> orderDToList;
                var list = Data.Category.List(userId);
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
        [HttpGet]
        public IHttpActionResult Get(int categoryId)
        {
            var userItem = Data.Category.Get(categoryId);
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
            var userItem = Data.Category.Save(null, data);
            if (userItem != 0)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Update(int categoryId, [FromBody] CategoryModel data)
        {
            var userItem = Data.Category.Save(categoryId, data);
            if (userItem != 0)
            {
                return NotFound();
            }
            return Ok();
        }


    }
}
