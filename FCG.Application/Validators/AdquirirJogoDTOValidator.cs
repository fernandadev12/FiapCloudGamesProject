using FluentValidation;
using FCG.Domain.DTOs.Biblioteca;

namespace FCG.Application.Validators;

public class AdquirirJogoDTOValidator : AbstractValidator<AdquirirJogoDTO>
{
    public AdquirirJogoDTOValidator()
    {
        RuleFor(x => x.JogoId)
            .NotEmpty().WithMessage("ID do jogo é obrigatório")
            .NotEqual(Guid.Empty).WithMessage("ID do jogo deve ser válido");
    }
}
