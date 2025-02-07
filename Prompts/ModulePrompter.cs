using ShopApp.Constants;


namespace ShopApp.Prompts;

public class ModulePrompter : BasePrompter
{
    public ModulePrompter() { }

    public string SelectModulePrompt()
    {
        string[] modules = EntityConstants.entities;

        var selectedModule = PickSelection(
            "Pick a module to add or modify:",
            modules
        );

        return EntityConstants.entities[selectedModule];
    }

    public string SelectOperationPrompt(string selectedModule)
    {
        Console.Clear();

        string[] operations = OperationConstants.operations;

        var selectedOperation = PickSelection(
            $"Select operation for {selectedModule} :",
            operations
        );

        return OperationConstants.operations[selectedOperation];
    }
}