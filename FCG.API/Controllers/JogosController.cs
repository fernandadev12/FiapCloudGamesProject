using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FCG.Domain.DTOs.Jogo;
using FCG.Domain.Interfaces;

namespace FCG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JogosController : ControllerBase
{
    private readonly IJogoService _jogoService;

    public JogosController(IJogoService jogoService)
    {
        _jogoService = jogoService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var jogos = await _jogoService.ObterTodosJogosAsync();
        return Ok(jogos);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var jogo = await _jogoService.ObterJogoPorIdAsync(id);
            return Ok(jogo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Create([FromBody] CriarJogoDTO criarJogoDto)
    {
        try
        {
            var jogo = await _jogoService.CriarJogoAsync(criarJogoDto);
            return CreatedAtAction(nameof(GetById), new { id = jogo.Id }, jogo);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CriarJogoDTO atualizarJogoDto)
    {
        try
        {
            var jogo = await _jogoService.AtualizarJogoAsync(id, atualizarJogoDto);
            return Ok(jogo);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _jogoService.DeletarJogoAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
