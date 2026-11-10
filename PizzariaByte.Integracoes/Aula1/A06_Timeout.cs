// Aula 1. Timeout: quanto tempo esperar pela resposta
static class A06_Timeout
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte),
        Timeout = TimeSpan.FromSeconds(3) // padrão: 100 s
    };

    public static async Task Executar()
    {
        Log("Chamando a rota lenta (10 s)...");
        try
        {
            HttpResponseMessage resposta =
                await http.GetAsync("api/status/lento");
            Log($"Respondeu: {(int)resposta.StatusCode}");
        }
        catch (TaskCanceledException)
        {
            Log("Desisti: passou de 3 segundos");
        }
    }
}
