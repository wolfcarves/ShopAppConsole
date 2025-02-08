namespace ShopApp.Constants;

public static class OperationConstants
{

    public const string GetAll = "GetAll";
    public const string GetById = "GetById";
    public const string Add = "Add";
    public const string Edit = "Edit";
    public const string Remove = "Remove";

    public static readonly string[] operations = {
        GetAll,
        GetById,
        Add,
        Edit,
        Remove
    };

}