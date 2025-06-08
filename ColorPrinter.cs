using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISUtime
{
    public static class ColorPrinter
    {
        private static readonly Dictionary<string, ConsoleColor> ColorMap = new Dictionary<string, ConsoleColor>(StringComparer.OrdinalIgnoreCase)
        {
            ["black"] = ConsoleColor.Black,
            ["blue"] = ConsoleColor.Blue,
            ["cyan"] = ConsoleColor.Cyan,
            ["gray"] = ConsoleColor.Gray,
            ["green"] = ConsoleColor.Green,
            ["magenta"] = ConsoleColor.Magenta,
            ["red"] = ConsoleColor.Red,
            ["white"] = ConsoleColor.White,
            ["yellow"] = ConsoleColor.Yellow,
            ["darkblue"] = ConsoleColor.DarkBlue,
            ["darkcyan"] = ConsoleColor.DarkCyan,
            ["darkgray"] = ConsoleColor.DarkGray,
            ["darkgreen"] = ConsoleColor.DarkGreen,
            ["darkmagenta"] = ConsoleColor.DarkMagenta,
            ["darkred"] = ConsoleColor.DarkRed,
            ["darkyellow"] = ConsoleColor.DarkYellow
        };

        public static void PrintColored(string input)
        {
            var defaultColor = ConsoleColor.White;
            var currentColor = defaultColor;
            Console.ForegroundColor = currentColor;

            var parts = input.Split(new[] { '&' }, StringSplitOptions.None);

            // First part has no preceding color code
            if (!string.IsNullOrWhiteSpace(parts[0]))
            {
                Console.Write(parts[0]);
            }

            for (int i = 1; i < parts.Length; i++)
            {
                int spaceIndex = parts[i].IndexOf(' ');
                if (spaceIndex > 0)
                {
                    string colorCode = parts[i].Substring(0, spaceIndex);
                    string text = parts[i].Substring(spaceIndex + 1);

                    if (ColorMap.TryGetValue(colorCode, out var newColor))
                    {
                        currentColor = newColor;
                    }
                    else
                    {
                        currentColor = defaultColor; // fallback
                    }

                    Console.ForegroundColor = currentColor;
                    Console.Write(text);
                }
                else
                {
                    // No space found, treat the whole thing as plain text
                    Console.ForegroundColor = currentColor;
                    Console.Write("&" + parts[i]);
                }
            }

            Console.ForegroundColor = defaultColor; // reset to default
            Console.WriteLine();
        }
    }

}
