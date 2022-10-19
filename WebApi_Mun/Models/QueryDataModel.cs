namespace WebApi_Mun.Models
{
    /// <summary>
    /// Entidad para la consulta de datos
    /// </summary>
    /// <typeparam name="F">Clase a utilizar para el filtro</typeparam>
    /// <typeparam name="O">Enumerador a utilizar para el ordenamiento</typeparam>
    public class QueryDataModel<F, O>
    {
        /// <summary>
        /// Orden
        /// </summary>
        public O OrderField { get; set; }

        /// <summary>
        /// Dirección del ordenamiento
        /// </summary>
        public bool? OrderAsc { get; set; }

        /// <summary>
        /// Filtro a utilizar
        /// </summary>
        public F Filter { get; set; }

        /// <summary>
        /// Desde qué registro se deben obtener los datos
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Cantidad de registros a obtener
        /// </summary>
        public int Length { get; set; }
    }
}