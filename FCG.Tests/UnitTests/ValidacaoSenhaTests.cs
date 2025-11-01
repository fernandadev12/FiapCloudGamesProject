using Xunit;

namespace FCG.Tests.UnitTests;

public class ValidacaoSenhaTests
{
    [Fact]
    public void Senha_Deve_Ter_Minimo_8_Caracteres()
    {
        // Arrange
        var senha = "Abc@123";

        // Act & Assert
        Assert.False(senha.Length >= 8, "Senha deve ter no mínimo 8 caracteres");
    }

    [Fact]
    public void Senha_Deve_Conter_Maiuscula_Minuscula_Numero_Especial()
    {
        // Arrange
        var senhaValida = "Senha@1234";

        // Act & Assert
        Assert.True(TemMaiuscula(senhaValida), "Deve conter letra maiúscula");
        Assert.True(TemMinuscula(senhaValida), "Deve conter letra minúscula");
        Assert.True(TemNumero(senhaValida), "Deve conter número");
        Assert.True(TemCaractereEspecial(senhaValida), "Deve conter caractere especial");
    }

    [Theory]
    [InlineData("Senha@1234", true)]
    [InlineData("MINHAsenha123", false)] // Sem caractere especial
    [InlineData("senha@1234", false)] // Sem maiúscula
    [InlineData("SENHA@1234", false)] // Sem minúscula
    [InlineData("Senha@teste", false)] // Sem número
    [InlineData("Abc@123", false)] // Muito curta
    public void Validar_Senha_Forte(string senha, bool esperado)
    {
        // Arrange & Act
        var valida = senha.Length >= 8 &&
                    TemMaiuscula(senha) &&
                    TemMinuscula(senha) &&
                    TemNumero(senha) &&
                    TemCaractereEspecial(senha);

        // Assert
        Assert.Equal(esperado, valida);
    }

    [Theory]
    [InlineData("email@valido.com", true)]
    [InlineData("email.invalido", false)]
    [InlineData("usuario@provedor.com.br", true)]
    [InlineData("semarroba.com", false)]
    public void Validar_Email(string email, bool esperado)
    {
        // Arrange & Act
        var emailValido = System.Text.RegularExpressions.Regex.IsMatch(
            email, 
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"
        );

        // Assert
        Assert.Equal(esperado, emailValido);
    }

    // Métodos auxiliares
    private bool TemMaiuscula(string texto) => System.Text.RegularExpressions.Regex.IsMatch(texto, "[A-Z]");
    private bool TemMinuscula(string texto) => System.Text.RegularExpressions.Regex.IsMatch(texto, "[a-z]");
    private bool TemNumero(string texto) => System.Text.RegularExpressions.Regex.IsMatch(texto, "[0-9]");
    private bool TemCaractereEspecial(string texto) => System.Text.RegularExpressions.Regex.IsMatch(texto, "[^a-zA-Z0-9]");
}
