using FCG.Domain.Entities;
using FCG.Domain.Interfaces;
using FCG.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FCG.Infra.Data.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly ApplicationDbContext _context;

    public JogoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Jogo?> ObterPorIdAsync(Guid id)
    {
        return await _context.Jogos
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<IEnumerable<Jogo>> ObterTodosAsync()
    {
        return await _context.Jogos
            .OrderBy(j => j.Nome)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Jogo jogo)
    {
        await _context.Jogos.AddAsync(jogo);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Jogo jogo)
    {
        _context.Jogos.Update(jogo);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarAsync(Jogo jogo)
    {
        _context.Jogos.Remove(jogo);
        await _context.SaveChangesAsync();
    }
}
