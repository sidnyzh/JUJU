using Application.DTO.Customer.Request;
using Application.DTO.Customer.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerApplication
    {
        Task<IEnumerable<GetCustomerResponse>> GetAllCustomers();
        Task CreateCustomer(CreateCustomerRequest createCustomer);
        Task UpdateCustomer(UpdateCustomerRequest updateCustomer);
        Task DeleteCustome(int customerId);
    }
}
