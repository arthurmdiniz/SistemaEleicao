namespace SistemaEleicao;

public class Eleitor
{
    public Eleitor(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título ou CPF do eleitor é obrigatório.", nameof(titulo));

        Titulo = titulo.Trim();
    }

    public string Titulo { get; }

    public bool JaVotou { get; private set; }

    internal void MarcarComoVotou() => JaVotou = true;
}
