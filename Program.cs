
using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.Operations;
using ShopApp.Prompts;

namespace ShopApp;

class Program
{
    async static Task Main()
    {
        using (var context = new AppDbContext())
        {
            var owners = context.Owners.ToList();

            var MainPrompter = new MainPrompter();
            var mainOperation = new MainOperation(context);

            var program = new Program(MainPrompter, mainOperation);

            await program.RunLoop();
        }
    }

    private readonly MainPrompter _prompter;
    private readonly MainOperation _mainOperation;

    public Program(MainPrompter prompter, MainOperation mainOperation)
    {
        _prompter = prompter;
        _mainOperation = mainOperation;
    }

    async private Task RunLoop()
    {
        while (true)
        {
            await Run();
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    async private Task Run()
    {
        var selectedModule = _prompter.SelectModulePrompt();
        var selectedOperation = _prompter.SelectOperationPrompt(selectedModule);

        var operations = new Dictionary<string, Func<string, Task>>
        {
            {EntityConstants.Owner,  _mainOperation.OwnerOperation}
        };

        if (operations.TryGetValue(selectedModule, out var operation))
            await operation(selectedOperation);
        else
            throw new Exception("Module selected is not valid");
    }
}