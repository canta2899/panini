<p align="center">
  <img alt="Panini" src="./src/Panini/assets/icon.png" width="100" />
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

// Your ini path

var iniPath = "./test.ini";

// Parses the file
ParsedIni parsedIni = IniParser.Parse(iniPath);

// Extracts the section with the given name
IniSection? currentSection = parsedIni.GetSection("General");

// Extracts a key from the section
Console.WriteLine($"WhoAmI: {currentSection?.Get("WhoAmI")}");

// Extracts all the sections for the given name
List<IniSection> sections = parsedIni.GetSections("User");

// Iterates through all the sections
sections.ForEach(s => Console.WriteLine($"Username is : {s.Get("Name")}"));

// Let's add a new user

// 1. Create a new section
IniSection newSection = new IniSection("User");

// 2. Add the entries to the section
newSection.Add("Name", "John").Add("Surname", "King");

// 3. Add the section to the ini file
parsedIni.AddSection(newSection);

// Write the changes
parsedIni.Save();

```

## How to install

You can easily compile `src/Panini/Panini.csproj` and referenced DLL inside your project. Otherwise, you can install via [NuGet](https://www.nuget.org/packages/Panini/) by running

```bash
dotnet add package Panini --version 1.0.7
```
