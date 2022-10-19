using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    public class UserInsertModel
    {
        /// <summary>
        /// Identificador Nombre negocio
        /// </summary>
        [Required]
        public string Business_Name { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [Required]
        public string  user_email { get; set; }

        /// <summary>
        /// contraseña del usuario
        /// </summary>
        [Required]
        public string Password { get; set; }

      

    }
}