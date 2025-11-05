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
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AgregarCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }
        public Cliente ObtenerPorId(int id)
        {
            return _context.Clientes.Find(id);
        }
        public List<Cliente> ObtenerTodos()
        {
            return _context.Clientes.ToList();
        }
    }
}
