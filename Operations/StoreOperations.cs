using System.Data.Entity;
using AutoMapper;
using ShopApp.Configurations;
using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.DTO;
using ShopApp.Prompts;

namespace ShopApp.Operations;

public class StoreOperations : BaseOperation
{
    private readonly AppDbContext _context;
    private readonly BasePrompter _prompt;
    private readonly IMapper _mapper;

    public StoreOperations(AppDbContext context)
    {
        _context = context;
        _prompt = new BasePrompter();
        _mapper = MapperConfig.InitializeMapper();
    }

    public async Task GetAllStoresAsync()
    {
        var stores = await _context.Stores
            .Include(o => o.Owner)
            .ToListAsync();
        var storesDto = _mapper.Map<IEnumerable<StoreDTO>>(stores);

        GetAllResponse(EntityConstants.Store, storesDto);
    }

    public async Task<StoreDTO> GetStoreByIdAsync(bool clearTerminal)
    {
        if (clearTerminal)
        {
            Console.Clear();
            _prompt.DisplayTitle($"Get {EntityConstants.Store}\n", ConsoleColor.Green);
        }

        string storeId;
        int parsedStoreId;

        do
        {
            storeId = _prompt.Input(new InputOptions { Title = "Enter Store Id: ", Inline = true, isRequired = true });

            if (!int.TryParse(storeId, out parsedStoreId))
            {
                Console.Clear();
                _prompt.Print("Please enter valid numberic id", ConsoleColor.Red);
            }

        } while (!int.TryParse(storeId, out parsedStoreId));

        var store = await _context.Stores
            .Where(o => o.Id == parsedStoreId)
            .FirstOrDefaultAsync();

        var storeDto = _mapper.Map<StoreDTO>(store);

        if (store == null)
            throw new KeyNotFoundException("Store not found 😞");

        GetByIdResponse(EntityConstants.Store, storeDto);

        return storeDto;
    }

    public async Task AddStoreAsync()
    {
        Console.Clear();
        _prompt.DisplayTitle($"Add {EntityConstants.Store}\n", ConsoleColor.Blue);

        string storeName = _prompt.Input(new InputOptions { Title = "Name: ", Inline = true, isRequired = true });
        string ownerId = string.Empty;
        int parsedOwnerId;

        do ownerId = _prompt.Input(new InputOptions { Title = "OwnerId: ", Inline = true, isRequired = true });
        while (!int.TryParse(ownerId, out parsedOwnerId));

        var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Id == parsedOwnerId);

        if (owner == null) throw new KeyNotFoundException("Owner not found 😞");

        var store = _mapper.Map<Store>(new Store
        {
            Name = storeName,
            Owner = owner!
        });

        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        _prompt.Print($"\n{storeName} Store is created.", ConsoleColor.Green);
    }

    public async Task EditStoreAsync()
    {
        Console.Clear();

        _prompt.DisplayTitle($"Edit {EntityConstants.Store}\n", ConsoleColor.Yellow);

        StoreDTO store = await GetStoreByIdAsync(false);
        Store storeToEdit = await _context.Stores.FindAsync(store.Id);

        _prompt.Print("\nUpdate store 👇\n", ConsoleColor.White);

        string storeName = _prompt.Input(new InputOptions { Title = "Store Name: ", Inline = true, isRequired = true });

        storeToEdit.Name = storeName;

        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Store} successfully updated!", ConsoleColor.Green);
    }

    public async Task Remove()
    {
        Console.Clear();
        _prompt.DisplayTitle($"Delete {EntityConstants.Store}\n", ConsoleColor.Red);

        StoreDTO store = await GetStoreByIdAsync(false);
        Store storeToDelete = await _context.Stores.FindAsync(store.Id);

        _context.Stores.Remove(storeToDelete);
        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Store} {store.Name} is successfully deleted.", ConsoleColor.Green);
    }
}