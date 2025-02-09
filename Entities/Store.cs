namespace ShopApp.Entities;

public class Store : BaseEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public int OwnerId { get; set; }
    public virtual Owner Owner { get; set; } = null!;
    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}