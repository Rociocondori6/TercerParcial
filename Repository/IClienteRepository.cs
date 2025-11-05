using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClasesEjercicioExamen.Models;

namespace ClasesEjercicioExamen.Repository
{
    public interface IClienteRepository
    {
        void AgregarCliente(Cliente cliente);
        Cliente ObtenerPorId(int id);
        List<Cliente> ObtenerTodos();
    }
}
