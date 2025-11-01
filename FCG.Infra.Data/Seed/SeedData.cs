using FCG.Domain.Entities;
using FCG.Domain.Enums;
using FCG.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Infra.Data.Seed;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();
        var jogoRepository = scope.ServiceProvider.GetRequiredService<IJogoRepository>();

        // Verificar se já existem usuários
        var admin = await usuarioService.ObterPorEmailAsync("admin@fcg.com");
        if (admin == null)
        {
            // Criar usuário admin
            var adminUsuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "Administrador",
                Email = "admin@fcg.com",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("Admin@1234"),
                Tipo = TipoUsuario.Administrador,
                DataCriacao = DateTime.UtcNow
            };

            var usuarioRepository = scope.ServiceProvider.GetRequiredService<IUsuarioRepository>();
            await usuarioRepository.AdicionarAsync(adminUsuario);
        }

        // Verificar se já existem jogos
        var jogos = await jogoRepository.ObterTodosAsync();
        if (!jogos.Any())
        {
            // Criar alguns jogos de exemplo
            var jogosExemplo = new List<Jogo>
            {
                new Jogo
                {
                    Id = Guid.NewGuid(),
                    Nome = "Code Master",
                    Descricao = "Aprenda programação de forma divertida resolvendo desafios",
                    Preco = 29.99m,
                    Categoria = "Educativo",
                    ImagemUrl = "https://example.com/codemaster.jpg",
                    DataLancamento = new DateTime(2023, 1, 15),
                    DataCriacao = DateTime.UtcNow
                },
                new Jogo
                {
                    Id = Guid.NewGuid(),
                    Nome = "Math Adventure",
                    Descricao = "Aventura emocionante para aprender matemática",
                    Preco = 19.99m,
                    Categoria = "Educativo",
                    ImagemUrl = "https://example.com/mathadventure.jpg",
                    DataLancamento = new DateTime(2023, 3, 20),
                    DataCriacao = DateTime.UtcNow
                },
                new Jogo
                {
                    Id = Guid.NewGuid(),
                    Nome = "History Quest",
                    Descricao = "Viaje no tempo e aprenda sobre história mundial",
                    Preco = 24.99m,
                    Categoria = "Aventura",
                    ImagemUrl = "https://example.com/historyquest.jpg",
                    DataLancamento = new DateTime(2023, 6, 10),
                    DataCriacao = DateTime.UtcNow
                }
            };

            foreach (var jogo in jogosExemplo)
            {
                await jogoRepository.AdicionarAsync(jogo);
            }
        }
    }
}
