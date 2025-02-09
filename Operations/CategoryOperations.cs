using System.Data.Entity;
using AutoMapper;
using ShopApp.Configurations;
using ShopApp.Constants;
using ShopApp.Data;
using ShopApp.DTO;
using ShopApp.Entities;
using ShopApp.Prompts;

namespace ShopApp.Operations;

public class CategoryOperations : BaseOperation
{
    private readonly AppDbContext _context;
    private readonly BasePrompter _prompt;
    private readonly IMapper _mapper;

    public CategoryOperations(AppDbContext context)
    {
        _context = context;
        _prompt = new BasePrompter();
        _mapper = MapperConfig.InitializeMapper();
    }

    public async Task GetAllCategoryAsync()
    {
        var categories = await _context.Category.ToListAsync();
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

        GetAllResponse(EntityConstants.Category, categoriesDto);
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(bool clearTerminal)
    {
        if (clearTerminal)
        {
            Console.Clear();
            _prompt.Print($"Get {EntityConstants.Category}\n", ConsoleColor.Green);
        }

        int categoryId = (int)_prompt.Input(new InputOptions { Title = "Enter Category Id: ", Inline = true, isRequired = true, Type = "Number" });

        var category = await _context.Category
            .Where(o => o.Id == categoryId)
            .FirstOrDefaultAsync();

        var categoryDto = _mapper.Map<CategoryDTO>(category);

        if (category == null)
            throw new KeyNotFoundException("Category not found 😞");

        GetByIdResponse(EntityConstants.Category, categoryDto);

        return categoryDto;
    }

    public async Task AddCategoryAsync()
    {
        Console.Clear();
        _prompt.Print($"Add {EntityConstants.Category}\n", ConsoleColor.Blue);

        string categoryName = (string)_prompt.Input(new InputOptions { Title = "Name: ", Inline = true, isRequired = true });

        var category = new Category
        {
            Name = categoryName,
        };

        _context.Category.Add(category);
        await _context.SaveChangesAsync();

        _prompt.Print($"\n{categoryName} Category is created.", ConsoleColor.Green);
    }

    public async Task EditCategoryAsync()
    {
        Console.Clear();

        _prompt.Print($"Edit {EntityConstants.Category}\n", ConsoleColor.Yellow);

        CategoryDTO category = await GetCategoryByIdAsync(false);
        Category categoryToEdit = await _context.Category.FindAsync(category.Id);

        _prompt.Print("\nUpdate category 👇\n", ConsoleColor.White);

        string categoryName = (string)_prompt.Input(new InputOptions { Title = "Category Name: ", Inline = true, isRequired = true });

        categoryToEdit.Name = categoryName;

        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Category} successfully updated!", ConsoleColor.Green);
    }

    public async Task RemoveCategoryAsync()
    {
        Console.Clear();
        _prompt.Print($"Delete {EntityConstants.Category}\n", ConsoleColor.Red);

        CategoryDTO category = await GetCategoryByIdAsync(false);
        Category categoryToDelete = await _context.Category.FindAsync(category.Id);

        _context.Category.Remove(categoryToDelete);
        await _context.SaveChangesAsync();

        _prompt.Print($"\n{EntityConstants.Category} {category.Name} is successfully deleted.", ConsoleColor.Green);
    }
}