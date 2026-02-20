namespace ShinyConsole.UnitTests
{
    /// <summary>
    /// Tests for the <see cref="Painter"/> class and color palettes.
    /// </summary>
    [TestClass]
    public class ShinyTests
    {
        /// <summary>
        /// Tests that <see cref="Painter.Colorize"/> writes the message to console with the specified color.
        /// </summary>
        [TestMethod]
        [DataRow("Hello, World!", ConsoleColor.Red)]
        [DataRow("Test message", ConsoleColor.Blue)]
        public void Colorize_ValidMessageAndColor_WritesMessageToConsole(string message, ConsoleColor color)
        {
            // Arrange
            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(message, color);

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
        /// Tests that premade color palettes in <see cref="ColorPallettes.Standard"/> are applied correctly.
        /// </summary>
        [TestMethod]
        public void StandardPalette_AppliesColorsCorrectly()
        {
            // Arrange
            string message = "Test";
            var palette = ColorPallettes.Standard.Pastels;

            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(message, palette);

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
        /// Tests that custom color palettes are applied correctly.
        /// </summary>
        [TestMethod]
        public void CustomPalette_AppliesColorsCorrectly()
        {
            // Arrange
            string message = "Custom Test";
            ConsoleColor[] customPalette = [ConsoleColor.Cyan, ConsoleColor.Magenta, ConsoleColor.Yellow];

            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(message, customPalette);

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
        /// Tests that <see cref="Painter.Colorize"/> handles empty strings without throwing exceptions.
        /// </summary>
        [TestMethod]
        public void Colorize_EmptyString_DoesNotThrow()
        {
            // Arrange
            string message = string.Empty;

            // Act & Assert
            Painter.Colorize(message, ConsoleColor.Green);
        }

        /// <summary>
        /// Tests that <see cref="Painter.Colorize"/> handles null messages gracefully.
        /// </summary>
        [TestMethod]
        public void Colorize_NullMessage_DoesNotThrow()
        {
            // Arrange
            string? message = null;

            // Act & Assert
            Painter.Colorize(message!, ConsoleColor.Green);
        }

        /// <summary>
        /// Tests that <see cref="Painter.Colorize"/> handles very long strings correctly.
        /// </summary>
        [TestMethod]
        public void Colorize_VeryLongString_WritesEntireString()
        {
            // Arrange
            string longMessage = new('A', 10000);

            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(longMessage, ConsoleColor.Cyan);

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
        /// Tests that <see cref="Painter.Colorize"/> handles special characters correctly.
        /// </summary>
        [TestMethod]
        [DataRow("Hello\nWorld")]
        [DataRow("Tab\there")]
        [DataRow("Unicode: \u00A9 \u00AE")]
        [DataRow("Emoji: 😀")]
        public void Colorize_SpecialCharacters_WritesSpecialCharacters(string message)
        {
            // Arrange
            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(message, ConsoleColor.DarkMagenta);

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
        /// Tests that <see cref="Painter.Colorize"/> handles invalid custom palettes gracefully.
        /// </summary>
        [TestMethod]
        [DataRow(null)]
        [DataRow(new ConsoleColor[0])]
        public void Colorize_InvalidCustomPalette_ThrowsArgumentException(ConsoleColor[]? palette)
        {
            // Arrange
            string message = "Test";

            // Act & Assert
            Assert.ThrowsExactly<ArgumentException>(() => Painter.Colorize(message, palette!));
        }

        /// <summary>
        /// Tests that <see cref="Painter.Colorize"/> cycles through colors correctly for long messages.
        /// </summary>
        [TestMethod]
        public void Colorize_LongMessage_CyclesColorsCorrectly()
        {
            // Arrange
            string message = new string('A', 20);
            ConsoleColor[] palette = [ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue];

            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(message, palette);

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
        /// Tests that <see cref="Painter.Colorize"/> is thread-safe.
        /// </summary>
        [TestMethod]
        public void Colorize_ThreadSafety_NoExceptions()
        {
            // Arrange
            string message = "Thread Test";
            var palette = new[] { ConsoleColor.Yellow, ConsoleColor.Cyan };

            void ThreadAction()
            {
                Painter.Colorize(message, palette);
            }

            // Act & Assert
            Parallel.For(0, 10, _ => ThreadAction());
        }

        /// <summary>
        /// Tests that <see cref="Painter.Colorize"/> resets the console color after execution.
        /// </summary>
        [TestMethod]
        public void Colorize_ResetsConsoleColor()
        {
            // Arrange
            ConsoleColor originalColor = Console.ForegroundColor;
            string message = "Reset Test";

            // Act
            Painter.Colorize(message, ConsoleColor.Magenta);

            // Assert
            Assert.AreEqual(originalColor, Console.ForegroundColor);
        }

        /// <summary>
        /// Tests that <see cref="Painter.Colorize"/> handles complex Unicode characters correctly.
        /// </summary>
        [TestMethod]
        [DataRow("你好世界")] // Chinese
        [DataRow("مرحبا بالعالم")] // Arabic
        [DataRow("こんにちは世界")] // Japanese
        public void Colorize_UnicodeCharacters_WritesCorrectly(string message)
        {
            // Arrange
            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(message, ConsoleColor.White);

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
        /// Tests that <see cref="Painter.Colorize"/> handles right-to-left text correctly.
        /// </summary>
        [TestMethod]
        public void Colorize_RightToLeftText_WritesCorrectly()
        {
            // Arrange
            string message = "שלום עולם"; // Hebrew

            using StringWriter stringWriter = new();
            TextWriter? originalOut = Console.Out;
            Console.SetOut(stringWriter);

            try
            {
                // Act
                Painter.Colorize(message, ConsoleColor.Yellow);

                // Assert
                string output = stringWriter.ToString();
                Assert.AreEqual(message, output);
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }
    }
}