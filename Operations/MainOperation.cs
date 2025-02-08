using ShopApp.Constants;
using ShopApp.Data;

namespace ShopApp.Operations;

public class MainOperation
{
    private readonly AppDbContext _context;
    public MainOperation(AppDbContext context) { _context = context; }

    public async Task OwnerOperation(string operation)
    {
        var ownerOperation = new OwnerOperations(_context);

        try
        {
            switch (operation)
            {
                case OperationConstants.GetAll:
                    await ownerOperation.GetAllOwnersAsync();
                    break;
                case OperationConstants.GetById:
                    await ownerOperation.GetOwnerByIdAsync();
                    break;
                case OperationConstants.Add:
                    await ownerOperation.AddOwnerAsync();
                    break;
                case OperationConstants.Edit:
                    await ownerOperation.EditOwnerAsync();
                    break;
                default:
                    break;
            }
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"{ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public void StoreOperation()
    {

    }
}