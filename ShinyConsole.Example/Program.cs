namespace ShinyConsole.Example
{
    public static class Program
    {
        public static void Main()
        {
            // Demonstrate Rainbow
            string message = "Hello, Rainbow!";
            Console.WriteLine($"Shiny.Rainbow(\"{message}\"):");
            Shiny.Rainbow(message);
            PrintGap();

            // Demonstrate Random
            string randomMessage = "Random colors!";
            Console.WriteLine($"Shiny.Random(\"{randomMessage}\"):");
            Shiny.Random(randomMessage);
            PrintGap();

            // Demonstrate Colorize
            string colorMessage = "This text is red.";
            Console.WriteLine($"Shiny.Rainbow(\"{colorMessage}\", ConsoleColor.Red):");
            Shiny.Colorize(colorMessage, ConsoleColor.Red);
            PrintGap();
        }

        private static void PrintGap()
        {
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
