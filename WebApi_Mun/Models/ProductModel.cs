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
        public int? CategoryId { get; set; }

        /// <summary>
        /// Id marca
        /// </summary>
        public int? MarcaId { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal? Price { get; set; }


        /// <summary>
        /// Codigo descuento
        /// </summary>
        public int? DiscountId { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public bool? State { get; set; }

        /// <summary>
        /// Stock
        /// </summary>
        public bool? Stock { get; set; }

        /// <summary>
        /// Destacado
        /// </summary>
        public bool? Featured { get; set; }

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
        /// Identificador nombre del producto
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// descripcion
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Nombre Categoria
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Nombre marca
        /// </summary>
        public string MarcaName { get; set; }

        /// <summary>
        /// monto descuento
        /// </summary>
        public int? DiscountAmount { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public bool? State { get; set; }

        /// <summary>
        /// Stock
        /// </summary>
        public bool? Stock { get; set; }

        /// <summary>
        /// Destacado
        /// </summary>
        public bool? Featured { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Precio
        /// </summary>
        public decimal? PriceWithDiscount { get; set; }
        
        /// <summary>
        /// Imagen destacada
        /// </summary>
        public string ImageName { get; set; }

    }
}