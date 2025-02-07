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
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                return selectedIdx;
            }
        }
    }
}