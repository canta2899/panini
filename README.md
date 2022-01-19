<p align="center">
  <img alt="Medusa" src="./src/Panini/assets/icon.svg" width="100" />
</p>
<h1 align="center">
  Panini 
</h1>

<p align="center">
  An simple .NET library for <b>INI</b> files parsing.
</p>

## Usage

```cs

using Panini;

// Parse a file
ParsedIni parsedIni = IniParser.Parse("./test.ini");

// Extract a section
IniSection? currentSection = parsedIni.GetSection("main1");

// Get values for parameters
Console.WriteLine($"Test2: {currentSection?.Get("test2")}");

Console.WriteLine($"Test1: {currentSection?.Get("test1")}");

Console.WriteLine("\n\n!!Multiple sections!!");


// Or extract multiple sections with the same name

List<IniSection> sections = parsedIni.GetSections("main3");

foreach (IniSection s in sections)
{
    Console.WriteLine($"Param1: {s.Get("test1")}");
    Console.WriteLine($"Param2: {s.Get("test2")}");
}

```

## How to install

You can easily compile `src/Panini/Panini.csproj` and referenced DLL inside your project. Otherwise, you can install via [NuGet](https://www.nuget.org/packages/Panini/) by running

```bash
dotnet add package Panini --version 1.0.0
```
