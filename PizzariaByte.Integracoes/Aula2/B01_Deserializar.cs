using Newtonsoft.Json;

// Aula 2. De texto JSON para objeto C#
static class B01_Deserializar
{
    public static void Executar()
    {
        // Um JSON como os que chegam das APIs
        string json = """
            { "Sabor": "Calabresa", "Preco": 45.90,
              "Vegetariana": false }
            """;

        // Texto JSON -> objeto Pizza
        Pizza pizza =
            JsonConvert.DeserializeObject<Pizza>(json);

        Console.WriteLine($"Sabor: {pizza.Sabor}");
        Console.WriteLine($"Preço: {pizza.Preco:C}");
        Console.WriteLine($"Vegetariana? {pizza.Vegetariana}");
    }
}
