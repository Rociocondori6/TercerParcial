using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Clases.Data
{
    public class ApplicationDbContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=LAPTOP-J7C6C098\\SQLEXPRESS;Database=Parcial3;Trusted_Connection=True;TrustServerCertificate=True;"

            );
        }



    }
}
