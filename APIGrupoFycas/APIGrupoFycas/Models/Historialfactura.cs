using System;
using System.Collections.Generic;

namespace APIGrupoFycas.Models
{
    public partial class Historialfactura
    {
        public int IdHistorialfactura { get; set; }
        public int? ClienteId { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }
        public int? FacturaId { get; set; }
        public decimal? MontoSolicitado { get; set; }
        public decimal? Tasa { get; set; }
        public decimal? Cuotas { get; set; }
        public decimal? CuotasMensuales { get; set; }
        public decimal? Capital { get; set; }
        public decimal? Interes { get; set; }
        public decimal? PagoNuevo { get; set; }
        public decimal? PagoRealizado { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
