// Aula 3. Rota protegida, sem token
static class C01_SemToken
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        HttpResponseMessage resposta =
            await http.GetAsync("api/entregas");

        int codigo = (int)resposta.StatusCode;
        Console.WriteLine($"Status: {codigo} {resposta.StatusCode}");

        // A API avisa como quer ser autenticada
        Console.WriteLine(
            $"WWW-Authenticate: {resposta.Headers.WwwAuthenticate}");
    }
}
