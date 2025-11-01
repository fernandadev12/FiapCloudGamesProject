namespace FCG.Domain.DTOs.Biblioteca;

public class BibliotecaDTO
{
    public Guid Id { get; set; }
    public Guid JogoId { get; set; }
    public string NomeJogo { get; set; } = string.Empty;
    public string DescricaoJogo { get; set; } = string.Empty;
    public string ImagemUrlJogo { get; set; } = string.Empty;
    public DateTime DataAquisição { get; set; }
}

public class AdquirirJogoDTO
{
    public Guid JogoId { get; set; }
}
