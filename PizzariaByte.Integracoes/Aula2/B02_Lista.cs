using Newtonsoft.Json;

// Aula 2. Um array JSON [ ] vira uma List
static class B02_Lista
{
    public static void Executar()
    {
        string json = """
            [
              { "Sabor": "Calabresa", "Preco": 45.90 },
              { "Sabor": "Margherita", "Preco": 42.00,
                "Vegetariana": true },
              { "Sabor": "Portuguesa", "Preco": 49.90 }
            ]
            """;

        List<Pizza> cardapio =
            JsonConvert.DeserializeObject<List<Pizza>>(json);

        Console.WriteLine($"{cardapio.Count} pizzas:");
        foreach (Pizza p in cardapio)
            Console.WriteLine($"  {p.Sabor,-11} {p.Preco:C}");
    }
}
