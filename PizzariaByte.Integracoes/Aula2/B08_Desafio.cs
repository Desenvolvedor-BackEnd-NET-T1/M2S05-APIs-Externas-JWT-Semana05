using System.Text;
using Newtonsoft.Json;

// Aula 2. Desafio: endereço (ViaCEP) + cotação (MotoByte)
// Rode com: dotnet run -- desafio2 (com a MotoByte rodando)
static class B08_Desafio
{
    static readonly HttpClient viaCep = new HttpClient
    {
        BaseAddress = new Uri(Apis.ViaCep)
    };

    static readonly HttpClient motoByte = new HttpClient
    {
        BaseAddress = new Uri(Apis.MotoByte)
    };

    public static async Task Executar()
    {
        await Cotar("88036000", 2);
        await Cotar("01001000", 1);
    }

    static async Task Cotar(string cep, int pizzas)
    {
        // 1. Busque o endereço no ViaCEP e transforme em Endereco.
        //    Mostre "Bairro (UF): " com Console.Write

        // 2. Monte um PedidoCotacao, serialize e envie com PostAsync
        //    para api/cotacoes da MotoByte

        // 3. Se a resposta não for sucesso, mostre "fora da área"

        // 4. Se for, transforme em Frete e mostre o valor e o prazo
    }
}
