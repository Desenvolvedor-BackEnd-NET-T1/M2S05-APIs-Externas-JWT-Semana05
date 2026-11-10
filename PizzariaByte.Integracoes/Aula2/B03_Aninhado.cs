using Newtonsoft.Json;

// Aula 2. Objeto dentro de objeto e lista dentro de objeto
static class B03_Aninhado
{
    public static void Executar()
    {
        string json = """
            {
              "Numero": 1001,
              "Cliente": { "Nome": "Ana Souza",
                           "Telefone": "48 99999-0001" },
              "Itens": [
                { "Sabor": "Calabresa", "Quantidade": 2 },
                { "Sabor": "Margherita", "Quantidade": 1 }
              ]
            }
            """;

        Pedido pedido =
            JsonConvert.DeserializeObject<Pedido>(json);

        Console.WriteLine(
            $"Pedido {pedido.Numero} de {pedido.Cliente.Nome}");
        foreach (ItemPedido item in pedido.Itens)
            Console.WriteLine($"  {item.Quantidade}x {item.Sabor}");
    }
}
