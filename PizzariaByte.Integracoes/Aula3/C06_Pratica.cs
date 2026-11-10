using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

// Aula 3. Mão na massa: criar uma entrega na MotoByte
// Rode com: dotnet run -- pratica3 (com a MotoByte rodando)
static class C06_Pratica
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        // 1. Peça o token com C02_PedirToken.PedirTokenAsync(http)
        //    e coloque no cabeçalho Authorization (Bearer)

        // 2. Crie uma NovaEntrega (Cliente, Cep 88040000, 2 pizzas),
        //    serialize e envie com PostAsync para api/entregas.
        //    Mostre o status code (deve ser 201)

        // 3. Transforme a resposta em Entrega e mostre
        //    código, status, valor do frete e previsão

        // 4. Faça um GET em api/entregas/{codigo} e mostre o status code
    }
}
