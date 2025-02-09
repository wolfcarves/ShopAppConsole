using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Entities;

public class OwnerDetails
{
    [ForeignKey("Owner")]
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public string Phone { get; set; } = null!;

    public Owner Owner { get; set; } = null!;
}