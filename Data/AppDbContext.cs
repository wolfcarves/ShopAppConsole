using System.Data.Entity;
using ShopApp.Entities;

namespace ShopApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() : base()
    {

    }

    public DbSet<Owner> Owners { get; set; }
    public DbSet<OwnerDetails> OwnerDetails { get; set; }
    public DbSet<Store> Stores { get; set; }

}