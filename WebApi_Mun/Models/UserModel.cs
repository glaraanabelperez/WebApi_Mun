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
        public int? UserId { get; set; }

        /// <summary>
        /// username
        /// </summary>
        public string  UserName { get; set; }

        /// <summary>
        /// contraseña del usuario
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Rol
        /// </summary>
        public int RoleId { get; set; }

       
    }

    public class UserLogin
    {
        /// <summary>
        /// username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// contraseña del usuario
        /// </summary>
        public string Password { get; set; }
    }
}