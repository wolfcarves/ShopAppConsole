
using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.Entities;
using ShopApp.Operations;
using ShopApp.Prompts;

namespace ShopApp;

class Program
{
    async static Task Main()
    {
        using (var context = new AppDbContext())
        {
            var seed = new SeedOperation(context);
            await seed.SeedOnce();

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
            Console.WriteLine("\nPress enter key to continue...");

            // Ignore other keys except Enter Key
            while (Console.ReadKey(intercept: true).Key != ConsoleKey.Enter) { }
        }
    }

    async private Task Run()
    {
        var selectedModule = _prompter.SelectModulePrompt();
        var selectedOperation = _prompter.SelectOperationPrompt(selectedModule);

        var operations = new Dictionary<string, Func<string, Task>>
        {
            {EntityConstants.Owner,  _mainOperation.OwnerOperation},
            {EntityConstants.Store,  _mainOperation.StoreOperation},
            {EntityConstants.Product,  _mainOperation.ProductOperation}
        };

        if (operations.TryGetValue(selectedModule, out var operation))
            await operation(selectedOperation);
        else
            throw new Exception("Module selected is not valid");
    }
}