using FiapGames.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class IdentityMap : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.ToTable("TB_IDENTITY");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.UsuarioId)
               .HasColumnName("ID_USUARIO")
               .IsRequired();

        builder.HasOne(x => x.Usuario)
               .WithMany() // ou .WithMany(u => u.Identities) se existir
               .HasForeignKey(x => x.UsuarioId)
               .HasConstraintName("FK_Identity_Usuario")
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Token)
               .HasColumnName("TOKEN")
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(x => x.Role)
               .HasConversion<int>() // só se Role for enum
               .HasColumnName("ROLE")
               .IsRequired();

        builder.Property(x => x.UltimoAcesso)
               .HasColumnType("DATETIME")
               .HasColumnName("ULTIMO_ACESSO")
               .IsRequired();
    }
}
