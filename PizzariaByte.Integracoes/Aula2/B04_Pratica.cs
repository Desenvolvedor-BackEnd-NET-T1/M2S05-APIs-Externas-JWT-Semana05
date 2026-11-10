using Newtonsoft.Json;

// Aula 2. Mão na massa: a resposta do ViaCEP vira um Endereco
// Rode com: dotnet run -- pratica2
static class B04_Pratica
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.ViaCep)
    };

    public static async Task Executar()
    {
        // 1. Na classe Endereco (pasta Modelos), crie a propriedade Ddd

        // 2. Faça o GET em ws/88036000/json/ e leia o corpo como texto

        // 3. Transforme o texto em Endereco com JsonConvert.DeserializeObject

        // 4. Mostre: Rua Lauro Linhares, Trindade - Florianópolis/SC
        //    e, na linha de baixo: DDD: 48
    }
}
