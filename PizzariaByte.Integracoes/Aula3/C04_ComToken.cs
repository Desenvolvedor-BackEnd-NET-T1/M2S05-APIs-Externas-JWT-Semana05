using System.Net.Http.Headers;
using Newtonsoft.Json;

// Aula 3. Enviar o token no cabeçalho Authorization
static class C04_ComToken
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        TokenResposta token =
            await C02_PedirToken.PedirTokenAsync(http);

        // Authorization: Bearer eyJhbGciOi...
        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer", token.AccessToken);

        HttpResponseMessage resposta =
            await http.GetAsync("api/entregas");
        Console.WriteLine($"Status: {(int)resposta.StatusCode}");

        string json =
            await resposta.Content.ReadAsStringAsync();
        List<Entrega> entregas =
            JsonConvert.DeserializeObject<List<Entrega>>(json);
        foreach (Entrega e in entregas)
            Console.WriteLine(
                $"{e.Codigo}  {e.Cliente,-10}  {e.Status}");
    }
}
