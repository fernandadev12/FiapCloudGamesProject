using FCG.Domain.DTOs.Biblioteca;

namespace FCG.Domain.Interfaces;

public interface IBibliotecaService
{
    Task<BibliotecaDTO> AdquirirJogoAsync(Guid usuarioId, Guid jogoId);
    Task<IEnumerable<BibliotecaDTO>> ObterBibliotecaUsuarioAsync(Guid usuarioId);
    Task<BibliotecaDTO?> ObterJogoNaBibliotecaAsync(Guid usuarioId, Guid jogoId);
}
