// Aula 1. EnsureSuccessStatusCode e try/catch
static class A05_TryCatch
{
    static readonly HttpClient motoByte = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    // Porta errada: como se a API estivesse fora do ar
    static readonly HttpClient foraDoAr = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5399/")
    };

    public static async Task Executar()
    {
        await Buscar(motoByte, "api/frete/88036000");
        await Buscar(motoByte, "api/frete/01001000");
        await Buscar(foraDoAr, "api/frete/88036000");
    }

    static async Task Buscar(
        HttpClient http, string caminho)
    {
        try
        {
            HttpResponseMessage resposta =
                await http.GetAsync(caminho);

            // Lança HttpRequestException se não for 2xx
            resposta.EnsureSuccessStatusCode();
            Console.WriteLine("Deu certo!");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Falhou: {ex.Message}");
        }
    }
}
