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
        /// email
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// contraseña del usuario
        /// </summary>
        [Required]
        public string Password { get; set; }

      

    }
}