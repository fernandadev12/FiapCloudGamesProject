using FluentValidation;
using FCG.Domain.DTOs.Usuario;

namespace FCG.Application.Validators;

public class LoginUsuarioDTOValidator : AbstractValidator<LoginUsuarioDTO>
{
    public LoginUsuarioDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email deve ser válido");

        RuleFor(x => x.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória");
    }
}
