using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Models;

namespace ClasesEjercicioExamen.Models
{
    public class VentaDetalle
    {
        public int id { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        // fk a Venta
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
        // fk a Producto
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
