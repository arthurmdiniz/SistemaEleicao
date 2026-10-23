# SistemaEleicao

Aplicação de console para simular uma eleição: cadastro de candidatos e eleitores, votação com controle de duplicidade e apuração com total de votos, brancos, nulos e vencedor.

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download)

## Como rodar

Na raiz do repositório:

```bash
dotnet run --project SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

## Como testar

```bash
dotnet test
```

## Menu

```
1 - Cadastrar candidato   Informe nome e número. Número já existente é recusado.
2 - Cadastrar eleitor     Informe o título/CPF. Título já cadastrado é recusado.
3 - Votar                 Informe o título/CPF do eleitor e depois:
                            - o número do candidato;
                            - 'branco' para voto em branco;
                            - 'nulo' para voto nulo.
4 - Apuração              Mostra os votos de cada candidato, brancos, nulos e o vencedor.
0 - Sair
```

Regras de votação:

- Cada eleitor pode votar apenas uma vez.
- Número de candidato inexistente é contabilizado como voto nulo.
- Eleitor não cadastrado não consegue votar.
- Brancos e nulos não entram na contagem do vencedor.
- Em caso de empate, todos os candidatos empatados são exibidos.

## Observações

- Os dados ficam apenas na memória: ao sair do programa, candidatos, eleitores e votos são perdidos.
- O título/CPF é tratado como um identificador livre (sem validação de formato ou dígitos verificadores), pois a identificação é simulada.

## Estrutura

```
SistemaEleicao.Console/   Aplicação de console
  Program.cs              Menu interativo
  Candidato.cs            Modelo de candidato
  Eleitor.cs              Modelo de eleitor
  EleicaoManager.cs       Regras de cadastro, votação e apuração

SistemaEleicao.Tests/     Testes unitários (xUnit)
  CandidatoTests.cs
  EleicaoManagerTests.cs
  VotacaoTests.cs
  ApuracaoTests.cs
```
## Sobre o Autor

Ajustado e documentado por **Arthur Marques Diniz**.

*   **GitHub:** [@arthurmdiniz](https://github.com)
*   **LinkedIn:** [arthurmdiniz](https://www.linkedin.com/in/arthurmdiniz/)
*   **E-mail:** arthurmdiniz@outlook.com.br

Sinta-se à vontade para enviar *pull requests*, reportar problemas ou propor melhorias!