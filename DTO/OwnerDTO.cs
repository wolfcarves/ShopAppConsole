using ShopApp.Entities;

namespace ShopApp.DTO;

public class OwnerDTO : BaseEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;
}
