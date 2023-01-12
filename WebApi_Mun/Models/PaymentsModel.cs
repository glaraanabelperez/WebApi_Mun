using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{

    public class PaymentsModel
    {
        /// <summary>
        /// Identificador .
        /// </summary>
        public int? PaymentId { get; set; }

        /// <summary>
        /// Identificador nombre .
        /// </summary>
        public string BuyerName { get; set; }

        /// <summary>
        /// Identificador Apellido.
        /// </summary>
        public string BuyerLastName { get; set; }

        /// <summary>
        /// Identificador descripcion.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Telefono.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Identificador email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Identificador Fecha Pago.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Identificador monto abonado.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Identificador Forma de Pago.
        /// </summary>
        public string MethodPayment { get; set; }

        /// <summary>
        /// Identificador Estado del Pago (Pendiente , aprovado).
        /// </summary>
        public string SatetPayment { get; set; }

        /// <summary>
        /// Establece si el pedido ah sido entregado.
        /// </summary>
        public bool? Delivered { get; set; }

    }

}