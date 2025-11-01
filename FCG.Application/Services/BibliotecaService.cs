using FCG.Domain.Interfaces;
using FCG.Domain.DTOs.Biblioteca;
using FCG.Domain.Entities;

namespace FCG.Application.Services;

public class BibliotecaService : IBibliotecaService
{
    private readonly IBibliotecaRepository _bibliotecaRepository;
    private readonly IJogoRepository _jogoRepository;

    public BibliotecaService(IBibliotecaRepository bibliotecaRepository, IJogoRepository jogoRepository)
    {
        _bibliotecaRepository = bibliotecaRepository;
        _jogoRepository = jogoRepository;
    }

    public async Task<BibliotecaDTO> AdquirirJogoAsync(Guid usuarioId, Guid jogoId)
    {
        // Verificar se usuário já tem o jogo
        var jaPossui = await _bibliotecaRepository.UsuarioTemJogoAsync(usuarioId, jogoId);
        if (jaPossui)
            throw new InvalidOperationException("Jogo já adquirido");

        // Verificar se jogo existe
        var jogo = await _jogoRepository.ObterPorIdAsync(jogoId);
        if (jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");

        var biblioteca = new BibliotecaJogos
        {
            Id = Guid.NewGuid(),
            UsuarioId = usuarioId,
            JogoId = jogoId,
            DataAquisição = DateTime.UtcNow
        };

        await _bibliotecaRepository.AdicionarAsync(biblioteca);

        return new BibliotecaDTO
        {
            Id = biblioteca.Id,
            JogoId = biblioteca.JogoId,
            NomeJogo = jogo.Nome,
            DescricaoJogo = jogo.Descricao,
            ImagemUrlJogo = jogo.ImagemUrl,
            DataAquisição = biblioteca.DataAquisição
        };
    }

    public async Task<IEnumerable<BibliotecaDTO>> ObterBibliotecaUsuarioAsync(Guid usuarioId)
    {
        var biblioteca = await _bibliotecaRepository.ObterPorUsuarioAsync(usuarioId);
        
        return biblioteca.Select(item => new BibliotecaDTO
        {
            Id = item.Id,
            JogoId = item.JogoId,
            NomeJogo = item.Jogo.Nome,
            DescricaoJogo = item.Jogo.Descricao,
            ImagemUrlJogo = item.Jogo.ImagemUrl,
            DataAquisição = item.DataAquisição
        });
    }

    public async Task<BibliotecaDTO?> ObterJogoNaBibliotecaAsync(Guid usuarioId, Guid jogoId)
    {
        var biblioteca = await _bibliotecaRepository.ObterPorUsuarioEJogoAsync(usuarioId, jogoId);
        
        if (biblioteca == null)
            return null;

        return new BibliotecaDTO
        {
            Id = biblioteca.Id,
            JogoId = biblioteca.JogoId,
            NomeJogo = biblioteca.Jogo.Nome,
            DescricaoJogo = biblioteca.Jogo.Descricao,
            ImagemUrlJogo = biblioteca.Jogo.ImagemUrl,
            DataAquisição = biblioteca.DataAquisição
        };
    }
}
