using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Repositories;

public class BibliotecaRepository : IBibliotecaRepository
{
    private readonly ApplicationDbContext _context;

    public BibliotecaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BibliotecaJogos?> ObterPorUsuarioEJogoAsync(Guid usuarioId, Guid jogoId)
    {
        return await _context.BibliotecaJogos
            .Include(b => b.Jogo)
            .FirstOrDefaultAsync(b => b.UsuarioId == usuarioId && b.JogoId == jogoId);
    }

    public async Task<IEnumerable<BibliotecaJogos>> ObterPorUsuarioAsync(Guid usuarioId)
    {
        return await _context.BibliotecaJogos
            .Include(b => b.Jogo)
            .Where(b => b.UsuarioId == usuarioId)
            .OrderByDescending(b => b.DataAquisição)
            .ToListAsync();
    }

    public async Task AdicionarAsync(BibliotecaJogos biblioteca)
    {
        await _context.BibliotecaJogos.AddAsync(biblioteca);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UsuarioTemJogoAsync(Guid usuarioId, Guid jogoId)
    {
        return await _context.BibliotecaJogos
            .AnyAsync(b => b.UsuarioId == usuarioId && b.JogoId == jogoId);
    }
}
