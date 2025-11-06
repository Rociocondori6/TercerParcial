using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Models;

namespace ClasesEjercicioExamen.Repository
{
    public interface IVentaRepository
    {
        void RegistrarVenta(Venta venta);
        List<VentaDetalle> ObtenerReporteVentasPorProducto();
    }
}

