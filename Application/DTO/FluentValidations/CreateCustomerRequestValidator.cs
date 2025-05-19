using Application.DTO.Customer.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO.FluentValidations
{
    public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator() 
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
        }
        
    }
}
