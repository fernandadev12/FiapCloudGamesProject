using FCG.Domain.Interfaces;
using FCG.Domain.DTOs;
using FCG.Domain.DTOs.Usuario;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FCG.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioService _usuarioService;
    private readonly IConfiguration _configuration;

    public AuthService(IUsuarioService usuarioService, IConfiguration configuration)
    {
        _usuarioService = usuarioService;
        _configuration = configuration;
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginUsuarioDTO loginDto)
    {
        var usuario = await _usuarioService.ObterPorEmailAsync(loginDto.Email);
        if (usuario == null)
            throw new UnauthorizedAccessException("Email ou senha inválidos");

        var senhaValida = await _usuarioService.VerificarSenhaAsync(loginDto.Senha, usuario.SenhaHash);
        if (!senhaValida)
            throw new UnauthorizedAccessException("Email ou senha inválidos");

        var usuarioDto = new Domain.DTOs.Usuario.UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Tipo = usuario.Tipo.ToString()
        };

        var token = GerarJwtToken(usuarioDto);

        return new AuthResponseDTO
        {
            Token = token,
            ExpiraEm = DateTime.UtcNow.AddHours(1),
            Usuario = usuarioDto
        };
    }

    public async Task<AuthResponseDTO> RegistrarAsync(CriarUsuarioDTO registrarDto)
    {
        var usuarioDto = await _usuarioService.CriarUsuarioAsync(registrarDto);
        
        var token = GerarJwtToken(usuarioDto);

        return new AuthResponseDTO
        {
            Token = token,
            ExpiraEm = DateTime.UtcNow.AddHours(1),
            Usuario = usuarioDto
        };
    }

    public string GerarJwtToken(Domain.DTOs.Usuario.UsuarioDTO usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"] ?? "fallback-secret-key-minimo-32-caracteres"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Role, usuario.Tipo)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
