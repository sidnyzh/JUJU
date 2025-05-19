using Domain.Entity;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transversal.Exceptions;

namespace Domain.Core
{
    public class CustomerDomain : ICustomerDomain
    {
        private readonly IGenericRepositoriy<Customer> _customerRepository;
        private readonly IGenericRepositoriy<Post> _postRepository;

        public CustomerDomain(IGenericRepositoriy<Customer> customerRepository, 
            IGenericRepositoriy<Post> postRepository)
        {
            _customerRepository = customerRepository;
            _postRepository = postRepository;
        }



        /// <summary>
        /// Obtiene todos los clientes
        /// </summary>
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customerRepository.GetAllAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Crea un nuevo cliente, validando que el nombre no se repita
        /// </summary>
        public async Task CreateCustomer(Customer customer)
        {
            await NameAlreadyExist(customer).ConfigureAwait(false);
            await _customerRepository.CreateAsync(customer).ConfigureAwait(false);
        }

        /// <summary>
        /// Actualiza un cliente, validando que el nombre no se repita
        /// </summary>
        public async Task UpdateCustomer(Customer customer)
        {
            await EnsureCustomerExists(customer.CustomerId).ConfigureAwait(false);
            await NameAlreadyExist(customer).ConfigureAwait(false);
            await _customerRepository.UpdateAsync(customer).ConfigureAwait(false);
        }

        /// <summary>
        /// Elimina un cliente y todos los posts asociados.
        /// </summary>
        /// <param name="customerId">ID del cliente a eliminar</param>
        public async Task DeleteCustomer(int customerId)
        {
            await EnsureCustomerExists(customerId).ConfigureAwait(false);

            var relatedPosts = await _postRepository
                .FindByConditionAsync(p => p.CustomerId == customerId)
                .ConfigureAwait(false);

            if (relatedPosts.Any())
                await _postRepository.DeleteRangeAsync(relatedPosts).ConfigureAwait(false);

            await _customerRepository.DeleteAsync(customerId).ConfigureAwait(false);
        }


        /// <summary>
        /// Verifica si ya existe un cliente con el mismo nombre
        /// </summary>
        private async Task NameAlreadyExist(Customer customer)
        {
            bool nameExists = await _customerRepository
                .AnyAsync(c => c.Name == customer.Name && c.CustomerId != customer.CustomerId)
                .ConfigureAwait(false);

            if (nameExists)
            {
                throw new BadRequestException("Ya existe un cliente con este nombre");
            }
        }

        private async Task EnsureCustomerExists(int customerId)
        {
            bool exists = await _customerRepository.AnyAsync(c => c.CustomerId == customerId).ConfigureAwait(false);

            if (!exists)
                throw new NotFoundException("El cliente no existe.");
        }
    }
}
