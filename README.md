# ShinyConsole

Lightweight console coloring helpers published as the `JMykitta.ShinyConsole` NuGet package.

[The github repository](https://github.com/JJWren/ShinyConsole)
contains [an example project](https://github.com/JJWren/ShinyConsole/tree/main/ShinyConsole.Example)
that references the package: `ShinyConsole.Example\ShinyConsole.Example.csproj`.

## Prerequisites
- .NET 10 SDK installed
- Visual Studio 2022/2026 or the `dotnet` CLI

## Install from NuGet
- Using the dotnet CLI (from repository root):
	- `dotnet add package JMykitta.ShinyConsole --version 2.0.0`
- Using the Package Manager Console in Visual Studio:
	- `NuGet\Install-Package JMykitta.ShinyConsole -Version 2.0.0`
- Using Visual Studio UI:
	1. Right-click the `ShinyConsole.Example` project -> __Manage NuGet Packages__.
	2. Browse for `JMykitta.ShinyConsole` and install version `2.0.0`.

## csproj reference (example)
```xml
<!-- ShinyConsole.Example\ShinyConsole.Example.csproj -->
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
	
  <ItemGroup>
    <PackageReference Include="JMykitta.ShinyConsole" Version="2.0.0" />
  </ItemGroup>
</Project>
```

## Quick Usage Example

```csharp
using static ShinyConsole.Enums;
using ShinyConsole.ColorPallettes;

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
```

Output:

![Example Output](./ExampleImages/ExampleOutput.png)

## General Notes
- The example project targets `net10.0`.
- Version 1.0.* is not supported. Please use Version 2.0.0+.

## Release Notes
- Version 2.0.0:
  - Complete refactor.
  - `Shiny` is now `Painter`.
  - There are now premade color pallettes available in the `ShinyConsole.ColorPallettes` namespace,
	but you can also create your own custom pallettes by passing an array of `ConsoleColor` values to the `Painter.Colorize` method.
  
