using Newtonsoft.Json;

namespace PizzariaByte.Api.Models;

// Aula 2. Os campos do ViaCEP que a pizzaria usa
public class Endereco
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }

    // No JSON, o campo se chama "localidade"
    [JsonProperty("localidade")]
    public string Cidade { get; set; }

    public string Uf { get; set; }

    // CEP que não existe: { "erro": "true" }
    public bool Erro { get; set; }
}
