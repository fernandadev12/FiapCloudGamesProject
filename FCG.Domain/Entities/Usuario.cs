using FCG.Domain.Enums;

namespace FCG.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public TipoUsuario Tipo { get; set; } = TipoUsuario.Usuario; // Atende: "Dois níveis de acesso"
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
    
    // Navegação para Biblioteca de Jogos
    public virtual ICollection<BibliotecaJogos> BibliotecaJogos { get; set; } = new List<BibliotecaJogos>();
}
