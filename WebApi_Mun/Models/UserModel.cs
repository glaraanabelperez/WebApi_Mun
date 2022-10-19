using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class UserModel
    {
        /// <summary>
        /// Identificador producto
        /// </summary>
        public int? User_id { get; set; }


        /// <summary>
        /// Identificador Nombre negocio
        /// </summary>
        public string Business_Name { get; set; }

        /// <summary>
        /// Identificador Slogan, descripcion comida, etc
        /// </summary>
        public string Slogan { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string  user_email { get; set; }

        /// <summary>
        /// contraseña del usuario
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Telefono
        /// </summary>
        public int Phone { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string Direction { get; set; }

        /// <summary>
        /// direccion de instagram
        /// </summary>
        public string Ig { get; set; }

        /// <summary>
        /// direccion de Facebook
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// nombre imagen logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// mostrar u ocultar whatsapp
        /// </summary>
        public bool OrdersWhatsapp { get; set; }

    }
}