using System.Diagnostics;
using System.Text;

// Ferramentas usadas pelos exemplos da semana.
static class Ferramentas
{
    // Cronômetro: o tempo desde o início do exemplo
    public static readonly Stopwatch Relogio = new Stopwatch();

    // Escreve assim: 3,0s texto
    public static void Log(string texto)
    {
        double s = Relogio.Elapsed.TotalSeconds;
        Console.WriteLine($"{s:0.0}s {texto}");
    }
}

// Aula 3. Um JWT tem três partes em Base64Url separadas por ponto.
static class Jwt
{
    // Base64Url -> texto. Não precisa de senha nenhuma!
    public static string LerParte(string parte)
    {
        string b64 = parte.Replace('-', '+').Replace('_', '/');
        b64 = b64.PadRight(b64.Length + (4 - b64.Length % 4) % 4, '=');
        byte[] bytes = Convert.FromBase64String(b64);
        return Encoding.UTF8.GetString(bytes);
    }

    // Texto -> Base64Url (usado para "falsificar" o token)
    public static string CriarParte(string texto)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(texto);
        return Convert.ToBase64String(bytes)
            .TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }
}
