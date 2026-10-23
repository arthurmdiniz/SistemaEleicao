namespace SistemaEleicao.Tests;

public class CandidatoTests
{
    [Fact]
    public void Construtor_DadosValidos_CriaCandidato()
    {
        var candidato = new Candidato("Maria Silva", 13);

        Assert.Equal("Maria Silva", candidato.Nome);
        Assert.Equal(13, candidato.Numero);
    }

    [Fact]
    public void Construtor_NomeComEspacos_RemoveEspacosDasBordas()
    {
        var candidato = new Candidato("  Joãozinho Souza  ", 45);

        Assert.Equal("Joãozinho Souza", candidato.Nome);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Construtor_NomeVazio_LancaArgumentException(string? nome)
    {
        Assert.Throws<ArgumentException>(() => new Candidato(nome!, 10));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Construtor_NumeroNaoPositivo_LancaArgumentException(int numero)
    {
        Assert.Throws<ArgumentException>(() => new Candidato("Ana", numero));
    }
}
