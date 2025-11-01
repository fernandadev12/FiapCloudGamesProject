using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FCG.Domain.Entities;

namespace FCG.Infra.Data.Configurations;

public class BibliotecaJogosConfiguration : IEntityTypeConfiguration<BibliotecaJogos>
{
    public void Configure(EntityTypeBuilder<BibliotecaJogos> builder)
    {
        builder.ToTable("BibliotecaJogos");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.DataAquisição)
            .IsRequired();
            
        // Relacionamentos
        builder.HasOne(b => b.Usuario)
            .WithMany(u => u.BibliotecaJogos)
            .HasForeignKey(b => b.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(b => b.Jogo)
            .WithMany(j => j.BibliotecaJogos)
            .HasForeignKey(b => b.JogoId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Garantir que um usuário não possa ter o mesmo jogo duas vezes
        builder.HasIndex(b => new { b.UsuarioId, b.JogoId })
            .IsUnique();
    }
}
