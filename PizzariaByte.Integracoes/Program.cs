// Escolha o exemplo pelo nome: dotnet run -- primeiro-get
// Sem nome, mostra a lista.
// Antes de rodar os exemplos da MotoByte, deixe o projeto
// PizzariaByte.MotoByte rodando em outro terminal (dotnet run).
using System.Globalization;

Console.OutputEncoding = System.Text.Encoding.UTF8; // acentos no terminal
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR"); // R$ 45,90

var exemplos = new Dictionary<string, Func<Task>>
{
    // Aula 1: HttpClient
    ["primeiro-get"] = A01_PrimeiroGet.Executar,
    ["base-address"] = A02_BaseAddress.Executar,
    ["pratica1"] = A03_Pratica.Executar,
    ["status"] = A04_StatusCode.Executar,
    ["try-catch"] = A05_TryCatch.Executar,
    ["timeout"] = A06_Timeout.Executar,

    // Aula 2: JSON com Newtonsoft.Json
    ["deserializar"] = Rodar(B01_Deserializar.Executar),
    ["lista"] = Rodar(B02_Lista.Executar),
    ["aninhado"] = Rodar(B03_Aninhado.Executar),
    ["pratica2"] = B04_Pratica.Executar,
    ["json-property"] = B05_JsonProperty.Executar,
    ["serializar"] = Rodar(B06_Serializar.Executar),
    ["post"] = B07_Post.Executar,
    ["desafio2"] = B08_Desafio.Executar,
    ["system-text-json"] = B09_SystemTextJson.Executar,

    // Aula 3: JWT e OAuth
    ["sem-token"] = C01_SemToken.Executar,
    ["pedir-token"] = C02_PedirToken.Executar,
    ["ler-token"] = C03_LerToken.Executar,
    ["com-token"] = C04_ComToken.Executar,
    ["token-alterado"] = C05_TokenAlterado.Executar,
    ["pratica3"] = C06_Pratica.Executar,
};

if (args.Length == 0 || !exemplos.ContainsKey(args[0]))
{
    Console.WriteLine("Use: dotnet run -- <exemplo>");
    foreach (string nome in exemplos.Keys)
        Console.WriteLine("  " + nome);
    return;
}

Ferramentas.Relogio.Restart();
await exemplos[args[0]]();

// Transforma um exemplo síncrono (void) em Task, para caber no dicionário
static Func<Task> Rodar(Action exemplo)
{
    return () =>
    {
        exemplo();
        return Task.CompletedTask;
    };
}
