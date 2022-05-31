[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

<p align="center">
  <img alt="Panini" src="./Panini/assets/panini.svg" width="200" />
</p>
<h1 align="center">
  Panini 
</h1>

<p align="center">
  An simple .NET library for <b>INI</b> files parsing.
</p>

## Usage

Here's an example on how to use **Panini** in order to create, parse and update your INIs. The main focus is on **chainable methods** and being able to build **multiple sections with the same name** (which is the main reason why i built this library).

```cs
using System;
using System.Collections.Generic;
using Panini;

// Build an IniFile istance and parse one or more files

IniFile parsedIni = new IniFile();
parsedIni.Parse("./test.ini");

// Extract a section and try add a new value
parsedIni.GetSectionByName("General")?.TryAdd("AnotherKey", "AnotherValue");

// Now let's try to extract a key from a section
string? value = parsedIni.GetSectionByName("General")?.TryGet("AnotherKey");

// Extracts all the sections for the given name
List<IniSection> sections = parsedIni.GetSectionsByName("User");

// Iterates through all the sections
sections.ForEach(s => Console.WriteLine($"Username is : {s.TryGet("Name")}"));

// Adds a new section (in this case, a new user entry)
parsedIni.AddSection("User").TryAdd("Name", "Paul")?.TryAdd("Surname", "Jacob");

// Write the changes
parsedIni.Save("./newini.ini");

// Now the ini file should be updated or created at the given path
```

## How to install

Panini is made of three files, so you can easily compile `/Panini/Panini.csproj` and referenced the DLL inside your project or copy the files at `/Panini/` inside your project. Otherwise you can install via [NuGet](https://www.nuget.org/packages/Panini/) by running

```bash
dotnet add package Panini --version 1.3.0
```

## API

Here's a quick overview on the methods and classes that the API exposes. **Panini** aims to be a simple library so there's only a few things that you need to know before starting to use it. 

Ini files can be handled by two objects: 

- `IniFile`
- `IniSection`

The first one, in fact, wraps a collection of Ini sections and allows you to retrieve single or multiple selection or parse a new file.

### Creating and parsing ini files

You can create a new IniFile object by calling

```cs
IniFile ini = new IniFile();
```

After that, you can parse one or multiple files by calling

```cs
ini.Parse("path/to/your/ini");
```

**Parsing multiple files** is also possibile by calling `Parse()` over multiple paths, the result will be a single ini construct containing the sections of all the parsed files.

An IniFile can be saved by calling

```cs
ini.Save("path/to/your/ini");
```

Where you can specify a new path or the previous one (in the latter case, your ini file will be overwritten).

### Retrieving sections

A single section can be retrieved by calling

```cs
IniSection mySection = ini.GetSectionByName("name of your section");
```

If your IniFile object contains multiple sections with the same name, the first one will be retrieved. Moreover, **if a section with the given name does not exists**, `null` will be returned.

Retrieving **multiple sections with the same name** is also possible by running

```cs
List<IniSection> mySections = ini.GetSectionsByName("name of your sections");
```

In this case, a list of IniSections will be returned (the list will be empty if no section with the given name is found).

**All** the sections can be obtained by running

```cs
List<IniSection> allTheSections = ini.GetAllSections();
```

### Adding or removing a new section

A new section requires to be

1. Created
2. Added to the ini file

You can create a new section by running

```cs
IniSection newSection = ini.AddSection("name of the new section");
```

or by instanciating it through the constructor

```cs
IniSection newSection = new IniSection("name of the new section");
ini.AddSection(newSection);
```

A sections can also be removed from the file by running

```cs
ini.RemoveSectionByName("name of the section to be removed");
```

### Retrieving, removing and adding entries 

A new key-value entry can be added to a section by running

```cs
mySection.TryAdd("Key", "Value");
```

which returns `null` if the pair couldn't be added to the file (this mainly happens because a key with the same name already exists).

In order to remove a key and its value you can instead run

```cs
mySection.TryRemove("Key");
```

You can also use the **Set** option in order to add or replace a value by running

```cs
mySection.Set("key", "value");
```

In this case, if the key exists its value will be replaced. Otherwise, the method behaves like the `TryAdd` option.

### Adding comments

Lines starting with `#` will be parsed as comments and added to the **Comments** property of a section. You can also add new comments in code by running 

```cs
mySection.AddComment("my new line of comment");
```

### Clearing all the sections

All the sections can be cleared from a file by running 

```cs
ini.Clear();
```

Remember that, in order be applied to the `.ini` file, these modifications require you to call the `Save()` method too.

### Chaining methods

You can use the previous methods in a **chained fashion** like the following example:

```cs
// Creating the ini 
IniFile ini = new IniFile();

// Parsing two files
ini.Parse("./myini1.ini");
ini.Parse("./myini2.ini");

// Chaining methods

ini.GetSectionByName("section1")?.Set("key", "value");

ini.GetSectionsByName("multiple").ForEach(x => Console.WriteLine(s.TryGet("key")));

ini.AddSection("newSection").Set("key", "value").Set("key2", "value2");

ini.Save("./newpath.ini");
```













