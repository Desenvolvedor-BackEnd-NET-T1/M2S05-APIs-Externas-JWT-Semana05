// Aula 1. A primeira requisição: buscar um CEP no ViaCEP
static class A01_PrimeiroGet
{
    public static async Task Executar()
    {
        // O HttpClient é quem faz as requisições HTTP
        HttpClient http = new HttpClient();

        string url = "https://viacep.com.br/ws/88010000/json/";

        // GET: "me mande os dados deste endereço"
        HttpResponseMessage resposta =
            await http.GetAsync(url);

        // O status diz se deu certo (200 = OK)
        int codigo = (int)resposta.StatusCode;
        Console.WriteLine($"Status: {codigo}");

        // O corpo da resposta, como texto
        string json =
            await resposta.Content.ReadAsStringAsync();
        Console.WriteLine(json);
    }
}
