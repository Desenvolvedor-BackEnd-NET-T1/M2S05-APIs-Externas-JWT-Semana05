using Newtonsoft.Json;

// Aula 2. A MotoByte responde: { "cep": "88036-000",
//   "valor_frete": 8.00, "prazo_minutos": 35 }

// Sem atributos: valor_frete não combina com ValorFrete
public class FreteSemAtributo
{
    public string Cep { get; set; }
    public decimal ValorFrete { get; set; }
    public int PrazoMinutos { get; set; }
}

// [JsonProperty] diz qual campo do JSON vai em cada propriedade
public class Frete
{
    public string Cep { get; set; }

    [JsonProperty("valor_frete")]
    public decimal ValorFrete { get; set; }

    [JsonProperty("prazo_minutos")]
    public int PrazoMinutos { get; set; }
}

// O que enviamos no POST de cotação
public class PedidoCotacao
{
    public string Cep { get; set; }
    public int Pizzas { get; set; }
}
