// Aula 2. Objetos dentro de objetos: cada { } vira uma classe
public class Pedido
{
    public int Numero { get; set; }
    public Cliente Cliente { get; set; }
    public List<ItemPedido> Itens { get; set; }
}

public class Cliente
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
}

public class ItemPedido
{
    public string Sabor { get; set; }
    public int Quantidade { get; set; }
}
