namespace FCG.Domain.DTOs;

public class AuthResponseDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraEm { get; set; }
    public Usuario.UsuarioDTO Usuario { get; set; } = null!;
}
