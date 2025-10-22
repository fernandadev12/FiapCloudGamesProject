using FiapGames.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
{
    public void Configure(EntityTypeBuilder<Identity> builder)
    {
        builder.ToTable("TB_IDENTITY");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
               .HasColumnName("TOKEN")
               .HasMaxLength(128)
               .IsRequired();

        builder.Property(x => x.Role)
               .HasConversion<int>()
               .HasColumnName("ROLE")
               .IsRequired();

        builder.Property(x => x.UltimoAcesso)
               .HasColumnType("DATETIME")
               .HasColumnName("ULTIMO_ACESSO")
               .IsRequired(false);
    }
}