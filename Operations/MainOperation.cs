using ShopApp.Constants;
using ShopApp.Data;

namespace ShopApp.Operations;

public class MainOperation
{
    public MainOperation() { }

    public void OwnerOperation(string operation)
    {
        var ownerOperation = new OwnerOperations();

        switch (operation)
        {
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