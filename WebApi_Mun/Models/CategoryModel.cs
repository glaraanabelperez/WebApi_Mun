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
        /// Identificador producto
        /// </summary>
        [Required]
        public int UserId { get; set; }


        /// <summary>
        /// Identificador Categoria
        /// </summary>
        [Required]
        public int? CategoryId { get; set; }


        /// <summary>
        /// Descripcion
        /// </summary>
        [Required]
        public string Description { get; set; }

    }
}