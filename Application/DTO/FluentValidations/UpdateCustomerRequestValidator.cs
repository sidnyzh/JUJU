using Application.DTO.Customer.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.FluentValidations
{
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator() 
        {
            RuleFor(x => x.CustomerId)
               .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor que 0.");

            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
    }
}
