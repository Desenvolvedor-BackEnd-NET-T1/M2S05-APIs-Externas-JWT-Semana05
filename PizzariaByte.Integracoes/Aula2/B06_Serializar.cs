using Newtonsoft.Json;

// Aula 2. O caminho inverso: de objeto C# para texto JSON
static class B06_Serializar
{
    public static void Executar()
    {
        Pizza pizza = new Pizza
        {
            Sabor = "Quatro Queijos",
            Preco = 52.50m,
            Vegetariana = true
        };

        // ToString não gera JSON: só o nome da classe
        Console.WriteLine(pizza.ToString());

        // Objeto -> texto JSON, numa linha só
        string json = JsonConvert.SerializeObject(pizza);
        Console.WriteLine(json);

        // Indented: com quebras de linha
        string bonito = JsonConvert.SerializeObject(
            pizza, Formatting.Indented);
        Console.WriteLine(bonito);
    }
}
