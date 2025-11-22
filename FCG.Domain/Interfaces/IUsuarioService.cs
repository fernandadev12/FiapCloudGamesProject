using FCG.Domain.DTOs.Usuario;

namespace FCG.Domain.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioDTO> CriarUsuarioAsync(CriarUsuarioDTO criarUsuarioDto);
    Task<Domain.Entities.Usuario?> ObterPorEmailAsync(string email);
    Task<Domain.Entities.Usuario?> ObterPorIdAsync(Guid id);
    Task<bool> VerificarSenhaAsync(string senha, string senhaHash);
    Task<string> GerarHashSenhaAsync(string senha);
     Task<IEnumerable<UsuarioDTO>> ObterTodosUsuariosAsync();
}
