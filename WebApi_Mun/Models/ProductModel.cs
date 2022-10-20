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
        /// Identificador nombre dle producto
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Identificador descripcion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identificador Categoria
        /// </summary>
        [Required]
        public int CategoryId { get; set; }


        public int MarcaId { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Codigo descuento
        /// </summary>
        public int Discount { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        public bool State { get; set; }


        /// <summary>
        /// Destacado
        /// </summary>
        [Required]
        public bool Featured { get; set; }


        /// <summary>
        /// usuario vinculado
        /// </summary>
        public int CreatedBy { get; set; }


    }

    public class ProductModelDto
    {
        /// <summary>
        /// Identificador producto
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Identificador nombre dle producto
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Identificador descripcion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Identificador Categoria
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public int MarcaId { get; set; }

        public string MarcaName { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Codigo descuento
        /// </summary>
        public int Discount { get; set; }

        /// <summary>
        /// monto descuento
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        public bool State { get; set; }


        /// <summary>
        /// Destacado
        /// </summary>
        [Required]
        public bool Featured { get; set; }


        /// <summary>
        /// usuario vinculado
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Imagenes Vinculadas
        /// </summary>
        public List<ProductImageModel> images { get; set; } = new List<ProductImageModel>();

    }
}