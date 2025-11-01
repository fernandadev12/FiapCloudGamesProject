using FCG.Domain.Entities;

namespace FCG.Domain.Interfaces;

public interface IJogoRepository
{
    Task<Jogo?> ObterPorIdAsync(Guid id);
    Task<IEnumerable<Jogo>> ObterTodosAsync();
    Task AdicionarAsync(Jogo jogo);
    Task AtualizarAsync(Jogo jogo);
    Task DeletarAsync(Jogo jogo);
}
