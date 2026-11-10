using Newtonsoft.Json;

namespace PizzariaByte.Api.Models;

// Aula 3. Uma entrega na MotoByte
public class Entrega
{
    public string Codigo { get; set; }
    public string Cliente { get; set; }
    public string Cep { get; set; }
    public int Pizzas { get; set; }
    public string Status { get; set; }

    [JsonProperty("valor_frete")]
    public decimal ValorFrete { get; set; }

    [JsonProperty("previsao_minutos")]
    public int PrevisaoMinutos { get; set; }
}

// O que a pizzaria envia para criar uma entrega
public class NovaEntrega
{
    public string Cliente { get; set; }
    public string Cep { get; set; }
    public int Pizzas { get; set; }
}

// Resposta do POST oauth/token
public class TokenResposta
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}
