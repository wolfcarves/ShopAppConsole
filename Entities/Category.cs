namespace ShopApp.Entities;

public class Category : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = null!;
}