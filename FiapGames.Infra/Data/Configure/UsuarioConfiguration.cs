using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TB_USUARIO");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.CreatedAt)
               .HasColumnType("datetime2")
               .HasColumnName("DT_CRIACAO")
               .IsRequired();

        builder.Property(x => x.ModifiedAt)
               .HasColumnType("datetime2")
               .HasColumnName("DT_ALTERACAO")
               .IsRequired();

        builder.Property(x => x.Nome)
               .HasColumnType("varchar(100)")
               .HasColumnName("NOME")
               .IsRequired();

        builder.Property(x => x.DataNascimento)
               .HasColumnType("datetime2")
               .HasColumnName("DT_NASCIMENTO")
               .IsRequired();

        builder.Property(x => x.Email)
               .HasColumnType("varchar(150)")
               .HasColumnName("EMAIL")
               .IsRequired();
    }
}
