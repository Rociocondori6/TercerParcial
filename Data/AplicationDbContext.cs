using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Models;
using Microsoft.EntityFrameworkCore;
using ClasesEjercicioExamen.Models;

namespace ClasesEjercicioExamen.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaDetalle> VentaDetalles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=LAPTOP-J7C6C098\\SQLEXPRESS;Database=ExamenVentasDB;Trusted_Connection=True;TrustServerCertificate=True;"

            );
        }
        

        
    }
}
