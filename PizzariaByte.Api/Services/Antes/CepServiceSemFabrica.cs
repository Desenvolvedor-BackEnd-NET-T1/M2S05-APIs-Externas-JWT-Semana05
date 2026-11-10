namespace PizzariaByte.Api.Services.Antes;

// NÃO FAÇA ASSIM (aula 1). Não está registrado no Program.cs.
// Um HttpClient novo a cada chamada: com muitos acessos, as
// conexões se acumulam e acabam (esgotamento de sockets).
public class CepServiceSemFabrica
{
    public async Task<string> BuscarJsonAsync(string cep)
    {
        // Um cliente novo a cada chamada
        using HttpClient http = new HttpClient();

        string url =
            $"https://viacep.com.br/ws/{cep}/json/";
        HttpResponseMessage resposta =
            await http.GetAsync(url);
        return await resposta.Content
            .ReadAsStringAsync();
    }
}
