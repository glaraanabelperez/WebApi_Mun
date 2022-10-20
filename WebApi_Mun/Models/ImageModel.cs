using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class ProductImageModel
    {
        /// <summary>
        /// Identificador tabla ProductImage
        /// </summary>
        public int? ProductImageId { get; set; }

        /// <summary>
        /// Identificador prducto
        /// </summary>
        public int? ProductId { get; set; }

        /// <summary>
        /// Identificador imagen
        /// </summary>
        public int? ImageId { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string  Name { get; set; }

       
    }
}