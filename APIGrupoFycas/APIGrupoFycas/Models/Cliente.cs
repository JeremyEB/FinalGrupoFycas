using System;
using System.Collections.Generic;

namespace APIGrupoFycas.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
