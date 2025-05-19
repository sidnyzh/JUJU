using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    /// <summary>
    /// Repositorio específico para operaciones sobre la entidad Customer.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly JujuTestContext _context;

        /// <summary>
        /// Constructor del repositorio de clientes
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado</param>
        public CustomerRepository(JujuTestContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene los IDs de clientes que existen en la base de datos, según una lista de IDs dada.
        /// </summary>
        /// <param name="ids">Lista de IDs a verificar</param>
        /// <returns>Lista de IDs existentes en la base de datos</returns>
        public async Task<List<int>> GetExistingIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Customer
                .Where(c => ids.Contains(c.CustomerId))
                .Select(c => c.CustomerId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
