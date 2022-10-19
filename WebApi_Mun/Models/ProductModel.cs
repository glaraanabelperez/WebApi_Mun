using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Identificador producto
        /// </summary>
        public int? ProductId { get; set; }


        /// <summary>
        /// Identificador Categoria
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Identificador usuario al cual pertenece
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        public bool State { get; set; }

        /// <summary>
        /// Titulo
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Titulo
        /// </summary>
        public string Subtitle { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Imagen
        /// </summary>
        public string NameImage { get; set; }

        /// <summary>
        /// Fecha publicacion
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Destacado
        /// </summary>
        [Required]
        public bool Featured { get; set; }

        /// <summary>
        /// Promocion del producto
        /// </summary>
        public string Promotion { get; set; }
    }
}