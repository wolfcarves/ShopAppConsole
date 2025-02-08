using System.Data.Entity;
using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.Entities;
using ShopApp.Prompts;

namespace ShopApp.Operations;

public class OwnerOperations : BaseOperation
{

    private readonly AppDbContext _context;
    private readonly BasePrompter _prompt;

    public OwnerOperations(AppDbContext context)
    {
        _context = context;
        _prompt = new BasePrompter();
    }

    public async Task GetAllOwnersAsync()
    {
        var owners = await _context.Owners
        .Select(o => new
        {
            o.Id,
            o.FirstName,
            o.LastName,
            Address = !String.IsNullOrEmpty(o.OwnerDetails.Address) ? o.OwnerDetails.Address : "NULL",
            Phone = !String.IsNullOrEmpty(o.OwnerDetails.Phone) ? o.OwnerDetails.Phone : "NULL"
        })
        .ToListAsync();

        GetAllResponse(EntityConstants.Owner, owners);
    }

    public async Task<Owner> GetOwnerByIdAsync()
    {
        Console.Clear();

        string ownerId;
        int parsedOwnerId;

        do
        {
            ownerId = _prompt.Input(new InputOptions { Title = "Enter ownerId to fetch: ", Inline = true, isRequired = true });

            if (!int.TryParse(ownerId, out parsedOwnerId))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                _prompt.Print("Please enter valid numberic id");

                Console.ResetColor();
            }

        } while (!int.TryParse(ownerId, out parsedOwnerId));

        var owner = await _context.Owners
            .Select(o => new
            {
                o.Id,
                o.FirstName,
                o.LastName,
                Address = !String.IsNullOrEmpty(o.OwnerDetails.Address) ? o.OwnerDetails.Address : "NULL",
                Phone = !String.IsNullOrEmpty(o.OwnerDetails.Phone) ? o.OwnerDetails.Phone : "NULL"
            })
            .Where(o => o.Id == parsedOwnerId)
            .FirstOrDefaultAsync();


        // var owner = await _context.Owners.Select(o => new Owner()
        // {
        //     Id = o.Id,
        //     FirstName = o.FirstName,
        //     LastName = o.LastName,

        // }).Where(ow => ow.Id == parsedOwnerId);


        if (owner == null)
            throw new KeyNotFoundException("Owner not found.");

        GetByIdResponse(EntityConstants.Owner, owner);

        return owner;
    }


    public async Task AddOwnerAsync()
    {
        Console.Clear();

        _prompt.Print($"Add {EntityConstants.Owner}");
        _prompt.Print("");

        string firstname = _prompt.Input(new InputOptions { Title = "FirstName: ", Inline = true, isRequired = true });
        string lastname = _prompt.Input(new InputOptions { Title = "Lastname: ", Inline = true, isRequired = true });
        string address = _prompt.Input(new InputOptions { Title = "Address (Optional): ", Inline = true, isRequired = false });
        string phone = null!;

        // Will also skip the phone since this is also part of owner details 
        // Will require phone if only addres was provided
        if (!string.IsNullOrEmpty(address))
            phone = _prompt.Input(new InputOptions { Title = "Phone: ", Inline = true, isRequired = true });

        var owner = new Owner()
        {
            FirstName = firstname,
            LastName = lastname,
            OwnerDetails = new OwnerDetails
            {
                Address = address ?? "",
                Phone = phone ?? ""
            }
        };

        _context.Owners.Add(owner);
        await _context.SaveChangesAsync();

        _prompt.Print($"Owner {firstname} {lastname} is created.");
    }

    public async Task EditOwnerAsync()
    {
        Console.Clear();

        _prompt.Print($"Edit {EntityConstants.Owner}");
        _prompt.Print("");

        Owner owner = await GetOwnerByIdAsync();
        _prompt.Print("");


        _prompt.Print("Update to :");

        string firstname = _prompt.Input(new InputOptions { Title = "FirstName: ", Inline = true, isRequired = true });
        string lastname = _prompt.Input(new InputOptions { Title = "Lastname: ", Inline = true, isRequired = true });
        string address = _prompt.Input(new InputOptions { Title = "Address: ", Inline = true, isRequired = false });
        string phone = _prompt.Input(new InputOptions { Title = "Phone: ", Inline = true, isRequired = false });






    }

}