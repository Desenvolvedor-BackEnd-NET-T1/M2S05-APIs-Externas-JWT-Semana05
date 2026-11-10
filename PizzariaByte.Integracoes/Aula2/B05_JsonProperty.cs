using Newtonsoft.Json;

// Aula 2. Quando o nome no JSON é diferente do nome em C#
static class B05_JsonProperty
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        HttpResponseMessage resposta =
            await http.GetAsync("api/frete/88036000");
        string json =
            await resposta.Content.ReadAsStringAsync();

        FreteSemAtributo f1 = JsonConvert
            .DeserializeObject<FreteSemAtributo>(json);
        Console.WriteLine($"Sem: {f1.ValorFrete:C}");

        Frete f2 = JsonConvert
            .DeserializeObject<Frete>(json);
        Console.WriteLine($"Com: {f2.ValorFrete:C}");
    }
}
