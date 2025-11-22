using Microsoft.EntityFrameworkCore;
using FCG.Domain.Entities;

namespace FCG.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    // Apenas as DbSets necessárias para os requisitos
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<BibliotecaJogos> BibliotecaJogos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicar configurações automaticamente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
