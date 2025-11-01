using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FCG.Domain.DTOs.Biblioteca;
using FCG.Domain.Interfaces;
using System.Security.Claims;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BibliotecaController : ControllerBase
{
    private readonly IBibliotecaService _bibliotecaService;

    public BibliotecaController(IBibliotecaService bibliotecaService)
    {
        _bibliotecaService = bibliotecaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMinhaBiblioteca()
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(usuarioId) || !Guid.TryParse(usuarioId, out var id))
            return Unauthorized();

        var biblioteca = await _bibliotecaService.ObterBibliotecaUsuarioAsync(id);
        return Ok(biblioteca);
    }

    [HttpPost("adquirir")]
    public async Task<IActionResult> AdquirirJogo([FromBody] AdquirirJogoDTO adquirirJogoDto)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(usuarioId) || !Guid.TryParse(usuarioId, out var id))
            return Unauthorized();

        try
        {
            var bibliotecaItem = await _bibliotecaService.AdquirirJogoAsync(id, adquirirJogoDto.JogoId);
            return Ok(bibliotecaItem);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpGet("{jogoId}")]
    public async Task<IActionResult> VerificarJogoNaBiblioteca(Guid jogoId)
    {
        var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(usuarioId) || !Guid.TryParse(usuarioId, out var id))
            return Unauthorized();

        var bibliotecaItem = await _bibliotecaService.ObterJogoNaBibliotecaAsync(id, jogoId);
        
        if (bibliotecaItem == null)
            return NotFound(new { message = "Jogo não encontrado na biblioteca" });

        return Ok(bibliotecaItem);
    }
}
