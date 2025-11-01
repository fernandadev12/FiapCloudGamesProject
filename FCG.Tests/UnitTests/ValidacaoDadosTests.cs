using Xunit;

namespace FCG.Tests.UnitTests;

public class ValidacaoDadosTests
{
    [Theory]
    [InlineData("João Silva", 2, 100, true)]
    [InlineData("A", 2, 100, false)] // Muito curto
    [InlineData("", 2, 100, false)] // Vazio
    [InlineData(null, 2, 100, false)] // Nulo
    public void Validar_Nome(string nome, int min, int max, bool esperado)
    {
        // Arrange & Act
        var valido = !string.IsNullOrEmpty(nome) && 
                    nome.Length >= min && 
                    nome.Length <= max;

        // Assert
        Assert.Equal(esperado, valido);
    }

    [Fact]
    public void Preco_Deve_Ser_Positivo()
    {
        // Arrange
        var precoValido = 29.99m;
        var precoInvalido = -10.0m;

        // Act & Assert
        Assert.True(precoValido > 0);
        Assert.False(precoInvalido > 0);
    }

    [Fact]
    public void Data_Lancamento_Deve_Ser_No_Passado()
    {
        // Arrange
        var dataPassado = new System.DateTime(2023, 1, 1);
        var dataFuturo = System.DateTime.Now.AddDays(1);

        // Act & Assert
        Assert.True(dataPassado < System.DateTime.Now);
        Assert.False(dataFuturo < System.DateTime.Now);
    }
}
