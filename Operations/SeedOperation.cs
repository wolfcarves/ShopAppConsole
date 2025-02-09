using System.Data.Entity;
using ShopApp.Data;
using ShopApp.Entities;

namespace ShopApp.Operations;

class SeedOperation
{
  private readonly AppDbContext _context;
  public SeedOperation(AppDbContext context)
  {
    _context = context;
  }

  public async Task SeedOnce()
  {
    var existingOwners = await _context.Owners.ToListAsync();

    if (existingOwners.Count > 0) return;

    var owner = new Owner()
    {
      FirstName = "Rodel",
      LastName = "Crisosto"
    };

    _context.Owners.Add(owner);
    await _context.SaveChangesAsync();


    var store = new Store()
    {
      Name = "Sari Sari Store",
      OwnerId = owner.Id
    };

    _context.Stores.Add(store);
    await _context.SaveChangesAsync();

    var product = new Product()
    {
      Name = "RTX 4090ti",
      StoreId = store.Id,
      Quantity = 1,
      Price = 999999999
    };

    _context.Products.Add(product);
    await _context.SaveChangesAsync();
  }
}
