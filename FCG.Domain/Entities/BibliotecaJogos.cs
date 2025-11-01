namespace FCG.Domain.Entities;

public class BibliotecaJogos
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid JogoId { get; set; }
    public DateTime DataAquisição { get; set; } = DateTime.UtcNow;
    
    // Navegação
    public virtual Usuario Usuario { get; set; } = null!;
    public virtual Jogo Jogo { get; set; } = null!;
}
