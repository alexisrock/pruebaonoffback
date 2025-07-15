using System.Diagnostics.CodeAnalysis;
using Domain.Dto.Request;
using FluentValidation;

namespace Application.Validations
{
    [ExcludeFromCodeCoverage]
    public class TareaRequestValidator : AbstractValidator<TareaRequest>
    {

        public TareaRequestValidator()
        {
            RuleFor(x => x.NameTarea)
               .NotEmpty().WithMessage("El nombre de la tarea es obligatorio")
               .MinimumLength(2).WithMessage("El nombre de la tarea debe tener al menos 2 caracteres");

            RuleFor(x => x.DescriptionTarea)
                .NotEmpty().WithMessage("La descripcion de la tarea es obligatorio")
                .MinimumLength(2).WithMessage("La descripcion de la tarea debe tener al menos 2 caracteres");

        }


    }

    [ExcludeFromCodeCoverage]
    public class TareaUpdateRequestValidator : AbstractValidator<TareaupdateRequest>
    {

        public TareaUpdateRequestValidator()
        {

            RuleFor(x => x.IdTarea)
                .Must(n => n >= 0).WithMessage("El id de la tarea debe ser mayor que cero.");

           
        }


    }
}
