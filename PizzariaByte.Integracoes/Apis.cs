// Endereços das APIs usadas na semana
static class Apis
{
    // API pública de CEP (precisa de internet).
    // Sem internet? Use a cópia que a MotoByte tem:
    // "http://localhost:5300/"
    public const string ViaCep = "https://viacep.com.br/";

    // A transportadora: rode o PizzariaByte.MotoByte
    public const string MotoByte = "http://localhost:5300/";

    // Credenciais da pizzaria na MotoByte (aula 3).
    // Só nos exemplos de console! Na API, o
    // segredo vai para o User Secrets.
    public const string ClientId = "pizzaria-byte";
    public const string ClientSecret = "mb-7f3a9c2e";
}
