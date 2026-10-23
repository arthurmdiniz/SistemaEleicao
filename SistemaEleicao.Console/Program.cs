using SistemaEleicao;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var eleicao = new EleicaoManager();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Sistema de Eleição ===");
    Console.WriteLine("1 - Cadastrar candidato");
    Console.WriteLine("2 - Cadastrar eleitor");
    Console.WriteLine("3 - Votar");
    Console.WriteLine("4 - Apuração");
    Console.WriteLine("0 - Sair");
    Console.Write("Opção: ");

    var opcao = Console.ReadLine();
    if (opcao is null)
        return;

    switch (opcao.Trim())
    {
        case "1": CadastrarCandidato(); break;
        case "2": CadastrarEleitor(); break;
        case "3": Votar(); break;
        case "4": Apurar(); break;
        case "0": return;
        default: Console.WriteLine("Opção inválida."); break;
    }
}

void CadastrarCandidato()
{
    var nome = LerTexto("Nome do candidato: ");
    var numeroTexto = LerTexto("Número do candidato: ");

    if (!int.TryParse(numeroTexto, out var numero))
    {
        Console.WriteLine("Número inválido.");
        return;
    }

    try
    {
        var candidato = eleicao.CadastrarCandidato(nome, numero);
        Console.WriteLine($"Candidato cadastrado: {candidato.Numero} - {candidato.Nome}");
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void CadastrarEleitor()
{
    var titulo = LerTexto("Título/CPF do eleitor: ");

    try
    {
        eleicao.CadastrarEleitor(titulo);
        Console.WriteLine("Eleitor cadastrado.");
    }
    catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void Votar()
{
    var titulo = LerTexto("Título/CPF do eleitor: ");
    var escolha = LerTexto("Número do candidato, 'branco' ou 'nulo': ");

    try
    {
        if (escolha.Equals("branco", StringComparison.OrdinalIgnoreCase))
            eleicao.VotarEmBranco(titulo);
        else if (escolha.Equals("nulo", StringComparison.OrdinalIgnoreCase))
            eleicao.VotarNulo(titulo);
        else if (int.TryParse(escolha, out var numero))
            eleicao.VotarEmCandidato(titulo, numero);
        else
        {
            Console.WriteLine("Escolha inválida.");
            return;
        }

        Console.WriteLine("Voto registrado.");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

void Apurar()
{
    Console.WriteLine();
    Console.WriteLine("=== Apuração ===");

    foreach (var candidato in eleicao.Candidatos)
        Console.WriteLine($"{candidato.Numero,-6} {candidato.Nome,-30} {candidato.Votos} voto(s)");

    Console.WriteLine($"Brancos: {eleicao.VotosBrancos}");
    Console.WriteLine($"Nulos: {eleicao.VotosNulos}");

    var maiorVotacao = eleicao.Candidatos.Count == 0 ? 0 : eleicao.Candidatos.Max(c => c.Votos);
    if (maiorVotacao == 0)
    {
        Console.WriteLine("Vencedor: nenhum candidato recebeu votos.");
        return;
    }

    var vencedores = eleicao.Candidatos.Where(c => c.Votos == maiorVotacao).ToList();
    if (vencedores.Count == 1)
        Console.WriteLine($"Vencedor: {vencedores[0].Nome} ({vencedores[0].Numero})");
    else
        Console.WriteLine($"Empate entre: {string.Join(", ", vencedores.Select(c => $"{c.Nome} ({c.Numero})"))}");
}

string LerTexto(string rotulo)
{
    Console.Write(rotulo);
    return Console.ReadLine()?.Trim() ?? "";
}
