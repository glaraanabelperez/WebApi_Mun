using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class ImageModel
    {

        /// <summary>
        /// Identificador Imagen
        /// </summary>
        public int? ImageId { get; set; }


        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }


    }
}