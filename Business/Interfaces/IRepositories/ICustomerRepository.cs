using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ICustomerRepository 
    {
        Task<List<int>> GetExistingIdsAsync(IEnumerable<int> ids);
    }
}
