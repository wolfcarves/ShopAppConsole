namespace ShopApp.Entities;

public class Owner : BaseEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    public OwnerDetails OwnerDetails { get; set; } = null!;
    public virtual ICollection<Store> Store { get; set; } = new List<Store>();
}
