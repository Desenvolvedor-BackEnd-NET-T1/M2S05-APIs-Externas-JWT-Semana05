using Newtonsoft.Json;
using PizzariaByte.Api.Models;

namespace PizzariaByte.Api.Services;

public class CepService
{
    private readonly HttpClient _http;

    // Chega pronto, por injeção de dependência
    public CepService(HttpClient http)
    {
        _http = http;
    }

    // Aula 1. Devolve o JSON do ViaCEP como texto
    public async Task<string> BuscarJsonAsync(string cep)
    {
        HttpResponseMessage resposta =
            await _http.GetAsync($"ws/{cep}/json/");
        resposta.EnsureSuccessStatusCode();
        return await resposta.Content.ReadAsStringAsync();
    }

    // Aula 2. Devolve um Endereco, ou null se o CEP não existir
    public async Task<Endereco> BuscarAsync(string cep)
    {
        HttpResponseMessage resposta =
            await _http.GetAsync($"ws/{cep}/json/");
        if (!resposta.IsSuccessStatusCode)
            return null; // CEP mal digitado: 400

        string json =
            await resposta.Content.ReadAsStringAsync();
        Endereco endereco = JsonConvert
            .DeserializeObject<Endereco>(json);

        if (endereco.Erro)
            return null; // CEP que não existe
        return endereco;
    }
}
