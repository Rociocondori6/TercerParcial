using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Models;

namespace ClasesEjercicioExamen.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime FechaVenta { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        // Clave Foránea (FK) al Cliente
        public int ClienteId { get; set; }
        // Propiedad de Navegación:
        public Cliente Cliente { get; set; }
        // Relación: Una Venta tiene muchos detalles (productos)
        public ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }
}
