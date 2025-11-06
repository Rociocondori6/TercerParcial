using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Models;

namespace ClasesEjercicioExamen.Repository
{
    public interface IProductoRepository
    {
        void AgregarProducto(Producto producto);
        Producto ObtenerPorId(int id);
        List<Producto> ObtenerTodos();
    }
}