using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class MarcaModel
    {

        /// <summary>
        /// Identificador marca
        /// </summary>
        public int? MarcaId { get; set; }


        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public bool State { get; set; }

    }
}