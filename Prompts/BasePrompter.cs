namespace ShopApp.Prompts;

public class BasePrompter
{
    public BasePrompter() { }

    public int PickSelection(string title, string[] list)
    {
        int selectedIdx = 0;

        while (true)
        {
            Console.Clear();
            Print(title);
            Print("");

            for (int i = 0; i < list.Length; i++)
            {
                if (i == selectedIdx)
                {
                    Print($"👉 {list[i]}");
                }
                else if (i != selectedIdx)
                {
                    Print($"   {list[i]}");
                    Console.ResetColor();
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            var lastIdx = list.Length - 1;

            if (keyInfo.Key == ConsoleKey.UpArrow)
                selectedIdx = selectedIdx == 0 ? lastIdx : selectedIdx - 1;

            else if (keyInfo.Key == ConsoleKey.DownArrow)
                selectedIdx = selectedIdx == lastIdx ? 0 : selectedIdx + 1;

            else if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Spacebar)
                return selectedIdx;
        }
    }

    public object Input(InputOptions options)
    {
        string value = string.Empty;
        int intValue = 0;

        do
        {
            if (options.Inline)
                Console.Write(options.Title + " ");
            else
                Print(options.Title);

            value = Console.ReadLine()?.Trim() ?? string.Empty;

            if (options.Type == "Number" && !int.TryParse(value, out intValue))
            {
                value = string.Empty;
                Print("\nPlease enter valid numberic id\n", ConsoleColor.Red);
                Console.ResetColor();
            }

        } while (string.IsNullOrEmpty(value)
                && options.isRequired
                && (options.Type == "Number" ? !int.TryParse(value, out intValue) : false)
                );

        return options.Type == "Text" ? value : Convert.ToInt32(intValue);
    }

    public void Print(string data, ConsoleColor? color = ConsoleColor.White, bool? inline = false)
    {
        Console.ForegroundColor = color ?? ConsoleColor.White;

        if (inline ?? false) // WriteLine by default
            Console.Write(data + " ");
        else
            Console.WriteLine(data);

        Console.ResetColor();

    }
}

public class InputOptions()
{
    public string Title { get; set; } = String.Empty;
    public bool Inline { get; set; } = false;
    public bool isRequired { get; set; } = true;
    public string? Type { get; set; } = "Text";
}