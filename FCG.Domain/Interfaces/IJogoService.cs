using FCG.Domain.DTOs.Jogo;

namespace FCG.Domain.Interfaces;

public interface IJogoService
{
    Task<JogoDTO> CriarJogoAsync(CriarJogoDTO criarJogoDto);
    Task<JogoDTO> ObterJogoPorIdAsync(Guid id);
    Task<IEnumerable<JogoDTO>> ObterTodosJogosAsync();
    Task<JogoDTO> AtualizarJogoAsync(Guid id, CriarJogoDTO atualizarJogoDto);
    Task<bool> DeletarJogoAsync(Guid id);
}
