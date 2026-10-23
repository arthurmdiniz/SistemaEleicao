namespace SistemaEleicao.Tests;

public class ApuracaoTests
{
    [Fact]
    public void ObterVencedores_SemCandidatos_RetornaVazio()
    {
        var manager = new EleicaoManager();

        Assert.Empty(manager.ObterVencedores());
    }

    [Fact]
    public void ObterVencedores_SemVotosEmCandidatos_RetornaVazio()
    {
        var manager = new EleicaoManager();
        manager.CadastrarCandidato("Maria Silva", 13);
        VotarEmBranco(manager, 3);
        VotarNulo(manager, 2);

        Assert.Empty(manager.ObterVencedores());
    }

    [Fact]
    public void ObterVencedores_CandidatoComMaisVotos_RetornaSomenteEsseCandidato()
    {
        var manager = new EleicaoManager();
        var maria = manager.CadastrarCandidato("Maria Silva", 13);
        var joao = manager.CadastrarCandidato("João Souza", 45);
        VotarEmCandidato(manager, 13, 3);
        VotarEmCandidato(manager, 45, 1);

        var vencedores = manager.ObterVencedores();

        Assert.Single(vencedores);
        Assert.Same(maria, vencedores[0]);
        Assert.DoesNotContain(joao, vencedores);
    }

    [Fact]
    public void ObterVencedores_Empate_RetornaTodosEmpatados()
    {
        var manager = new EleicaoManager();
        var maria = manager.CadastrarCandidato("Maria Silva", 13);
        var joao = manager.CadastrarCandidato("João Souza", 45);
        manager.CadastrarCandidato("Ana Lima", 22);
        VotarEmCandidato(manager, 13, 2);
        VotarEmCandidato(manager, 45, 2);
        VotarEmCandidato(manager, 22, 1);

        var vencedores = manager.ObterVencedores();

        Assert.Equal(2, vencedores.Count);
        Assert.Contains(maria, vencedores);
        Assert.Contains(joao, vencedores);
    }

    [Fact]
    public void ObterVencedores_BrancosENulosNaoContamParaVencedor()
    {
        var manager = new EleicaoManager();
        var maria = manager.CadastrarCandidato("Maria Silva", 13);
        VotarEmCandidato(manager, 13, 1);
        VotarEmBranco(manager, 5);
        VotarNulo(manager, 5);

        var vencedores = manager.ObterVencedores();

        Assert.Single(vencedores);
        Assert.Same(maria, vencedores[0]);
    }

    private static void VotarEmCandidato(EleicaoManager manager, int numero, int quantidade)
    {
        for (var i = 0; i < quantidade; i++)
            manager.VotarEmCandidato(NovoEleitor(manager), numero);
    }

    private static void VotarEmBranco(EleicaoManager manager, int quantidade)
    {
        for (var i = 0; i < quantidade; i++)
            manager.VotarEmBranco(NovoEleitor(manager));
    }

    private static void VotarNulo(EleicaoManager manager, int quantidade)
    {
        for (var i = 0; i < quantidade; i++)
            manager.VotarNulo(NovoEleitor(manager));
    }

    private static string NovoEleitor(EleicaoManager manager)
    {
        var titulo = Guid.NewGuid().ToString();
        manager.CadastrarEleitor(titulo);
        return titulo;
    }
}
