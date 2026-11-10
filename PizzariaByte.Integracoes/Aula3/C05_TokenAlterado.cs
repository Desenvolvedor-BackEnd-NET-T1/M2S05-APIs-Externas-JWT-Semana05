using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// Aula 3. Ler é fácil. E alterar o token, dá?
static class C05_TokenAlterado
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        TokenResposta token =
            await C02_PedirToken.PedirTokenAsync(http);
        string[] partes = token.AccessToken.Split('.');

        // "Espertinho": o token passa a valer por 1 ano
        JObject payload = JObject.Parse(Jwt.LerParte(partes[1]));
        payload["exp"] = DateTimeOffset.UtcNow
            .AddYears(1).ToUnixTimeSeconds();
        string novoPayload = Jwt.CriarParte(
            payload.ToString(Formatting.None));

        // Mesma assinatura, payload diferente
        string alterado =
            $"{partes[0]}.{novoPayload}.{partes[2]}";

        http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", alterado);
        HttpResponseMessage resposta =
            await http.GetAsync("api/entregas");

        Console.WriteLine($"Status: {(int)resposta.StatusCode}");
        Console.WriteLine(resposta.Headers.WwwAuthenticate);
    }
}
