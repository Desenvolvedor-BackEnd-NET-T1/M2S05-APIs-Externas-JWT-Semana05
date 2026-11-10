using Newtonsoft.Json;

// Aula 3. Uma entrega da MotoByte
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

// O que enviamos para criar uma entrega
public class NovaEntrega
{
    public string Cliente { get; set; }
    public string Cep { get; set; }
    public int Pizzas { get; set; }
}
