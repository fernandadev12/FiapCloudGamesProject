using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FCG.Domain.Entities;
using FCG.Domain.Enums;

namespace FCG.Infra.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        
        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);
            
        builder.HasIndex(u => u.Email)
            .IsUnique();
            
        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(255);
            
        builder.Property(u => u.Tipo)
            .IsRequired()
            .HasConversion<string>()
            .HasDefaultValue(TipoUsuario.Usuario);
            
        builder.Property(u => u.DataCriacao)
            .IsRequired();
    }
}
