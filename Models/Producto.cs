using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesEjercicioExamen.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        // Relación: Un Producto puede estar en muchas Líneas de Venta
        public ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }
}
