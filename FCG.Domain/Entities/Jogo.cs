namespace FCG.Domain.Entities;

public class Jogo
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public string ImagemUrl { get; set; } = string.Empty;
    public DateTime DataLancamento { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
    
    // Navegação para Biblioteca de Jogos
    public virtual ICollection<BibliotecaJogos> BibliotecaJogos { get; set; } = new List<BibliotecaJogos>();
}
