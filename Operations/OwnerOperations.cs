using System.Data.Entity;
using ShopApp.Data;
using ShopApp.Entities;

namespace ShopApp.Operations;

public class OwnerOperations
{

    private readonly AppDbContext _context;

    public OwnerOperations(AppDbContext context) { _context = context; }

    public void AddOwner()
    {

    }

    public async Task<IEnumerable<Owner>> GetAllOwners()
    {
        try
        {
            var owners = await _context.Owners.ToListAsync();
            return owners;
        }
        catch (Exception)
        {
            Console.Clear();
            Console.WriteLine("Error!");

            Environment.Exit(1);
            throw;
        }
    }
}