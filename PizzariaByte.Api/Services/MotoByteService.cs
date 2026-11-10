using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using PizzariaByte.Api.Models;

namespace PizzariaByte.Api.Services;

// Aula 3. Cliente da transportadora MotoByte (OAuth 2.0, client credentials)
public class MotoByteService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public MotoByteService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<List<Entrega>> ListarAsync()
    {
        await AutenticarAsync();
        HttpResponseMessage resposta =
            await _http.GetAsync("api/entregas");
        resposta.EnsureSuccessStatusCode();
        string json =
            await resposta.Content.ReadAsStringAsync();
        return JsonConvert
            .DeserializeObject<List<Entrega>>(json);
    }

    public async Task<Entrega> CriarAsync(NovaEntrega nova)
    {
        await AutenticarAsync();
        StringContent corpo = new StringContent(
            JsonConvert.SerializeObject(nova),
            Encoding.UTF8, "application/json");
        HttpResponseMessage resposta =
            await _http.PostAsync("api/entregas", corpo);
        resposta.EnsureSuccessStatusCode();
        string json =
            await resposta.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Entrega>(json);
    }

    // Pede o token e coloca no cabeçalho Authorization
    private async Task AutenticarAsync()
    {
        // O segredo vem do User Secrets, nunca do código
        string id = _config["MotoByte:ClientId"];
        string segredo =
            _config["MotoByte:ClientSecret"];
        if (string.IsNullOrEmpty(segredo))
            throw new InvalidOperationException(
                "Falta o segredo no User Secrets");

        var dados = new Dictionary<string, string>
        {
            ["grant_type"] = "client_credentials",
            ["client_id"] = id,
            ["client_secret"] = segredo
        };

        FormUrlEncodedContent corpo =
            new FormUrlEncodedContent(dados);
        HttpResponseMessage resposta =
            await _http.PostAsync("oauth/token", corpo);
        resposta.EnsureSuccessStatusCode();

        string json =
            await resposta.Content.ReadAsStringAsync();
        TokenResposta token = JsonConvert
            .DeserializeObject<TokenResposta>(json);

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer", token.AccessToken);
    }
}
