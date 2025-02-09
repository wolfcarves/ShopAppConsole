using System.Data.Entity;
using AutoMapper;
using ShopApp.Configurations;
using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.DTO;
using ShopApp.Entities;
using ShopApp.Prompts;

namespace ShopApp.Operations;

public class ProductOperations : BaseOperation
{
    private readonly AppDbContext _context;
    private readonly BasePrompter _prompt;
    private readonly IMapper _mapper;

    public ProductOperations(AppDbContext context)
    {
        _context = context;
        _prompt = new BasePrompter();
        _mapper = MapperConfig.InitializeMapper();
    }

    public async Task GetAllProductsAsync()
    {
        var products = await _context.Products
            .Include(p => p.Store)
            .ToListAsync();

        var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

        GetAllResponse(EntityConstants.Product, productsDto);
    }

    public async Task<ProductDTO> GetProductByIdAsync(bool clearTerminal)
    {
        if (clearTerminal)
        {
            Console.Clear();
            _prompt.Print($"Get {EntityConstants.Product}\n", ConsoleColor.Green);
        }

        int productId = (int)_prompt.Input(new InputOptions { Title = "Enter Product Id: ", Inline = true, isRequired = true, Type = "Number" });

        var product = await _context.Products
            .Where(o => o.Id == productId)
            .FirstOrDefaultAsync();

        var productDto = _mapper.Map<ProductDTO>(product);

        if (product == null)
            throw new KeyNotFoundException("Product not found 😞");

        GetByIdResponse(EntityConstants.Product, productDto);

        return productDto;
    }

    public async Task AddProductAsync()
    {
        Console.Clear();
        _prompt.Print($"Add {EntityConstants.Product}\n", ConsoleColor.Blue);

        int storeId = (int)_prompt.Input(new InputOptions { Title = "StoreId: ", Inline = true, isRequired = true, Type = "Number" });

        var store = await _context.Stores.FirstOrDefaultAsync(o => o.Id == storeId);

        if (store == null) throw new KeyNotFoundException("Store not found 😞");

        string productName = (string)_prompt.Input(new InputOptions { Title = "Name: ", Inline = true, isRequired = true });

        int price = (int)_prompt.Input(new InputOptions { Title = "Price: ", Inline = true, isRequired = true, Type = "Number" });

        int quantity = (int)_prompt.Input(new InputOptions { Title = "Quantity: ", Inline = true, isRequired = true, Type = "Number" });

        var product = _mapper.Map<Product>(new Product
        {
            Name = productName,
            Price = price,
            Quantity = quantity,
            Store = store,
        });

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        _prompt.Print($"\n{productName} Product is created.", ConsoleColor.Green);
    }

    public async Task EditProductAsync()
    {
        Console.Clear();

        _prompt.Print($"Edit {EntityConstants.Product}\n", ConsoleColor.Yellow);

        ProductDTO product = await GetProductByIdAsync(false);
        Product productToEdit = await _context.Products.FindAsync(product.Id);

        _prompt.Print("\nUpdate product 👇\n", ConsoleColor.White);

        string productName = (string)_prompt.Input(new InputOptions { Title = "Product Name: ", Inline = true, isRequired = true });
        int price = (int)_prompt.Input(new InputOptions { Title = "Price: ", Inline = true, isRequired = true, Type = "Number" });
        int quantity = (int)_prompt.Input(new InputOptions { Title = "Quantity: ", Inline = true, isRequired = true, Type = "Number" });

        productToEdit.Name = productName;
        productToEdit.Price = price;
        productToEdit.Quantity = quantity;

        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Product} successfully updated!", ConsoleColor.Green);
    }

    public async Task RemoveProductAsync()
    {
        Console.Clear();
        _prompt.Print($"Delete {EntityConstants.Product}\n", ConsoleColor.Red);

        ProductDTO product = await GetProductByIdAsync(false);
        Product productToDelete = await _context.Products.FindAsync(product.Id);

        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Product} {product.Name} is successfully deleted.", ConsoleColor.Green);
    }
}