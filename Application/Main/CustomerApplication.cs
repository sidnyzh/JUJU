using Application.DTO.Customer.Request;
using Application.DTO.Customer.Response;
using Application.Interfaces;
using AutoMapper;
using Domain.Core;
using Domain.Entity;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Main
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerDomain _customerDomain;
        private readonly IMapper _mapper;

        public CustomerApplication(ICustomerDomain customerDomain, IMapper mapper)
        {
            _customerDomain = customerDomain;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los clientes desde la capa de dominio
        /// y los transforma a DTOs de respuesta
        /// </summary>
        public async Task<IEnumerable<GetCustomerResponse>> GetAllCustomers()
        {
            var customers = await _customerDomain.GetAllCustomers().ConfigureAwait(false);
            return _mapper.Map<IEnumerable<GetCustomerResponse>>(customers);
        }

        /// <summary>
        /// Crea un nuevo cliente a partir del DTO recibido
        /// </summary>
        public async Task CreateCustomer(CreateCustomerRequest createCustomer)
        {
            var customer = _mapper.Map<Customer>(createCustomer);
            await _customerDomain.CreateCustomer(customer).ConfigureAwait(false);
        }

        /// <summary>
        /// Actualiza un cliente existente con los datos del DTO
        /// </summary>
        public async Task UpdateCustomer(UpdateCustomerRequest updateCustomer)
        {
            var customer = _mapper.Map<Customer>(updateCustomer);
            await _customerDomain.UpdateCustomer(customer).ConfigureAwait(false);
        }

        /// <summary>
        /// Elimina un cliente por su identificador
        /// </summary>
        public async Task DeleteCustome(int customerId)
        {
            await _customerDomain.DeleteCustomer(customerId).ConfigureAwait(false);
        }
    }
}
