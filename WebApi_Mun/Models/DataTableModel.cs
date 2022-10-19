using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Mun.Models
{
    /// <summary>
    /// Entidad para la respuesta filtrada y paginada,  a las consultas de las base de datos
    /// </summary>
    public class DataTableModel
    {
            /// <summary>
            /// Cantidad de registros filtrados
            /// </summary>
            public int RecordsCount { get; set; }

            /// <summary>
            /// Datos a devolver
            /// </summary>
            public dynamic Data { get; set; }
    }
}