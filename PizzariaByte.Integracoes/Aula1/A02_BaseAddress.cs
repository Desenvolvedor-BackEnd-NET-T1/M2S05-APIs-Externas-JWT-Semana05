// Aula 1. BaseAddress: a parte fixa do endereço fica no cliente
static class A02_BaseAddress
{
    // Um cliente só, reaproveitado
    static readonly HttpClient http = new HttpClient
    {
        BaseAddress = new Uri("https://viacep.com.br/")
    };

    public static async Task Executar()
    {
        string[] ceps = { "88010000", "88036000" };

        foreach (string cep in ceps)
        {
            // Só o final do endereço
            string caminho = $"ws/{cep}/json/";
            HttpResponseMessage resposta =
                await http.GetAsync(caminho);

            int codigo = (int)resposta.StatusCode;
            Console.WriteLine(
                $"{http.BaseAddress}{caminho}: {codigo}");
        }
    }
}
