using Domain.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerDomain
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task CreateCustomer(Customer customer);
        Task UpdateCustomer(Customer customer);
        Task DeleteCustomer(int customerId);
    }
}
