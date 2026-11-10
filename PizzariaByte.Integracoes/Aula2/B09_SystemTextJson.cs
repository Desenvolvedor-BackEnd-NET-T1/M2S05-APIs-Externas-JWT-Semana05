using System.Net.Http.Json;

// Aula 2. Para conhecer: o System.Text.Json, que já vem no .NET
static class B09_SystemTextJson
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.ViaCep)
    };

    public static async Task Executar()
    {
        // GET + leitura + desserialização numa linha só
        Endereco e = await http
            .GetFromJsonAsync<Endereco>("ws/88010000/json/");

        Console.WriteLine($"{e.Logradouro}, {e.Bairro}");
    }
}
