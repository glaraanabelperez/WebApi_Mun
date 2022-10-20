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
        [Required]
        public int? CategoryId { get; set; }


        /// <summary>
        /// Nombre
        /// </summary>
        [Required]
        public string Name { get; set; }

    }
}