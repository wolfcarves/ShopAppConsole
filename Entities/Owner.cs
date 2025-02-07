namespace ShopApp.Entities;

public class Owner
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;

    public virtual OwnerDetails OwnerDetails { get; set; } = null!;
    public ICollection<Store> Store { get; set; } = null!;
}
