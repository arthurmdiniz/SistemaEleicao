namespace SistemaEleicao.Tests;

public class EleicaoManagerTests
{
    [Fact]
    public void CadastrarCandidato_DadosValidos_AdicionaCandidato()
    {
        var manager = new EleicaoManager();

        var candidato = manager.CadastrarCandidato("Maria Silva", 13);

        Assert.Single(manager.Candidatos);
        Assert.Same(candidato, manager.Candidatos[0]);
    }

    [Fact]
    public void CadastrarCandidato_NumeroDuplicado_LancaInvalidOperationException()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Maria Silva", 13);

        var excecao = Assert.Throws<InvalidOperationException>(
            () => manager.CadastrarCandidato("Joãozinho Souza", 13));

        Assert.Contains("13", excecao.Message);
        Assert.Single(manager.Candidatos);
    }

    [Fact]
    public void CadastrarCandidato_MesmoNomeComNumerosDiferentes_Permitido()
    {
        var manager = new EleicaoManager();

        manager.CadastrarCandidato("Maria Silva", 13);
        manager.CadastrarCandidato("Maria Silva", 14);

        Assert.Equal(2, manager.Candidatos.Count);
    }

    [Fact]
    public void CadastrarCandidato_NomeInvalido_NaoAdicionaCandidato()
    {
        var manager = new EleicaoManager();

        Assert.Throws<ArgumentException>(() => manager.CadastrarCandidato(" ", 13));

        Assert.Empty(manager.Candidatos);
    }

    [Fact]
    public void CadastrarCandidato_NumeroInvalido_NaoAdicionaCandidato()
    {
        var manager = new EleicaoManager();

        Assert.Throws<ArgumentException>(() => manager.CadastrarCandidato("Ana", 0));

        Assert.Empty(manager.Candidatos);
    }

    [Fact]
    public void BuscarCandidatoPorNumero_NumeroExistente_RetornaCandidato()
    {
        var manager = new EleicaoManager();
        var candidato = manager.CadastrarCandidato("Maria Silva", 13);

        var encontrado = manager.BuscarCandidatoPorNumero(13);

        Assert.Same(candidato, encontrado);
    }

    [Fact]
    public void BuscarCandidatoPorNumero_NumeroInexistente_RetornaNull()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Maria Silva", 13);

        var encontrado = manager.BuscarCandidatoPorNumero(99);

        Assert.Null(encontrado);
    }
}
