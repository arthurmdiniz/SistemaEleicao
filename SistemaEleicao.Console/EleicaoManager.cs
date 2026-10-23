namespace SistemaEleicao;

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();
    private readonly List<Eleitor> _eleitores = new();

    public IReadOnlyList<Candidato> Candidatos => _candidatos;

    public IReadOnlyList<Eleitor> Eleitores => _eleitores;

    public int VotosNulos { get; private set; }

    public int VotosBrancos { get; private set; }

    public Candidato CadastrarCandidato(string nome, int numero)
    {
        if (BuscarCandidatoPorNumero(numero) is not null)
            throw new InvalidOperationException($"Já existe um candidato com o número {numero}.");

        var candidato = new Candidato(nome, numero);
        _candidatos.Add(candidato);
        return candidato;
    }

    public Candidato? BuscarCandidatoPorNumero(int numero) =>
        _candidatos.FirstOrDefault(c => c.Numero == numero);

    public Eleitor CadastrarEleitor(string titulo)
    {
        var eleitor = new Eleitor(titulo);

        if (BuscarEleitor(eleitor.Titulo) is not null)
            throw new InvalidOperationException($"Já existe um eleitor com o título {eleitor.Titulo}.");

        _eleitores.Add(eleitor);
        return eleitor;
    }

    public Eleitor? BuscarEleitor(string titulo) =>
        _eleitores.FirstOrDefault(e => e.Titulo == titulo.Trim());

    public void VotarEmCandidato(string titulo, int numero)
    {
        var eleitor = ObterEleitorApto(titulo);
        var candidato = BuscarCandidatoPorNumero(numero);

        if (candidato is null)
            VotosNulos++;
        else
            candidato.ReceberVoto();

        eleitor.MarcarComoVotou();
    }

    public void VotarEmBranco(string titulo)
    {
        var eleitor = ObterEleitorApto(titulo);

        VotosBrancos++;
        eleitor.MarcarComoVotou();
    }

    public void VotarNulo(string titulo)
    {
        var eleitor = ObterEleitorApto(titulo);

        VotosNulos++;
        eleitor.MarcarComoVotou();
    }

    private Eleitor ObterEleitorApto(string titulo)
    {
        var eleitor = BuscarEleitor(titulo)
            ?? throw new InvalidOperationException("Eleitor não cadastrado.");

        if (eleitor.JaVotou)
            throw new InvalidOperationException("Eleitor já votou.");

        return eleitor;
    }
}
