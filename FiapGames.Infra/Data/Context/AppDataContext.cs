using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FiapGames.Infra.Data.Context
{
    public class AppDataContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder config)
        {
            config.UseSqlServer(_configuration.GetConnectionString("ConnectionString"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new UsuarioConfiguration());  //adicionar outros modelsbuilder contexts]]
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDataContext).Assembly);

        }
    }

}
