using System.Security.Claims;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuariosController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(usuarioId) || !Guid.TryParse(usuarioId, out var id))
            return Unauthorized();

        var usuario = await _usuarioService.ObterPorIdAsync(id);

        if (usuario == null)
            return NotFound();

        var usuarioDto = new Domain.DTOs.Usuario.UsuarioDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome ?? "",
            Email = usuario?.Email ?? "",
            Tipo = usuario?.Tipo.ToString() ?? TipoUsuario.Usuario.ToString(),
            DataCriacao = DateTime.Now
        };

        return Ok(usuarioDto);
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAll()
    {
        // Apenas admin pode listar todos os usuários
        // Implementação futura
        return Ok(new { message = "Endpoint para listar todos os usuários (apenas Admin)" });
    }
}
