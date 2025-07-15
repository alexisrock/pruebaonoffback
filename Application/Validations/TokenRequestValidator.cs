using System.Threading.Tasks;
using Domain.Dto;
using Domain.Dto.Request;
using FluentValidation;

namespace Application.Validations
{
    public class TokenRequestValidator : AbstractValidator<TokenCreateRequest>
    {


        public TokenRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El userName es obligatorio")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("El password es obligatorio")
                .MinimumLength(2).WithMessage("El password debe tener al menos 2 caracteres");

       


        }

    }
}
