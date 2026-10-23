namespace SistemaEleicao;

public class EleicaoManager
{
    private readonly List<Candidato> _candidatos = new();

    public IReadOnlyList<Candidato> Candidatos => _candidatos;

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
}
