namespace ShinyConsole
{
    public static class Shiny
    {
        private static readonly Random _rng = new();

        /// <summary>
        /// Writes the specified message to the console, coloring each character in a repeating rainbow pattern.
        /// </summary>
        /// <remarks>
        /// Whitespace characters in the message are printed without color. The method resets the console's foreground color after output.
        /// </remarks>
        /// <param name="message">The text to display in rainbow colors. Cannot be null.</param>
        public static void Rainbow(string message)
        {
            ConsoleColor[] rainbow =
            [
                ConsoleColor.Red,
                ConsoleColor.DarkYellow,
                ConsoleColor.Yellow,
                ConsoleColor.Green,
                ConsoleColor.Blue,
                ConsoleColor.DarkBlue,
                ConsoleColor.Magenta,
            ];

            int curr = 0;

            foreach (char c in message)
            {
                if (char.IsWhiteSpace(c))
                {
                    PrintWhitespace(c);
                    continue;
                }

                Console.ForegroundColor = rainbow[curr];
                Console.Write(c);

                curr++;

                if (curr == rainbow.Length)
                {
                    curr = 0;
                }
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Writes the specified message to the console, with each character in a random color.
        /// </summary>
        /// <remarks>
        /// Consecutive characters will not be the same color, and black is disallowed (as it would be invisible on the default console background).
        /// Whitespace characters in the message are printed without color. The method resets the console's foreground color after output.
        /// </remarks>
        /// <param name="message">The message to print in random colors.</param>
        public static void Random(string message)
        {
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor[] values = Enum.GetValues<ConsoleColor>();

            foreach (char c in message)
            {
                if (char.IsWhiteSpace(c))
                {
                    PrintWhitespace(c);
                    continue;
                }

                // Generate a random color, must not be the same as previous color
                ConsoleColor next;

                do
                {
                    next = values[_rng.Next(values.Length)];
                } while (next == ConsoleColor.Black || next == prev);

                Console.ForegroundColor = next;
                Console.Write(c);
                prev = next;
            }

            Console.ResetColor();
        }

        /// <summary>
        /// Prints the message in the specified color.
        /// </summary>
        /// <remarks>
        /// The color must be a valid ConsoleColor enum value and cannot be black (as it would be invisible on the default console background).
        /// Whitespace characters in the message are printed without color. The method resets the console's foreground color after output.
        /// </remarks>
        /// <param name="message">The message that will be colorized.</param>
        /// <param name="color">The color the message will be printed in.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="color"/> is invalid or black.</exception>
        public static void Colorize(string message, ConsoleColor color)
        {
            // Invalid color if it's not defined in the ConsoleColor enum or if it's black (invisible on default console background)
            if (!Enum.IsDefined(color) || color == ConsoleColor.Black)
            {
                Console.Write(message);
                return;
            }

            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        private static void PrintWhitespace(char c)
        {
            Console.ResetColor();
            Console.Write(c);
        }
    }
}
