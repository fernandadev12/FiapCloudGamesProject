using FiapGames.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FiapGames.Infra.Data.Context
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }


        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Identity> Identity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UsuarioMap());
            // modelBuilder.ApplyConfiguration(new IdentityMap());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=FiapGamesDb;User Id=sa;Password=022017;TrustServerCertificate=True;");
            }
        }

    }
}