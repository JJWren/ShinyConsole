using System.Text.RegularExpressions;
using static ShinyConsole.Enums;

namespace ShinyConsole
{
    public static partial class Painter
    {
        private static readonly Random _rng = new();


        [GeneratedRegex(@"(\r\n\r\n|\n\n)")]
        private static partial Regex ParagraphRgx();

        [GeneratedRegex(@"(.*?[\.\!\?]+)")]
        private static partial Regex SentenceRgx();

        [GeneratedRegex(@"(\S+|\s+)")]
        private static partial Regex WordRgx();

        /// <summary>
        /// Applies colorization to the specified message in the console using the provided palette and scope.
        /// </summary>
        /// <remarks>The method writes the colorized message directly to the console. The colorization
        /// scope determines how the palette is applied: by character, word, sentence, or paragraph. The palette must
        /// contain at least one color; otherwise, no colorization occurs.</remarks>
        /// <param name="message">The message to be colorized and written to the console. If null or empty, no output is produced.</param>
        /// <param name="pallette">An array of console colors used to colorize the message. The palette determines the colors applied according
        /// to the selected scope.</param>
        /// <param name="shouldRandomize">A value indicating whether the palette colors should be applied in random order. If <see langword="true"/>,
        /// colors are assigned randomly; otherwise, they follow the palette sequence.</param>
        /// <param name="scope">The scope that defines how the message is segmented for colorization. Possible values include characters,
        /// words, sentences, or paragraphs.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="scope"/> specifies an unsupported colorization scope.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="pallette"/> is null or empty.</exception>
        public static void Colorize(
            string message,
            ConsoleColor[] pallette,
            bool shouldRandomize = false,
            ColorizationScope scope = ColorizationScope.Characters)
        {
            if (string.IsNullOrEmpty(message)) return;

            if (pallette == null || pallette.Length == 0)
                throw new ArgumentException("Palette must contain at least one color.", nameof(pallette));

            switch (scope)
            {
                case ColorizationScope.Characters:
                    ColorizeCharacters(message, pallette, shouldRandomize);
                    break;
                case ColorizationScope.Words:
                    ColorizeWords(message, pallette, shouldRandomize);
                    break;
                case ColorizationScope.Sentences:
                    ColorizeSentences(message, pallette, shouldRandomize);
                    break;
                case ColorizationScope.Paragraphs:
                    ColorizeParagraphs(message, pallette, shouldRandomize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(scope), $"Unsupported colorization scope: {scope}");
            }
        }

        /// <summary>
        /// Writes the specified message to the console using the given foreground color.
        /// </summary>
        /// <remarks>After writing the message, the console's foreground color is reset to its previous
        /// value.</remarks>
        /// <param name="message">The message to display in the console. If null or empty, no output is written.</param>
        /// <param name="color">The foreground color to use when displaying the message.</param>
        public static void Colorize(string message, ConsoleColor color)
        {
            if (string.IsNullOrEmpty(message)) return;

            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the specified whitespace character to the console after resetting the console's color settings.
        /// </summary>
        /// <param name="c">The whitespace character to write to the console.</param>
        public static void PrintWhitespace(char c)
        {
            Console.ResetColor();
            Console.Write(c);
        }

        /// <summary>
        /// Writes the specified message to the console after resetting the console color.
        /// </summary>
        /// <param name="message">The message to display. If null or empty, no output is written.</param>
        public static void PrintWhitespace(string message)
        {
            if (string.IsNullOrEmpty(message)) return;

            Console.ResetColor();
            Console.Write(message);
        }

        private static void ColorizeCharacters(string message, ConsoleColor[] pallette, bool shouldRandomize)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Console.ResetColor();

            if (shouldRandomize)
            {
                WriteRandom(message, pallette);
            }
            else
            {
                WriteSequential(message, pallette);
            }

            Console.ResetColor();
        }

        private static void ColorizeWords(string message, ConsoleColor[] pallette, bool shouldRandomize)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Console.ResetColor();
            MatchCollection matches = WordRgx().Matches(message);
            string[] words = [.. matches.Select(m => m.Value)];

            if (shouldRandomize)
            {
                WriteRandom(words, pallette);
            }
            else
            {
                WriteSequential(words, pallette);
            }

            Console.ResetColor();
        }

        private static void ColorizeSentences(string message, ConsoleColor[] pallette, bool shouldRandomize)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Console.ResetColor();
            MatchCollection matches = SentenceRgx().Matches(message);
            string[] sentences = [.. matches.Select(m => m.Value)];

            if (shouldRandomize)
            {
                WriteRandom(sentences, pallette);
            }
            else
            {
                WriteSequential(sentences, pallette);
            }

            Console.ResetColor();
        }

        private static void ColorizeParagraphs(string message, ConsoleColor[] pallette, bool shouldRandomize)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            Console.ResetColor();
            string[] paragraphs = ParagraphRgx().Split(message);

            if (shouldRandomize)
            {
                WriteRandom(paragraphs, pallette);
            }
            else
            {
                WriteSequential(paragraphs, pallette);
            }

            Console.ResetColor();
        }

        private static void WriteRandom<T>(
            IEnumerable<T> items,
            ConsoleColor[] pallette) where T : notnull
        {
            if (items == null) return;

            ConsoleColor prev = Console.ForegroundColor;

            foreach (T item in items)
            {
                if (item is char c)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        PrintWhitespace(c);
                        continue;
                    }
                }
                else if (item is string s)
                {
                    if (string.IsNullOrWhiteSpace(s))
                    {
                        PrintWhitespace(s);
                        continue;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Unsupported type: {typeof(T)}");
                }

                // Generate a random color, must not be the same as previous color
                ConsoleColor next;

                do
                {
                    next = pallette[_rng.Next(pallette.Length)];
                } while (next == prev);

                Console.ForegroundColor = next;
                Console.Write(item);
                prev = next;
            }
        }

        /// <summary>
        /// Helper method to write items colored sequentially according to the provided palette.
        /// </summary>
        /// <typeparam name="T">The type of the items to be written.</typeparam>
        /// <param name="items">The items to be written.</param>
        /// <param name="pallette">The color palette to use for coloring the items.</param>
        private static void WriteSequential<T>(
            IEnumerable<T> items,
            ConsoleColor[] pallette) where T : notnull
        {
            if (items == null) return;

            int curr = 0;

            foreach (T item in items)
            {
                if (item is char c)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        PrintWhitespace(c);
                        continue;
                    }
                }
                else if (item is string s)
                {
                    if (string.IsNullOrWhiteSpace(s))
                    {
                        PrintWhitespace(s);
                        continue;
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException($"Unsupported type: {typeof(T)}");
                }

                Console.ForegroundColor = pallette[curr];
                Console.Write(item);
                curr++;

                if (curr == pallette.Length) curr = 0;
            }
        }
    }
}
