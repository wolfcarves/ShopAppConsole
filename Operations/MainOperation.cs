using ShopApp.Constants;
using ShopApp.Data;

namespace ShopApp.Operations;

public class MainOperation
{

    private readonly AppDbContext _context;
    public MainOperation(AppDbContext context) { _context = context; }

    public async void OwnerOperation(string operation)
    {
        var ownerOperation = new OwnerOperations(_context);

        switch (operation)
        {
            case OperationConstants.GetAll:
                var owners = await ownerOperation.GetAllOwners();
                Console.WriteLine($"owners : {owners}");
                break;
            case OperationConstants.Add:
                ownerOperation.AddOwner();
                break;
            default:
                break;
        }
    }

    public void StoreOperation()
    {

    }
}