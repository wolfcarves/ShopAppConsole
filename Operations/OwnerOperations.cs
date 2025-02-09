using System.Data.Entity;
using AutoMapper;
using ShopApp.Configurations;
using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.DTO;
using ShopApp.Entities;
using ShopApp.Prompts;

namespace ShopApp.Operations;

public class OwnerOperations : BaseOperation
{
    private readonly AppDbContext _context;
    private readonly BasePrompter _prompt;
    private readonly IMapper _mapper;

    public OwnerOperations(AppDbContext context)
    {
        _context = context;
        _prompt = new BasePrompter();
        _mapper = MapperConfig.InitializeMapper();
    }

    public async Task GetAllOwnersAsync()
    {
        var owners = await _context.Owners
            .Include(o => o.OwnerDetails)
            .ToListAsync();

        var ownersDto = _mapper.Map<IEnumerable<OwnerDTO>>(owners);

        GetAllResponse(EntityConstants.Owner, ownersDto);
    }

    public async Task<OwnerDTO> GetOwnerByIdAsync(bool clearTerminal)
    {
        if (clearTerminal)
        {
            Console.Clear();
            _prompt.DisplayTitle($"Get {EntityConstants.Owner}\n", ConsoleColor.Green);
        }

        int ownerId = (int)_prompt.Input(new InputOptions { Title = "Enter Owner Id: ", Inline = true, isRequired = true, Type = "Number" });

        var owner = await _context.Owners
            .Where(o => o.Id == ownerId)
            .FirstOrDefaultAsync();

        var ownerDto = _mapper.Map<OwnerDTO>(owner);

        if (owner == null)
            throw new KeyNotFoundException("\nOwner not found 😞");

        GetByIdResponse(EntityConstants.Owner, ownerDto);

        return ownerDto;
    }


    public async Task AddOwnerAsync()
    {
        Console.Clear();
        _prompt.DisplayTitle($"Add {EntityConstants.Owner}\n", ConsoleColor.Blue);

        string firstname = (string)_prompt.Input(new InputOptions { Title = "FirstName: ", Inline = true, isRequired = true });
        string lastname = (string)_prompt.Input(new InputOptions { Title = "Lastname: ", Inline = true, isRequired = true });

        string address;
        string phone = null!;

        address = (string)_prompt.Input(new InputOptions { Title = "Address (Optional): ", Inline = true, isRequired = false });
        address = !string.IsNullOrEmpty(address) ? address : null!;

        // Will also skip the phone since this is also part of owner details 
        // Will require phone if only address was provided
        if (!string.IsNullOrEmpty(address))
            phone = (string)_prompt.Input(new InputOptions { Title = "Phone: ", Inline = true, isRequired = true });

        var owner = _mapper.Map<Owner>(new Owner
        {
            FirstName = firstname,
            LastName = lastname,
            OwnerDetails = new OwnerDetails
            {
                Address = address,
                Phone = phone
            }
        });

        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();

        _prompt.Print($"Owner {firstname} {lastname} is created.");
    }

    public async Task EditOwnerAsync()
    {
        Console.Clear();

        _prompt.DisplayTitle($"Edit {EntityConstants.Owner}\n", ConsoleColor.Yellow);

        OwnerDTO owner = await GetOwnerByIdAsync(false);
        Owner ownerToEdit = await _context.Owners.FindAsync(owner.Id);

        _prompt.Print("\nUpdate owner 👇\n", ConsoleColor.White);

        string firstname = (string)_prompt.Input(new InputOptions { Title = "FirstName: ", Inline = true, isRequired = true });
        string lastname = (string)_prompt.Input(new InputOptions { Title = "Lastname: ", Inline = true, isRequired = true });
        string address = (string)_prompt.Input(new InputOptions { Title = "Address (Optional): ", Inline = true, isRequired = false });
        string phone = (string)_prompt.Input(new InputOptions { Title = "Phone (Optional): ", Inline = true, isRequired = false });

        ownerToEdit.FirstName = firstname;
        ownerToEdit.LastName = lastname;
        ownerToEdit.OwnerDetails.Address = !string.IsNullOrEmpty(address) ? address : null!;
        ownerToEdit.OwnerDetails.Phone = !string.IsNullOrEmpty(phone) ? phone : null!;

        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Owner} successfully updated!", ConsoleColor.Green);
    }

    public async Task Remove()
    {
        Console.Clear();
        _prompt.DisplayTitle($"Delete {EntityConstants.Owner}\n", ConsoleColor.Red);

        OwnerDTO owner = await GetOwnerByIdAsync(false);
        Owner ownerToDelete = await _context.Owners.FindAsync(owner.Id);

        _context.Owners.Remove(ownerToDelete);
        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Owner} {owner.FirstName} {owner.LastName} is successfully deleted.", ConsoleColor.Green);
    }
}