using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2_EducationProgramClient.Models.UI
{
    public static class ConsolePrintHelper
    {
        

        public static object GetMenuColor()
        {
            return ConsoleColor.Gray;
        }

        // Pauses the console until a key is pressed
        public static void Pause()
        {
            Console.Write("\n Press any key to continue...");
            Console.ReadKey();
        }
        // Informs the user of a faulty menu choice
        public static void FaultyMenuChoice()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid choice. Try again! ");
            Console.ResetColor();
            Console.ReadKey();
        }
        // Print out information to the user
        public static void PrintInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        // Print out a succesful desposit/transfer/etc
        public static void PrintSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        // Print out minor warning to the user
        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        // Print out a error warning when something is wrong to the user
        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }
        // Mask the password with ***
        public static string ReadPasswordMasked(char mask = '*')
        {
            var sb = new StringBuilder();
            ConsoleKeyInfo key;

            while (true)
            {
                // Hides input
                key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        // remove last char from buffer
                        sb.Length--;

                        // move cursor back, overwrite with space, move back again
                        Console.Write("\b \b");
                    }
                    continue;
                }

                // optionally ignore other control keys (e.g., arrows)
                if (char.IsControl(key.KeyChar)) continue;

                // append to buffer and show mask
                sb.Append(key.KeyChar);
                Console.Write(mask);
            }

            return sb.ToString();
        }

        // Admin title
        public static void AdminTitle(string title)
        {
            Console.ForegroundColor = (ConsoleColor)GetMenuColor();

            string border = new string('═', 100);
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║{title.ToUpper().PadLeft((100 + title.Length) / 2).PadRight(100)}║");
            Console.WriteLine($"╚{border}╝");

            //Console.ResetColor();
        }
        // Prints a formatted header box in the console
        public static void AdminHeader(string headerName)
        {
            Console.ForegroundColor = (ConsoleColor)GetMenuColor();

            int width = 125;
            string border = new string('═', 100);
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║ {headerName.ToUpper().PadRight(width - 1)}║");
            Console.WriteLine($"╚{border}╝");

            //Console.ResetColor();
        }
        // Prints a formatted menu box in the console
        public static void AdminMenu(string menuName, List<string> userMenu)
        {
            Console.ForegroundColor = (ConsoleColor)GetMenuColor();

            int width = 100;
            string border = new string('═', 100);
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║ {menuName.ToUpper().PadRight(width - 1)}║");
            Console.WriteLine($"║{border}║");
            int nr = 1;
            foreach (var selection in userMenu)
            {
                string line = $"║[{nr++}] {selection}";
                int padding = width - (line.Length - 1); // minus 1 because '║' at start counts once
                if (padding < 0) padding = 0; // prevent negative padding if text is too long

                Console.WriteLine(line + new string(' ', padding) + "║");
            }

            string quitLine = "║[0] Exit";
            int quitPadding = width - (quitLine.Length - 1);
            if (quitPadding < 0) quitPadding = 0;
            Console.WriteLine(quitLine + new string(' ', quitPadding) + "║");

            Console.WriteLine($"╚{border}╝");
            //Console.ResetColor();
        }

        // Prints a formatted list box in the console
        public static void AdminList(string listName, List<string> listToPrint)
        {
            Console.ForegroundColor = (ConsoleColor)GetMenuColor();

            int width = 100;
            string border = new string('═', 100);

            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║ {listName.ToUpper().PadRight(width - 1)}║");
            Console.WriteLine($"╚{border}╝");
            foreach (var listItem in listToPrint)
            {
                Console.WriteLine($" {listItem}");
            }
            Console.WriteLine($" {border} ");
        }
        public static void AdminSubTitle(string title)
        {
            int width = 100;
            string border = new string('═', 100);

            Console.ForegroundColor = (ConsoleColor)GetMenuColor();
            Console.WriteLine($"╔{border}╗");
            Console.WriteLine($"║ {title.ToUpper().PadRight(width - 1)}║");
            Console.WriteLine($"╚{border}╝");
            Console.ResetColor();
        }

        public static string? AdminAskChoice(string question)
        {
            Console.ForegroundColor = (ConsoleColor)GetMenuColor();
            string border = new string('═', 102);
            Console.WriteLine($"{border}");
            Console.Write($" {question} ");
            string? choice = Console.ReadLine();

            //Console.ResetColor();

            if (string.IsNullOrWhiteSpace(choice))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Choice cannot be empty.");
                Console.ResetColor();
                return null;
            }
            else
            {
                return choice.Trim();
            }
        }

        public static bool NullInputWarning(string inString)
        {
            if (string.IsNullOrWhiteSpace(inString))
            {
                ConsolePrintHelper.PrintError("Input cannot be blank.");
                ConsolePrintHelper.Pause();
                return true;
            }
            return false;
        }

        public static void Banner()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.ForegroundColor = (ConsoleColor)GetMenuColor();

            int width = Math.Min(Console.LargestWindowWidth, 120);
            Console.WindowWidth = width;

            string[] banner = new[]
            {
                "╔" + new string('═', width - 2) + "╗",

                CenterLine("▄▄▄      ▄▄▄▄▄ ▄▄▄▄▄▄▄   ▄▄▄▄▄▄▄     ▄▄▄▄   ▄▄▄▄▄▄▄   ▄▄▄   ▄▄▄ ", width),
                CenterLine("███       ███  ███▀▀███▄ ███▀▀███▄ ▄██▀▀██▄ ███▀▀███▄ ███   ███ ", width),
                CenterLine("███       ███  ███▄▄███▀ ███▄▄███▀ ███  ███ ███▄▄███▀ ▀███▄███▀ ", width),
                CenterLine("███       ███  ███  ███▄ ███▀▀██▄  ███▀▀███ ███▀▀██▄    ▀███▀   ", width),
                CenterLine("████████ ▄███▄ ████████▀ ███  ▀███ ███  ███ ███  ▀███    ███    ", width),

                "╚" + new string('═', width - 2) + "╝"
            };


            foreach (string line in banner)
            {
                Console.WriteLine(
                    line);
            }

            Console.WriteLine("");
            string stringToPrint = "Press any key to start...";
            int padLeft = Math.Max(0, width - 2 - stringToPrint.Length) / 2;
            string centeredString = new string(' ', padLeft) + stringToPrint;

            Console.Write(centeredString);
            Console.ReadKey();
        }

        private static string CenterLine(string text, int width)
        {
            int totalPadding = Math.Max(0, width - 2 - text.Length);
            int padLeft = totalPadding / 2;
            int padRight = totalPadding - padLeft;
            return "║" + new string(' ', padLeft) + text + new string(' ', padRight) + "║";
        }
    }
}
