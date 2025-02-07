using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entities;

public class OwnerDetails
{
    [ForeignKey("Owner")]
    public int Id { get; set; }
    public string Address { get; set; } = String.Empty;
    public string Phone { get; set; } = String.Empty;
    public DateTime BirthDate { get; set; }

    public virtual Owner Owner { get; set; } = null!;
}