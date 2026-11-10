using System.Text.Encodings.Web;
using System.Text.Json;

namespace PizzariaByte.MotoByte;

public record Frete(string Cep, decimal ValorFrete, int PrazoMinutos);

public record PedidoCotacao(string? Cep, int Pizzas);

public record NovaEntrega(string? Cliente, string? Cep, int Pizzas);

public record Entrega(string Codigo, string Cliente, string Cep, int Pizzas,
                      string Status, decimal ValorFrete, int PrevisaoMinutos);

// Tabela de frete: a MotoByte só entrega em Florianópolis (CEP 880xx-xxx).
public static class Tabela
{
    public static Frete? Calcular(string cep, int pizzas)
    {
        cep = cep.Replace("-", "");
        if (cep.Length != 8 || !cep.All(char.IsDigit) || !cep.StartsWith("880"))
            return null;

        (decimal valor, int prazo) = cep[..4] switch
        {
            "8801" => (5.00m, 25),
            "8803" => (8.00m, 35),
            "8804" => (10.00m, 40),
            "8805" or "8806" => (15.00m, 50),
            _ => (12.00m, 45),
        };
        int extra = Math.Max(0, pizzas - 1) * 5;
        return new Frete($"{cep[..5]}-{cep[5..]}", valor, prazo + extra);
    }
}

// "Banco" em memória das entregas, separado por cliente (o sub do token).
public class Entregas
{
    private readonly List<(string dono, Entrega entrega)> _lista = new()
    {
        ("pizzaria-byte", new Entrega("MB-1001", "Ana Souza", "88010-000", 2, "entregue", 5.00m, 30)),
        ("pizzaria-byte", new Entrega("MB-1002", "Bruno Lima", "88036-000", 1, "a_caminho", 8.00m, 35)),
    };
    private int _proximo = 1003;
    private readonly object _trava = new();

    public List<Entrega> DoCliente(string dono)
    {
        lock (_trava)
            return _lista.Where(x => x.dono == dono).Select(x => x.entrega).ToList();
    }

    public Entrega? Buscar(string dono, string codigo)
    {
        lock (_trava)
            return _lista.Where(x => x.dono == dono && x.entrega.Codigo == codigo)
                         .Select(x => x.entrega).FirstOrDefault();
    }

    public Entrega Criar(string dono, NovaEntrega nova, Frete frete)
    {
        lock (_trava)
        {
            var e = new Entrega($"MB-{_proximo++}", nova.Cliente!, frete.Cep, nova.Pizzas,
                                "aguardando_motoboy", frete.ValorFrete, frete.PrazoMinutos);
            _lista.Add((dono, e));
            return e;
        }
    }
}

// Respostas copiadas do ViaCEP (viacep.com.br) em 09/11/2026.
public static class ViaCepOffline
{
    private record Endereco(string cep, string logradouro, string complemento, string unidade,
        string bairro, string localidade, string uf, string estado, string regiao,
        string ibge, string gia, string ddd, string siafi);

    private static readonly JsonSerializerOptions Formato = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
    };

    private static Endereco Floripa(string cep, string rua, string compl, string bairro) =>
        new(cep, rua, compl, "", bairro, "Florianópolis", "SC", "Santa Catarina", "Sul",
            "4205407", "", "48", "8105");

    public static readonly Dictionary<string, string> Enderecos = new Endereco[]
    {
        new("01001-000", "Praça da Sé", "lado ímpar", "", "Sé", "São Paulo", "SP", "São Paulo",
            "Sudeste", "3550308", "1004", "11", "7107"),
        Floripa("88010-000", "Rua Felipe Schmidt", "até 348/349", "Centro"),
        Floripa("88015-200", "Avenida Rio Branco", "até 532 - lado par", "Centro"),
        Floripa("88034-001", "Rodovia Admar Gonzaga", "de 2026/2027 a 3787/3788", "Itacorubi"),
        Floripa("88036-000", "Rua Lauro Linhares", "até 559/560", "Trindade"),
        Floripa("88040-000", "Rua Deputado Antônio Edu Vieira", "até 407/408", "Saco dos Limões"),
        Floripa("88058-000", "Rodovia Armando Calil Bulos", "", "Vargem Grande"),
    }.ToDictionary(e => e.cep.Replace("-", ""), e => JsonSerializer.Serialize(e, Formato));
}
