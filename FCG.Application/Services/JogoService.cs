using FCG.Domain.Interfaces;
using FCG.Domain.DTOs.Jogo;
using FCG.Domain.Entities;

namespace FCG.Application.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _jogoRepository;

    public JogoService(IJogoRepository jogoRepository)
    {
        _jogoRepository = jogoRepository;
    }

    public async Task<JogoDTO> CriarJogoAsync(CriarJogoDTO criarJogoDto)
    {
        var jogo = new Jogo
        {
            Id = Guid.NewGuid(),
            Nome = criarJogoDto.Nome,
            Descricao = criarJogoDto.Descricao,
            Preco = criarJogoDto.Preco,
            Categoria = criarJogoDto.Categoria,
            ImagemUrl = criarJogoDto.ImagemUrl,
            DataLancamento = criarJogoDto.DataLancamento,
            DataCriacao = DateTime.UtcNow
        };

        await _jogoRepository.AdicionarAsync(jogo);

        return new JogoDTO
        {
            Id = jogo.Id,
            Nome = jogo.Nome,
            Descricao = jogo.Descricao,
            Preco = jogo.Preco,
            Categoria = jogo.Categoria,
            ImagemUrl = jogo.ImagemUrl,
            DataLancamento = jogo.DataLancamento,
            DataCriacao = jogo.DataCriacao
        };
    }

    public async Task<JogoDTO> ObterJogoPorIdAsync(Guid id)
    {
        var jogo = await _jogoRepository.ObterPorIdAsync(id);
        if (jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");

        return new JogoDTO
        {
            Id = jogo.Id,
            Nome = jogo.Nome,
            Descricao = jogo.Descricao,
            Preco = jogo.Preco,
            Categoria = jogo.Categoria,
            ImagemUrl = jogo.ImagemUrl,
            DataLancamento = jogo.DataLancamento,
            DataCriacao = jogo.DataCriacao
        };
    }

    public async Task<IEnumerable<JogoDTO>> ObterTodosJogosAsync()
    {
        var jogos = await _jogoRepository.ObterTodosAsync();
        
        return jogos.Select(jogo => new JogoDTO
        {
            Id = jogo.Id,
            Nome = jogo.Nome,
            Descricao = jogo.Descricao,
            Preco = jogo.Preco,
            Categoria = jogo.Categoria,
            ImagemUrl = jogo.ImagemUrl,
            DataLancamento = jogo.DataLancamento,
            DataCriacao = jogo.DataCriacao
        });
    }

    public async Task<JogoDTO> AtualizarJogoAsync(Guid id, CriarJogoDTO atualizarJogoDto)
    {
        var jogo = await _jogoRepository.ObterPorIdAsync(id);
        if (jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");

        jogo.Nome = atualizarJogoDto.Nome;
        jogo.Descricao = atualizarJogoDto.Descricao;
        jogo.Preco = atualizarJogoDto.Preco;
        jogo.Categoria = atualizarJogoDto.Categoria;
        jogo.ImagemUrl = atualizarJogoDto.ImagemUrl;
        jogo.DataLancamento = atualizarJogoDto.DataLancamento;
        jogo.DataAtualizacao = DateTime.UtcNow;

        await _jogoRepository.AtualizarAsync(jogo);

        return new JogoDTO
        {
            Id = jogo.Id,
            Nome = jogo.Nome,
            Descricao = jogo.Descricao,
            Preco = jogo.Preco,
            Categoria = jogo.Categoria,
            ImagemUrl = jogo.ImagemUrl,
            DataLancamento = jogo.DataLancamento,
            DataCriacao = jogo.DataCriacao
        };
    }

    public async Task<bool> DeletarJogoAsync(Guid id)
    {
        var jogo = await _jogoRepository.ObterPorIdAsync(id);
        if (jogo == null)
            throw new KeyNotFoundException("Jogo não encontrado");

        await _jogoRepository.DeletarAsync(jogo);
        return true;
    }
}
