namespace SistemaEleicao.Tests;

public class VotacaoTests
{
    [Fact]
    public void CadastrarEleitor_DadosValidos_AdicionaEleitor()
    {
        var manager = new EleicaoManager();

        var eleitor = manager.CadastrarEleitor("123456");

        Assert.Single(manager.Eleitores);
        Assert.Same(eleitor, manager.BuscarEleitor("123456"));
        Assert.False(eleitor.JaVotou);
    }

    [Fact]
    public void CadastrarEleitor_TituloDuplicado_LancaInvalidOperationException()
    {
        var manager = new EleicaoManager();
        manager.CadastrarEleitor("123456");

        Assert.Throws<InvalidOperationException>(() => manager.CadastrarEleitor(" 123456 "));

        Assert.Single(manager.Eleitores);
    }

    [Fact]
    public void CadastrarEleitor_TituloVazio_LancaArgumentException()
    {
        var manager = new EleicaoManager();

        Assert.Throws<ArgumentException>(() => manager.CadastrarEleitor("  "));

        Assert.Empty(manager.Eleitores);
    }

    [Fact]
    public void VotarEmCandidato_EleitorApto_ContabilizaVotoEMarcaEleitor()
    {
        var manager = new EleicaoManager();
        var candidato = manager.CadastrarCandidato("Maria Silva", 13);
        manager.CadastrarEleitor("123456");

        manager.VotarEmCandidato("123456", 13);

        Assert.Equal(1, candidato.Votos);
        Assert.Equal(0, manager.VotosNulos);
        Assert.Equal(0, manager.VotosBrancos);
        Assert.True(manager.BuscarEleitor("123456")!.JaVotou);
    }

    [Fact]
    public void VotarEmCandidato_EleitorJaVotou_LancaEExcecaoNaoContabilizaVoto()
    {
        var manager = new EleicaoManager();
        var candidato = manager.CadastrarCandidato("Maria Silva", 13);
        manager.CadastrarEleitor("123456");
        manager.VotarEmCandidato("123456", 13);

        Assert.Throws<InvalidOperationException>(() => manager.VotarEmCandidato("123456", 13));

        Assert.Equal(1, candidato.Votos);
    }

    [Fact]
    public void VotarEmBranco_EleitorJaVotouEmCandidato_LancaEExcecao()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Maria Silva", 13);
        manager.CadastrarEleitor("123456");
        manager.VotarEmCandidato("123456", 13);

        Assert.Throws<InvalidOperationException>(() => manager.VotarEmBranco("123456"));

        Assert.Equal(0, manager.VotosBrancos);
    }

    [Fact]
    public void VotarNulo_EleitorJaVotouEmBranco_LancaEExcecao()
    {
        var manager = new EleicaoManager();
        manager.CadastrarEleitor("123456");
        manager.VotarEmBranco("123456");

        Assert.Throws<InvalidOperationException>(() => manager.VotarNulo("123456"));

        Assert.Equal(0, manager.VotosNulos);
    }

    [Fact]
    public void VotarEmCandidato_NumeroInexistente_ContabilizaComoNulo()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Maria Silva", 13);
        manager.CadastrarEleitor("123456");

        manager.VotarEmCandidato("123456", 99);

        Assert.Equal(1, manager.VotosNulos);
        Assert.Equal(0, manager.Candidatos[0].Votos);
        Assert.True(manager.BuscarEleitor("123456")!.JaVotou);
    }

    [Fact]
    public void VotarEmBranco_EleitorApto_ContabilizaBrancoEMarcaEleitor()
    {
        var manager = new EleicaoManager();
        manager.CadastrarEleitor("123456");

        manager.VotarEmBranco("123456");

        Assert.Equal(1, manager.VotosBrancos);
        Assert.True(manager.BuscarEleitor("123456")!.JaVotou);
    }

    [Fact]
    public void VotarNulo_EleitorApto_ContabilizaNuloEMarcaEleitor()
    {
        var manager = new EleicaoManager();
        manager.CadastrarEleitor("123456");

        manager.VotarNulo("123456");

        Assert.Equal(1, manager.VotosNulos);
        Assert.True(manager.BuscarEleitor("123456")!.JaVotou);
    }

    [Fact]
    public void Votar_EleitorNaoCadastrado_LancaEExcecaoNaoContabilizaVoto()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Maria Silva", 13);

        Assert.Throws<InvalidOperationException>(() => manager.VotarEmCandidato("999999", 13));
        Assert.Throws<InvalidOperationException>(() => manager.VotarEmBranco("999999"));
        Assert.Throws<InvalidOperationException>(() => manager.VotarNulo("999999"));

        Assert.Equal(0, manager.Candidatos[0].Votos);
        Assert.Equal(0, manager.VotosBrancos);
        Assert.Equal(0, manager.VotosNulos);
    }
}
