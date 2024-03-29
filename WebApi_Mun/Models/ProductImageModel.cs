﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class ProductImageModel
    {

        /// <summary>
        /// Identificador Categoria
        /// </summary>
        public int? ProductImageId { get; set; }

        /// <summary>
        /// Identificador Categoria
        /// </summary>
        public int? ImageId { get; set; }


        /// <summary>
        /// Identificador Categoria
        /// </summary>
        public int? ProductId { get; set; }



        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }


    }
}