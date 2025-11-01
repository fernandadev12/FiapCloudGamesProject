using Xunit;
using FCG.Domain.Enums;

namespace FCG.Tests.UnitTests;

public class EntidadesTests
{
    [Fact]
    public void TipoUsuario_Deve_Ter_Valores_Corretos()
    {
        // Arrange & Act & Assert
        Assert.Equal(1, (int)TipoUsuario.Usuario);
        Assert.Equal(2, (int)TipoUsuario.Administrador);
        Assert.Equal("Usuario", TipoUsuario.Usuario.ToString());
        Assert.Equal("Administrador", TipoUsuario.Administrador.ToString());
    }

    [Fact]
    public void Guid_Deve_Ser_Valido()
    {
        // Arrange & Act
        var guid = System.Guid.NewGuid();

        // Assert
        Assert.NotEqual(System.Guid.Empty, guid);
    }

    [Fact]
    public void DateTime_Deve_Ser_UTC()
    {
        // Arrange & Act
        var data = System.DateTime.UtcNow;

        // Assert
        Assert.Equal(System.DateTimeKind.Utc, data.Kind);
    }
}
