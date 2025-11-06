using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Data;
using ClasesEjercicioExamen.Models;
using Microsoft;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClasesEjercicioExamen.Repository
{
    public class VentaRepository : IVentaRepository
    {
        private readonly ApplicationDbContext _context;
        public VentaRepository(ApplicationDbContext context)
        {
            _context = context;

        }
        public void RegistrarVenta(Venta venta)
        {
            _context.Ventas.Add(venta);
            _context.SaveChanges();
        }
        public List<VentaDetalle> ObtenerReporteVentasPorProducto()
        {
            return _context.VentaDetalles
                .Include(vd => vd.Producto)
                .ToList();
        }
    }
}
   