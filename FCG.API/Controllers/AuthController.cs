using Microsoft.AspNetCore.Mvc;
using FCG.Domain.DTOs.Usuario;
using FCG.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("registrar")]
    [AllowAnonymous]
    public async Task<IActionResult> Registrar([FromBody] CriarUsuarioDTO registrarDto)
    {
        try
        {
            var resultado = await _authService.RegistrarAsync(registrarDto);
            return Ok(resultado);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginUsuarioDTO loginDto)
    {
        try
        {
            var resultado = await _authService.LoginAsync(loginDto);
            return Ok(resultado);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetUsuarioAtual()
    {
        var usuario = new
        {
            Id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value,
            Email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value,
            Nome = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value,
            Tipo = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value
        };

        return Ok(usuario);
    }
}
