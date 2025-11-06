using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesEjercicioExamen.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
