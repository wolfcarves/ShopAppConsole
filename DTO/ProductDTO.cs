using ShopApp.Entities;

namespace ShopApp.DTO;

public class ProductDTO : BaseEntity
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int? CategoryId { get; set; } = null!;
    public required string Name { get; set; }
    public required int Price { get; set; }
    public required int Quantity { get; set; }
}