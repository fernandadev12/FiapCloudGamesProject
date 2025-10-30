using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FiapGames.Domain.Models;
using FiapGames.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDataContext _context;
    public record RegisterModel(string Nome, string Senha, UserRole Role = UserRole.Usuario);
    public record LoginModel(string Nome, string Senha);

    public AuthController(AppDataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var identity = new Identity(model.Nome, model.Senha, model.Role);
        _context.Set<Identity>().Add(identity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(null, new { id = identity.Id }, null);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = await _context.Set<Identity>()
            .FirstOrDefaultAsync(u => u.Nome == model.Nome);

        if (user == null) return Unauthorized();

        if (!user.VerifyPassword(model.Senha)) return Unauthorized();

        user.UpdateUltimoAcesso(DateTime.UtcNow);
        await _context.SaveChangesAsync();

        var secret = _configuration["Jwt:Secret"];
        if (string.IsNullOrEmpty(secret)) return StatusCode(500, "JWT secret não configurado.");

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expiresMinutes = int.TryParse(_configuration["Jwt:ExpiresMinutes"], out var m) ? m : 60;

        var claims = new[]
        {
           new Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nome),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { message = "Autenticado" });
    }
}

