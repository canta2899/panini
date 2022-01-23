using System;
using System.Collections.Generic;
using Panini;

// Your ini file path
var iniPath = "./test.ini";

// If a file at the given path exists, Panini will parse it.
// Otherwise, an empty INI structure will be built

IniFile parsedIni = new IniFile(iniPath);

// Extract a section and add a new value
parsedIni.GetSection("General")?.Add("AnotherKey", "AnotherValue");

// Extracts a key from the section
string? value = parsedIni.GetSection("General")?.Get("AnotherKey");

// Extracts all the sections for the given name
List<IniSection> sections = parsedIni.GetSections("User");

// Iterates through all the sections
sections.ForEach(s => Console.WriteLine($"Username is : {s.Get("Name")}"));

// Adds a new section (in this case, a new user entry)
parsedIni.AddSection("User").TryAdd("Name", "Paul")?.TryAdd("Surname", "Jacob");

// Write the changes
parsedIni.Save();

// Now the ini file should be updated or created at the given path
