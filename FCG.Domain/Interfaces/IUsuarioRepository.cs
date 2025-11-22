using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorIdAsync(Guid id);
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
    Task AtualizarAsync(Usuario usuario);
    Task<bool> ExisteEmailAsync(string email);
    Task<IEnumerable<Usuario>> ObterTodosAsync();
}
