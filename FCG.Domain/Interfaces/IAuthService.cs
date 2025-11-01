using FCG.Domain.DTOs;
using FCG.Domain.DTOs.Usuario;

namespace FCG.Domain.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> LoginAsync(LoginUsuarioDTO loginDto);
    Task<AuthResponseDTO> RegistrarAsync(CriarUsuarioDTO registrarDto);
    string GerarJwtToken(UsuarioDTO usuario);
}
