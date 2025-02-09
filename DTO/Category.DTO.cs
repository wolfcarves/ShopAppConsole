namespace ShopApp.Entities;

public class CategoryDTO : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
}