using FluentValidation;
using FCG.Domain.DTOs.Jogo;

namespace FCG.Application.Validators;

public class CriarJogoDTOValidator : AbstractValidator<CriarJogoDTO>
{
    public CriarJogoDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome do jogo é obrigatório")
            .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória")
            .Length(10, 1000).WithMessage("Descrição deve ter entre 10 e 1000 caracteres");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero")
            .LessThan(1000).WithMessage("Preço não pode ser maior que 1000");

        RuleFor(x => x.Categoria)
            .NotEmpty().WithMessage("Categoria é obrigatória")
            .Length(2, 50).WithMessage("Categoria deve ter entre 2 e 50 caracteres");

        RuleFor(x => x.ImagemUrl)
            .MaximumLength(500).WithMessage("URL da imagem deve ter no máximo 500 caracteres")
            .When(x => !string.IsNullOrEmpty(x.ImagemUrl));

        RuleFor(x => x.DataLancamento)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Data de lançamento não pode ser no futuro")
            .GreaterThanOrEqualTo(new DateTime(2000, 1, 1)).WithMessage("Data de lançamento deve ser após o ano 2000");
    }
}
