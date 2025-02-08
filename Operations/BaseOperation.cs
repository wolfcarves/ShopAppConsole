using System.Text.Json;
using Spectre.Console;

namespace ShopApp.Operations;

public class BaseOperation
{
    public static string Serialize<T>(T data)
    {
        return JsonSerializer.Serialize(data, new JsonSerializerOptions() { WriteIndented = true });
    }

    public void GetAllResponse<T>(string module, IEnumerable<T> data)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);

        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
            table.AddColumn(prop.Name);

        foreach (var item in data)
        {
            var values = properties.Select(p => p.GetValue(item)?.ToString() ?? "NULL").ToArray();
            table.AddRow(values);
        }

        var result = Serialize(data);

        Console.Clear();
        Console.WriteLine($"{module} result:");
        Console.WriteLine();
        AnsiConsole.Write(table);
    }

    public void GetByIdResponse<T>(string module, T data)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);

        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
            table.AddColumn(prop.Name);

        var values = properties.Select(p => p.GetValue(data)?.ToString() ?? "NULL").ToArray();
        table.AddRow(values);

        Console.Clear();
        Console.WriteLine($"{module} result:");
        Console.WriteLine();
        AnsiConsole.Write(table);
    }
}
