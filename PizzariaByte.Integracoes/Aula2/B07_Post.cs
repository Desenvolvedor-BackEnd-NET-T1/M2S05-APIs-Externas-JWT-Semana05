using System.Text;
using Newtonsoft.Json;

// Aula 2. POST: enviar um objeto em JSON para a API
static class B07_Post
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        PedidoCotacao pedido = new PedidoCotacao
            { Cep = "88036000", Pizzas = 3 };

        // 1. Objeto C# -> texto JSON
        string json = JsonConvert.SerializeObject(pedido);
        Console.WriteLine($"Enviando: {json}");

        // 2. Corpo da requisição, do tipo JSON
        StringContent corpo = new StringContent(
            json, Encoding.UTF8, "application/json");

        // 3. POST para a API
        HttpResponseMessage resposta =
            await http.PostAsync("api/cotacoes", corpo);
        resposta.EnsureSuccessStatusCode();

        // 4. Texto JSON da resposta -> objeto C#
        string retorno =
            await resposta.Content.ReadAsStringAsync();
        Frete f = JsonConvert
            .DeserializeObject<Frete>(retorno);
        Console.WriteLine(
            $"{f.ValorFrete:C} em {f.PrazoMinutos} min");
    }
}
