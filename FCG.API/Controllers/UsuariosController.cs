using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FCG.Domain.DTOs.Usuario;
using FCG.Domain.Interfaces;
using FCG.Domain.Enums;
using System.Security.Claims;

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
            Nome = usuario.Nome,
            Email = usuario.Email,
            Tipo = usuario.Tipo.ToString(),
            DataCriacao = usuario.DataCriacao
        };

        return Ok(usuarioDto);
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> GetAll()
    {
       try
        {
            var usuarios = await _usuarioService.ObterTodosUsuariosAsync();
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Erro interno ao listar usuários", details = ex.Message });
        }
    }
}
