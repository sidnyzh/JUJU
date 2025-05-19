using Application.DTO.Customer.Request;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Customer
{
    /// <summary>
    /// Controlador para gestionar operaciones relacionadas con los clientes.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;

        /// <summary>
        /// Constructor que inyecta la lógica de aplicación para clientes.
        /// </summary>
        /// <param name="customerApplication">Servicio de aplicación para clientes</param>
        public CustomerController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerApplication.GetAllCustomers();
            return Ok(customers);
        }

        /// <summary>
        /// Crea un nuevo cliente.
        /// </summary>
        /// <param name="createCustomer">DTO con los datos del cliente a crear</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest createCustomer)
        {
            await _customerApplication.CreateCustomer(createCustomer);
            return Ok();
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="updateCustomer">DTO con los datos actualizados del cliente</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerRequest updateCustomer)
        {
            await _customerApplication.UpdateCustomer(updateCustomer);
            return Ok();
        }

        /// <summary>
        /// Elimina un cliente y sus posts asociados.
        /// </summary>
        /// <param name="customerId">ID del cliente a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(int customerId)
        {
            await _customerApplication.DeleteCustome(customerId);
            return Ok();
        }
    }
}
