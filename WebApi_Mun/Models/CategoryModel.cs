using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class CategoryModel
    {

        /// <summary>
        /// Identificador Categoria
        /// </summary>
        public int? CategoryId { get; set; }


        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public bool State { get; set; }

    }
}