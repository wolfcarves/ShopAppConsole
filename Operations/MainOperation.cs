using ShopApp.Constants;
using ShopApp.Data;

namespace ShopApp.Operations;

public class MainOperation
{
    private readonly AppDbContext _context;
    public MainOperation(AppDbContext context) { _context = context; }

    public async Task OwnerOperation(string operation)
    {
        var ownerOperations = new OwnerOperations(_context);

        try
        {
            switch (operation)
            {
                case OperationConstants.GetAll:
                    await ownerOperations.GetAllOwnersAsync();
                    break;
                case OperationConstants.GetById:
                    await ownerOperations.GetOwnerByIdAsync(true);
                    break;
                case OperationConstants.Add:
                    await ownerOperations.AddOwnerAsync();
                    break;
                case OperationConstants.Edit:
                    await ownerOperations.EditOwnerAsync();
                    break;
                case OperationConstants.Remove:
                    await ownerOperations.RemoveOwnerAsync();
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

    public async Task StoreOperation(string operation)
    {
        var storeOperations = new StoreOperations(_context);

        try
        {
            switch (operation)
            {
                case OperationConstants.GetAll:
                    await storeOperations.GetAllStoresAsync();
                    break;
                case OperationConstants.GetById:
                    await storeOperations.GetStoreByIdAsync(true);
                    break;
                case OperationConstants.Add:
                    await storeOperations.AddStoreAsync();
                    break;
                case OperationConstants.Edit:
                    await storeOperations.EditStoreAsync();
                    break;
                case OperationConstants.Remove:
                    await storeOperations.RemoveStoreAsync();
                    break;
                default:
                    break;
            }
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"\n{ex.Message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}\n");
        }
    }

    public async Task ProductOperation(string operation)
    {
        var productOperations = new ProductOperations(_context);

        try
        {
            switch (operation)
            {
                case OperationConstants.GetAll:
                    await productOperations.GetAllProductsAsync();
                    break;
                case OperationConstants.GetById:
                    await productOperations.GetProductByIdAsync(true);
                    break;
                case OperationConstants.Add:
                    await productOperations.AddProductAsync();
                    break;
                case OperationConstants.Edit:
                    await productOperations.EditProductAsync();
                    break;
                case OperationConstants.Remove:
                    await productOperations.RemoveProductAsync();
                    break;
                default:
                    break;
            }
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"\n{ex.Message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}\n");
        }
    }

    public async Task CategoryOperation(string operation)
    {
        var productOperations = new CategoryOperations(_context);

        try
        {
            switch (operation)
            {
                case OperationConstants.GetAll:
                    await productOperations.GetAllCategoryAsync();
                    break;
                case OperationConstants.GetById:
                    await productOperations.GetCategoryByIdAsync(true);
                    break;
                case OperationConstants.Add:
                    await productOperations.AddCategoryAsync();
                    break;
                case OperationConstants.Edit:
                    await productOperations.EditCategoryAsync();
                    break;
                case OperationConstants.Remove:
                    await productOperations.RemoveCategoryAsync();
                    break;
                default:
                    break;
            }
        }
        catch (KeyNotFoundException ex)
        {
            Console.WriteLine($"\n{ex.Message}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn error occurred: {ex.Message}\n");
        }
    }
}