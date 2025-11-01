using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FCG.Domain.Entities;

namespace FCG.Infra.Data.Configurations;

public class JogoConfiguration : IEntityTypeConfiguration<Jogo>
{
    public void Configure(EntityTypeBuilder<Jogo> builder)
    {
        builder.ToTable("Jogos");
        
        builder.HasKey(j => j.Id);
        
        builder.Property(j => j.Nome)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(j => j.Descricao)
            .IsRequired()
            .HasMaxLength(1000);
            
        builder.Property(j => j.Preco)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
            
        builder.Property(j => j.Categoria)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(j => j.ImagemUrl)
            .HasMaxLength(500);
            
        builder.Property(j => j.DataLancamento)
            .IsRequired();
            
        builder.Property(j => j.DataCriacao)
            .IsRequired();
    }
}
