using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShinyConsole;

namespace ShinyConsole.UnitTests
{
    /// <summary>
    /// Tests for the <see cref="PainterFactory"/> class.
    /// </summary>
    [TestClass]
    public class ShinyTests
    {
        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> writes the message to console with the specified color.
        /// <br/>Input: Normal message and a valid ConsoleColor.
        /// <br/>Expected: Message is written to console output.
        /// </summary>
        [TestMethod]
        [DataRow("Hello, World!", ConsoleColor.Red)]
        [DataRow("Test message", ConsoleColor.Blue)]
        [DataRow("Another test", ConsoleColor.Green)]
        public void Colorize_ValidMessageAndColor_WritesMessageToConsole(string message, ConsoleColor color)
        {
            // Arrange
            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize(message, color);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> resets console color after writing.
        /// <br/>Input: Valid message and color.
        /// <br/>Expected: <see cref="Console.ForegroundColor"/> is reset to original color.
        /// </summary>
        [TestMethod]
        public void Colorize_ValidInput_ResetsConsoleColor()
        {
            // Arrange
            ConsoleColor originalColor = Console.ForegroundColor;
            TextWriter? originalOut = Console.Out;
            using StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize("Test", ConsoleColor.Magenta);

                // Assert
                Assert.AreEqual(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> handles empty string correctly.
        /// <br/>Input: Empty string and valid color.
        /// <br/>Expected: No exception thrown, empty output written.
        /// </summary>
        [TestMethod]
        public void Colorize_EmptyString_WritesEmptyString()
        {
            // Arrange
            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize(string.Empty, ConsoleColor.Yellow);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(string.Empty, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> handles whitespace-only string correctly.
        /// <br/>Input: Whitespace-only string and valid color.
        /// <br/>Expected: Whitespace written to console.
        /// </summary>
        [TestMethod]
        [DataRow("   ")]
        [DataRow("\t")]
        [DataRow("\n")]
        [DataRow(" \t\n ")]
        public void Colorize_WhitespaceString_WritesWhitespace(string message)
        {
            // Arrange
            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize(message, ConsoleColor.White);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> handles very long strings correctly.
        /// <br/>Input: Very long string and valid color.
        /// <br/>Expected: Entire string written to console.
        /// </summary>
        [TestMethod]
        public void Colorize_VeryLongString_WritesEntireString()
        {
            // Arrange
            string longMessage = new string('A', 10000);
            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize(longMessage, ConsoleColor.Cyan);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(longMessage, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> handles special characters correctly.
        /// <br/>Input: String with special characters and valid color.
        /// <br/>Expected: Special characters written to console.
        /// </summary>
        [TestMethod]
        [DataRow("Hello\nWorld")]
        [DataRow("Tab\there")]
        [DataRow("Unicode: \u00A9 \u00AE")]
        [DataRow("Emoji: 😀")]
        public void Colorize_SpecialCharacters_WritesSpecialCharacters(string message)
        {
            // Arrange
            using StringWriter stringWriter = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize(message, ConsoleColor.DarkMagenta);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> works with all valid ConsoleColor enum values.
        /// <br/>Input: Valid message and each valid ConsoleColor value.
        /// <br/>Expected: No exception thrown, message written for each color.
        /// </summary>
        [TestMethod]
        [DataRow(ConsoleColor.Black)]
        [DataRow(ConsoleColor.DarkBlue)]
        [DataRow(ConsoleColor.DarkGreen)]
        [DataRow(ConsoleColor.DarkCyan)]
        [DataRow(ConsoleColor.DarkRed)]
        [DataRow(ConsoleColor.DarkMagenta)]
        [DataRow(ConsoleColor.DarkYellow)]
        [DataRow(ConsoleColor.Gray)]
        [DataRow(ConsoleColor.DarkGray)]
        [DataRow(ConsoleColor.Blue)]
        [DataRow(ConsoleColor.Green)]
        [DataRow(ConsoleColor.Cyan)]
        [DataRow(ConsoleColor.Red)]
        [DataRow(ConsoleColor.Magenta)]
        [DataRow(ConsoleColor.Yellow)]
        [DataRow(ConsoleColor.White)]
        public void Colorize_AllValidConsoleColors_WritesMessage(ConsoleColor color)
        {
            // Arrange
            string message = "Test";
            using StringWriter stringWriter = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize(message, color);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> handles invalid enum values.
        /// <br/>Input: Valid message and invalid ConsoleColor enum value (cast from int).
        /// <br/>Expected: Behavior depends on Console implementation, but method should not throw.
        /// </summary>
        [TestMethod]
        [DataRow(-1)]
        [DataRow(16)]
        [DataRow(100)]
        public void Colorize_InvalidEnumValue_DoesNotThrow(int invalidColorValue)
        {
            // Arrange
            ConsoleColor invalidColor = (ConsoleColor)invalidColorValue;
            string message = "Test";
            using StringWriter stringWriter = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act & Assert - should not throw
                PainterFactory.Colorize(message, invalidColor);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Colorize"/> handles null message.
        /// <br/>Input: Null message and valid color.
        /// <br/>Expected: Console.Write handles null according to its implementation.
        /// </summary>
        [TestMethod]
        public void Colorize_NullMessage_HandlesNull()
        {
            // Arrange
            string? message = null;
            using StringWriter stringWriter = new StringWriter();
            TextWriter originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                PainterFactory.Colorize(message!, ConsoleColor.Green);

                // Assert
                string output = stringWriter.ToString();
                // Console.Write(null) writes empty string
                Assert.AreEqual(string.Empty, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles empty string without throwing and resets color.
        /// <br/>Input: Empty string
        /// <br/>Expected: No output and console color is reset.
        /// </summary>
        [TestMethod]
        public void Rainbow_EmptyString_CompletesSuccessfullyAndResetsColor()
        {
            // Arrange
            string message = string.Empty;
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(string.Empty, output);
                Assert.AreEqual(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles string with only whitespace characters correctly.
        /// <br/>Input: "   \t\n"
        /// <br/>Expected: All whitespace characters are written.
        /// </summary>
        [TestMethod]
        public void Rainbow_OnlyWhitespace_WritesWhitespaceCharacters()
        {
            // Arrange
            string message = "   \t\n";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> writes all non-whitespace characters with cycling colors.
        /// <br/>Input: "Hello"
        /// <br/>Expected: All characters are written, colors cycle through rainbow array.
        /// </summary>
        [TestMethod]
        public void Rainbow_OnlyNonWhitespace_WritesAllCharacters()
        {
            // Arrange
            string message = "Hello";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles mixed whitespace and non-whitespace characters.
        /// <br/>Input: "Hello World"
        /// <br/>Expected: All characters including space are written.
        /// </summary>
        [TestMethod]
        public void Rainbow_MixedWhitespaceAndNonWhitespace_WritesAllCharacters()
        {
            // Arrange
            string message = "Hello World";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> cycles through all seven rainbow colors with exactly seven characters.
        /// <br/>Input: "ABCDEFG" (7 characters, matches rainbow array length)
        /// <br/>Expected: All characters are written, each gets a different color.
        /// </summary>
        [TestMethod]
        public void Rainbow_SevenCharacters_CyclesThroughAllColors()
        {
            // Arrange
            string message = "ABCDEFG";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
                Assert.AreEqual(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> cycles back to the first color after seven non-whitespace characters.
        /// <br/>Input: "ABCDEFGH" (8 characters, should cycle back to first color)
        /// <br/>Expected: All characters are written, color cycles back after 7th character.
        /// </summary>
        [TestMethod]
        public void Rainbow_EightCharacters_CyclesBackToFirstColor()
        {
            // Arrange
            string message = "ABCDEFGH";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
                Assert.AreEqual(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles very long strings and cycles colors multiple times.
        /// <br/>Input: 50-character string
        /// <br/>Expected: All characters are written, colors cycle through rainbow multiple times.
        /// </summary>
        [TestMethod]
        public void Rainbow_LongString_WritesAllCharactersWithColorCycling()
        {
            // Arrange
            string message = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwx";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
                Assert.AreEqual(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles special and unicode characters correctly.
        /// <br/>Input: String with special characters like punctuation and unicode
        /// <br/>Expected: All characters are written correctly.
        /// </summary>
        [TestMethod]
        public void Rainbow_SpecialCharacters_WritesAllCharacters()
        {
            // Arrange
            string message = "!@#$%^&*()_+-=[]{}|;:',.<>?/~`äöü";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles strings with various types of whitespace correctly.
        /// <br/>Input: String with tabs, newlines, and spaces mixed with text
        /// <br/>Expected: All characters including whitespace are written.
        /// </summary>
        [TestMethod]
        public void Rainbow_VariousWhitespaceTypes_WritesAllCharacters()
        {
            // Arrange
            string message = "Line1\tTabbed\nLine2\rCarriage Return";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles single character string correctly.
        /// <br/>Input: "A"
        /// <br/>Expected: Character is written with first rainbow color, then color is reset.
        /// </summary>
        [TestMethod]
        public void Rainbow_SingleCharacter_WritesCharacterAndResetsColor()
        {
            // Arrange
            string message = "A";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
                Assert.AreEqual(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles single whitespace character correctly.
        /// <br/>Input: " "
        /// <br/>Expected: Whitespace is written and color is reset.
        /// </summary>
        [TestMethod]
        public void Rainbow_SingleWhitespace_WritesWhitespaceAndResetsColor()
        {
            // Arrange
            string message = " ";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
                Assert.AreEqual(originalColor, Console.ForegroundColor);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Rainbow"/> handles string with consecutive whitespace characters.
        /// <br/>Input: "A  B  C" (double spaces between letters)
        /// <br/>Expected: All characters including multiple spaces are written correctly.
        /// </summary>
        [TestMethod]
        public void Rainbow_ConsecutiveWhitespace_WritesAllCharacters()
        {
            // Arrange
            string message = "A  B  C";
            TextWriter originalOut = Console.Out;
            StringWriter stringWriter = new StringWriter();
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                Console.SetOut(stringWriter);

                // Act
                PainterFactory.Rainbow(message);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
                Console.ForegroundColor = originalColor;
            }
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> does not throw when given an empty string.
        /// <br/>Input: Empty string
        /// <br/>Expected: No exception thrown.
        /// </summary>
        [TestMethod]
        public void Random_EmptyString_DoesNotThrow()
        {
            // Arrange
            string message = string.Empty;

            // Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles strings containing only whitespace characters without throwing.
        /// <br/>Input: Whitespace-only strings
        /// <br/>Expected: No exception thrown.
        /// </summary>
        /// <param name="message">The whitespace-only string to test.</param>
        [TestMethod]
        [DataRow(" ")]
        [DataRow("   ")]
        [DataRow("\t")]
        [DataRow("\n")]
        [DataRow("\r\n")]
        [DataRow(" \t\n\r")]
        public void Random_WhitespaceOnlyString_DoesNotThrow(string message)
        {
            // Arrange & Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles a single non-whitespace character without throwing.
        /// <br/>Input: Single non-whitespace character
        /// <br/>Expected: No exception thrown.
        /// </summary>
        [TestMethod]
        public void Random_SingleCharacter_DoesNotThrow()
        {
            // Arrange
            string message = "A";

            // Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles normal strings with mixed characters without throwing.
        /// <br/>Input: Normal strings
        /// <br/>Expected: No exception thrown.
        /// </summary>
        /// <param name="message">The string to test.</param>
        [TestMethod]
        [DataRow("Hello")]
        [DataRow("Hello World")]
        [DataRow("Test123")]
        [DataRow("A B C")]
        public void Random_NormalStrings_DoesNotThrow(string message)
        {
            // Arrange & Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles strings with special and unicode characters without throwing.
        /// <br/>Input: Strings with special and unicode characters
        /// <br/>Expected: No exception thrown.
        /// </summary>
        /// <param name="message">The string with special characters to test.</param>
        [TestMethod]
        [DataRow("!@#$%^&*()")]
        [DataRow("Test\nNewline")]
        [DataRow("Tab\there")]
        [DataRow("Unicode: 你好")]
        [DataRow("Emoji: 🎨")]
        [DataRow("Mixed: Hello! 123 🎨")]
        public void Random_SpecialCharacters_DoesNotThrow(string message)
        {
            // Arrange & Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles very long strings without throwing.
        /// <br/>Input: Very long string
        /// <br/>Expected: No exception thrown.
        /// </summary>
        [TestMethod]
        public void Random_VeryLongString_DoesNotThrow()
        {
            // Arrange
            string message = new string('A', 10000);

            // Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles strings with all whitespace types mixed with characters without throwing.
        /// <br/>Input: Strings mixing whitespace and characters
        /// <br/>Expected: No exception thrown.
        /// </summary>
        [TestMethod]
        public void Random_MixedWhitespaceAndCharacters_DoesNotThrow()
        {
            // Arrange
            string message = "Hello\tWorld\nTest\rLine";

            // Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles strings starting and ending with whitespace without throwing.
        /// <br/>Input: Strings with leading and trailing whitespace
        /// <br/>Expected: No exception thrown.
        /// </summary>
        [TestMethod]
        public void Random_StringWithLeadingAndTrailingWhitespace_DoesNotThrow()
        {
            // Arrange
            string message = "  Hello World  ";

            // Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles consecutive whitespace characters without throwing.
        /// <br/>Input: Strings with consecutive whitespace
        /// <br/>Expected: No exception thrown.
        /// </summary>
        [TestMethod]
        public void Random_ConsecutiveWhitespace_DoesNotThrow()
        {
            // Arrange
            string message = "A     B     C";

            // Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }

        /// <summary>
        /// Tests that <see cref="PainterFactory.Random"/> handles strings with various control characters without throwing.
        /// <br/>Input: Strings containing control characters
        /// <br/>Expected: No exception thrown.
        /// </summary>
        [TestMethod]
        public void Random_ControlCharacters_DoesNotThrow()
        {
            // Arrange
            string message = "Test\0Null\bBackspace\fFormFeed";

            // Act
            PainterFactory.Random(message);

            // Assert
            // No exception means success
        }
    }
}