using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class DiscountModel
    {

        /// <summary>
        /// Identificador descuento
        /// </summary>
        public int? DiscountId { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public byte Amount { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public bool? State { get; set; }

        /// <summary>
        /// Identificador usuario creador
        /// </summary>
        public int? CreatedBy { get; set; }


    }
}