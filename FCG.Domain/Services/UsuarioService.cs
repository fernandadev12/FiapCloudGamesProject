using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Domain.DTOs.Usuario;
using FCG.Domain.Enums;

namespace FCG.Domain.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioDTO> CriarUsuarioAsync(CriarUsuarioDTO criarUsuarioDto)
    {
        // Verificar se email já existe
        var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(criarUsuarioDto.Email);
        if (usuarioExistente != null)
            throw new InvalidOperationException("Email já cadastrado");

        // Criar usuário
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = criarUsuarioDto.Nome,
            Email = criarUsuarioDto.Email,
            SenhaHash = await GerarHashSenhaAsync(criarUsuarioDto.Senha),
            Tipo = TipoUsuario.Usuario,
            DataCriacao = DateTime.UtcNow
        };

        await _usuarioRepository.AdicionarAsync(usuario);

        return new UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Tipo = usuario.Tipo.ToString(),
            DataCriacao = usuario.DataCriacao
        };
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _usuarioRepository.ObterPorEmailAsync(email);
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        return await _usuarioRepository.ObterPorIdAsync(id);
    }

    public Task<bool> VerificarSenhaAsync(string senha, string senhaHash)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.Verify(senha, senhaHash));
    }

    public Task<string> GerarHashSenhaAsync(string senha)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(senha));
    }

    public async Task<IEnumerable<UsuarioDTO>> ObterTodosUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync();
        
        return usuarios.Select(usuario => new UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Tipo = usuario.Tipo.ToString(),
            DataCriacao = usuario.DataCriacao
        });
    }
}
