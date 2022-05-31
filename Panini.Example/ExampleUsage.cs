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
