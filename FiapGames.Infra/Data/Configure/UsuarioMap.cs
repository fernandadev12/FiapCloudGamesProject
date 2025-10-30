using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("TB_USUARIO");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
               .HasColumnName("ID")
               .ValueGeneratedOnAdd();

        builder.Property(u => u.Nome)
               .HasColumnName("NOME")
               .HasColumnType("VARCHAR(100)")
               .IsRequired();

        builder.Property(u => u.Email)
               .HasColumnName("EMAIL")
               .HasColumnType("VARCHAR(200)")
               .IsRequired();

        builder.Property(u => u.Senha)
               .HasColumnName("SENHA")
               .HasColumnType("VARCHAR(100)")
               .IsRequired(false); // se for opcional

        builder.Property(u => u.DataNascimento)
               .HasColumnName("DT_NASCIMENTO")
               .HasColumnType("DATETIME")
               .IsRequired();

        builder.Property(u => u.CreatedAt)
               .HasColumnName("DT_CRIACAO")
               .HasColumnType("DATETIME")
               .IsRequired();

        builder.Property(u => u.ModifiedAt)
               .HasColumnName("DT_ALTERACAO")
               .HasColumnType("DATETIME")
               .IsRequired();
    }
}