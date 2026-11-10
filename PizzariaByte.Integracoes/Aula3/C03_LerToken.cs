using Newtonsoft.Json.Linq;

// Aula 3. Qualquer um consegue LER um JWT
static class C03_LerToken
{
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        TokenResposta token =
            await C02_PedirToken.PedirTokenAsync(http);

        // cabeçalho.payload.assinatura
        string[] partes = token.AccessToken.Split('.');
        Console.WriteLine($"Partes: {partes.Length}");

        string cabecalho = Jwt.LerParte(partes[0]);
        Console.WriteLine($"Cabeçalho: {cabecalho}");

        // JToken.Parse só deixa o JSON indentado
        string payload = Jwt.LerParte(partes[1]);
        Console.WriteLine("Payload:");
        Console.WriteLine(JToken.Parse(payload));

        Console.WriteLine($"Assinatura: {partes[2]}");
    }
}
