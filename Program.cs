using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.Operations;
using ShopApp.Prompts;

namespace ShopApp;

class Program
{
    static void Main()
    {
        using (var context = new AppDbContext())
        {
            var modulePrompter = new ModulePrompter();
            var mainOperation = new MainOperation(context);

            var program = new Program(modulePrompter, mainOperation);
            program.Run();
        }
    }

    private readonly ModulePrompter _prompter;
    private readonly MainOperation _mainOperation;

    public Program(ModulePrompter prompter, MainOperation mainOperation)
    {
        _prompter = prompter;
        _mainOperation = mainOperation;
    }

    private void Run()
    {
        var selectedModule = _prompter.SelectModulePrompt();
        var selectedOperation = _prompter.SelectOperationPrompt(selectedModule);

        var operations = new Dictionary<string, Action<string>>
        {
            {EntityConstants.Owner, _mainOperation.OwnerOperation}
        };

        if (operations.TryGetValue(selectedModule, out var operation))
            operation(selectedOperation);
        else
            throw new Exception("Module selected is not valid");
    }
}