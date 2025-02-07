using ShopApp.Entities;

public class Store
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public Owner Owner { get; set; } = null!;
}