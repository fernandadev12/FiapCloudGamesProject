using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IBibliotecaRepository
{
    Task<BibliotecaJogos?> ObterPorUsuarioEJogoAsync(Guid usuarioId, Guid jogoId);
    Task<IEnumerable<BibliotecaJogos>> ObterPorUsuarioAsync(Guid usuarioId);
    Task AdicionarAsync(BibliotecaJogos biblioteca);
    Task<bool> UsuarioTemJogoAsync(Guid usuarioId, Guid jogoId);
}
