using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class LoginModel
    {
        /// <summary>
        /// Identificador 
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Identificador Nombre negocio
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [Required]
        public string email { get; set; }

        /// <summary>
        /// contraseña del usuario
        /// </summary>
        [Required]
        public string Password { get; set; }

      

    }
}