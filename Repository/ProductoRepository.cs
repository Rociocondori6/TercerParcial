using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Data;
using ClasesEjercicioExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace ClasesEjercicioExamen.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AgregarProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }
        public Producto ObtenerPorId(int id)
        {
            return _context.Productos.Find(id);
        }
        public List<Producto> ObtenerTodos()
        {
            return _context.Productos.ToList();
        }



    }
}
