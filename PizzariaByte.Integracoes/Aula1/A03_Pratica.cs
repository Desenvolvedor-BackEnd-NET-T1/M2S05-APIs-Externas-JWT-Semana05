// Aula 1. Mão na massa: buscar o CEP que o usuário digitar
// Rode com: dotnet run -- pratica1
static class A03_Pratica
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.ViaCep)
    };

    public static async Task Executar()
    {
        // 1. Peça o CEP ao usuário (Console.Write e Console.ReadLine)

        // 2. Faça o GET em ws/{cep}/json/ com o http acima

        // 3. Mostre o status code (como número)

        // 4. Leia o corpo com ReadAsStringAsync e mostre na tela
    }
}
