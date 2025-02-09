using ShopApp.Entities;

namespace ShopApp.DTO;

public class StoreDTO : BaseEntity
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Name { get; set; } = String.Empty;
}