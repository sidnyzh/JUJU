using Application.DTO.Post.Request;
using FluentValidation;

namespace Application.DTO.FluentValidations
{
    public class UpdatePostRequestValidator : AbstractValidator<UpdatePostRequest>
    {
        public UpdatePostRequestValidator()
        {
            RuleFor(x => x.PostId)
                .GreaterThan(0).WithMessage("El ID del post debe ser mayor que 0.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MaximumLength(100).WithMessage("El título no puede superar los 100 caracteres.");
;

            RuleFor(x => x.Type)
                .NotEmpty().GreaterThan(0);


            When(x => x.Type != 1 && x.Type != 2 && x.Type != 3, () =>
            {
                RuleFor(x => x.Category)
                    .NotEmpty().WithMessage("La categoría es obligatoria cuando el tipo no es 1, 2 o 3.")
                    .MaximumLength(100).WithMessage("La categoría no puede superar los 100 caracteres.");
            });

            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("El ID del cliente debe ser mayor que 0.");
        }
    }
}
