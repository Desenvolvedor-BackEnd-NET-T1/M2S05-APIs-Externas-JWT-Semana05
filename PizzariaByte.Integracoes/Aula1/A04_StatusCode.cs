// Aula 1. Nem toda resposta é sucesso: confira o status
static class A04_StatusCode
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        await ConsultarFrete("88036000"); // Trindade
        await ConsultarFrete("01001000"); // São Paulo
    }

    static async Task ConsultarFrete(string cep)
    {
        HttpResponseMessage resposta =
            await http.GetAsync($"api/frete/{cep}");
        string json =
            await resposta.Content.ReadAsStringAsync();
        int codigo = (int)resposta.StatusCode;

        // true para os status de 200 a 299
        if (resposta.IsSuccessStatusCode)
            Console.WriteLine($"Frete: {json}");
        else
            Console.WriteLine($"Erro {codigo}: {json}");
    }
}
