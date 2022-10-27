using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class StateModel
    {

        /// <summary>
        /// Identificador Entidad
        /// </summary>
        public int? ItemId { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public bool State { get; set; }

    }
}