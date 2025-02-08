using System.Text;

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
            Console.WriteLine(title);
            Console.WriteLine("");

            for (int i = 0; i < list.Length; i++)
            {
                if (i == selectedIdx)
                {
                    Console.WriteLine($"> {list[i]}");

                }

                else if (i != selectedIdx)
                {
                    Console.WriteLine($"  {list[i]}");
                    Console.ResetColor();
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            var lastIdx = list.Length - 1;

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedIdx = selectedIdx == 0 ? lastIdx : selectedIdx - 1;
            }

            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedIdx = selectedIdx == lastIdx ? 0 : selectedIdx + 1;
            }
            else if (keyInfo.Key == ConsoleKey.Enter || keyInfo.Key == ConsoleKey.Spacebar)
            {
                return selectedIdx;
            }
        }
    }

    public string Input(InputOptions options)
    {
        string value = String.Empty;

        do
        {
            if (options.Inline)
                Console.Write(options.Title + " ");
            else
                Console.WriteLine(options.Title);

            value = Console.ReadLine()?.Trim();

        } while (string.IsNullOrEmpty(value) && options.isRequired);

        return value;
    }

    public void Print(string data, bool? inline = false)
    {
        if (inline ?? false) // WriteLine by default
            Console.Write(data + " ");
        else
            Console.WriteLine(data);
    }
}

public class InputOptions()
{
    public string Title { get; set; }
    public bool Inline { get; set; } = false;
    public bool isRequired { get; set; } = true;

}