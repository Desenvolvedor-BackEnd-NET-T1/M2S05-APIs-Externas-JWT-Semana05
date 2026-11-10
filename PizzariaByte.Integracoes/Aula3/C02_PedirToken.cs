using Newtonsoft.Json;

// Aula 3. OAuth 2.0, fluxo client credentials: pedir o token
static class C02_PedirToken
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        TokenResposta token = await PedirTokenAsync(http);

        Console.WriteLine($"Tipo: {token.TokenType}");
        Console.WriteLine($"Escopo: {token.Scope}");
        Console.WriteLine($"Vale por: {token.ExpiresIn} s");
        Console.WriteLine(
            $"Token: {token.AccessToken[..40]}...");
    }

    // Usado também nos próximos exemplos
    public static async Task<TokenResposta> PedirTokenAsync(
        HttpClient http)
    {
        // O OAuth pede os dados como formulário
        var dados = new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = Apis.ClientId,
            ["client_secret"] = Apis.ClientSecret
        };
        FormUrlEncodedContent corpo =
            new FormUrlEncodedContent(dados);

        HttpResponseMessage resposta =
            await http.PostAsync("oauth/token", corpo);
        resposta.EnsureSuccessStatusCode();

        string json =
            await resposta.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TokenResposta>(json);
    }
}
