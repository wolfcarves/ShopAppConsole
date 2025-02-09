namespace ShopApp.Entities;

public class Product : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public required int Quantity { get; set; }

    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;
}