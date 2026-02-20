using static ShinyConsole.Enums;
using ShinyConsole.ColorPallettes;

namespace ShinyConsole.Example
{
    public static class Program
    {
        public static void Main()
        {
            /* Demonstrate Rainbow */

            // - Sequential Characters
            Painter.Colorize("This text tests the coloring goes sequentially on the characters.", Prides.Rainbow);
            PrintGap();

            // - Random Characters
            Painter.Colorize("This text tests the coloring goes randomly on the characters.", Prides.Rainbow, true);
            PrintGap();

            // - Sequential Words
            Painter.Colorize("This text tests the coloring goes sequentially on the words.", Prides.Rainbow, false, ColorizationScope.Words);
            PrintGap();

            // - Random Words
            Painter.Colorize("This text tests the coloring goes randomly on the words.", Prides.Rainbow, true, ColorizationScope.Words);
            PrintGap();

            /* Demonstrate Custom Pallette */

            ConsoleColor[] customPallette = [ ConsoleColor.Magenta, ConsoleColor.Cyan, ConsoleColor.Yellow ];
            Painter.Colorize("This text uses a custom pallette!", customPallette, true);
            PrintGap();

            /* Demonstrate Custom Pallette with Sentences */
            string multiSentenceMessage = "This tests the coloring goes sequentially on the sentences. See how it changes? See how it changes again!?";
            Painter.Colorize(multiSentenceMessage, National.Italian, false, ColorizationScope.Sentences);
            PrintGap();

            /* Demonstrate Custom Pallette with Paragraphs */
            string multiParagraphMessage = "" +
                "This tests the coloring goes sequentially on the paragraphs.\nSee how it stays the same even though this sentence is on a new line?\n\n" +
                "But now, the paragraph shifted, so it changes colors!\r\n\r\n" +
                "Let's try a few sentences in a row. No change yet! Looking good! :)\n\n";
            Painter.Colorize(multiParagraphMessage, National.American, false, ColorizationScope.Paragraphs);
            PrintGap();
        }

        private static void PrintGap()
        {
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
