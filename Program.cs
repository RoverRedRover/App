using static System.Console;
using SoloLearn.Shared;

namespace App
{
    class Program
    {
        static readonly string Title = "Luhn Tester";
        static readonly string Author = "David Casson";
        static readonly string LastUpdated = "October 2022";
        static ConsoleColor origForeground;

        static void Main(string[] args)
        {
            Console.Title = Title;

            // Handle program called without arguments passed from console
            if (args.Length == 0)
            {
                WriteLine(
                    "Please pass at least one 16-digit credit " +
                    "card number as an argument at the command " +
                    "line. Spaces and dashes are acceptable but " +
                    "not required."
                );
                Environment.Exit(0);
            }

            // Save user's current console character color for later restore
            origForeground = ForegroundColor;

            PrintHeading(argsLength: args.Length);

            // Loop thru args
            foreach (string arg in args)
            {
                TestAndPrintResult(arg);
            }

            // Restore user's original font color setting
            ForegroundColor = origForeground;

            // Keep console open until user presses a key
            WriteLine("\nPress enter to exit...");
            Console.Read();

        }

        static void PrintHeading(int argsLength)
        {
            ForegroundColor = ConsoleColor.Blue;
            WriteLine();
            WriteLine(Title);
            ForegroundColor = origForeground;
            WriteLine($"{Author}, {LastUpdated}");
            WriteLine("Determine if a given credit card number passes the Luhn test.");
            WriteLine();

            WriteLine(
                format: "Testing {0} argument(s) passed from console...",
                arg0:   argsLength
            );
        }

        /// <summary>
        /// For each argument the user has passed to this program
        /// from the console, determine if the input is a 16-digit
        /// string; if not, print "INVALID INPUT"; if so, indicate
        /// whether the 16-digit number passes the Luhn test.
        /// </summary>
        /// <param name="arg">the input to be parsed as a credit card number</param>
        static void TestAndPrintResult(string arg)
        {
            // Determine if user input is minimally qualified as
            // a credit card number (i.e., is 16 digits long)
            (bool IsValidCardNumber, string validatedCardNumber) =
                InputValidator.ValidateCardNumber(ccn: arg);

            ForegroundColor = ConsoleColor.Cyan;

            if (IsValidCardNumber)
            {
                Write($"{SeparateWithChars(validatedCardNumber)}: ");
                if (LuhnTest.Test(validatedCardNumber))
                {
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("PASSED");
                }
                else
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("FAILED");
                }
            }
            else
            {
                ForegroundColor = ConsoleColor.Red;
                WriteLine($"{arg}: INVALID INPUT");
            }
        }

        /// <summary>
        /// Insert a single-character separator after every 4th number
        /// in a sixteen-character string. Remember to validate ccn
        /// before using it as an argument to this method.
        /// </summary>
        /// <param name="ccn">16-digit credit card number</param>
        /// <param name="separator">The separator character to be inserted
        /// after every 4th character</param>
        /// <returns>A 19-character string</returns>
        static string SeparateWithChars(string ccn, char separator = '-')
        {
            string res = ccn;
            byte passNum = 1;
            byte baseNum = 4;

            while (passNum < (res.Length/4))
            {
                res = res.Insert(
                    startIndex: baseNum * passNum + passNum - 1, 
                    value: separator.ToString()
                );
                passNum++;
            }
            return res;
        }

    }

}
