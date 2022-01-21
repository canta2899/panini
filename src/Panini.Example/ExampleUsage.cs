using Panini;

// Your ini path

var iniPath = "/Users/andrea/development/projects/panini/src/Panini.Example/test.ini";

// Parses the file
IniFile parsedIni = new IniFile(iniPath);

// Extracts the section with the given name
IniSection? currentSection = parsedIni.GetSection("General")?.Add("AnotherKey", "AnotherValue");

// Extracts a key from the section
Console.WriteLine($"WhoAmI: {currentSection?.Get("WhoAmI")}");

// Extracts all the sections for the given name
List<IniSection> sections = parsedIni.GetSections("User");

// Iterates through all the sections
sections.ForEach(s => Console.WriteLine($"Username is : {s.Get("Name")}"));

// Let's add a new user

// 1. Create a new section
IniSection newSection = new IniSection("User");

Console.Write("New user name: ");
string newName = Console.ReadLine() ?? "";
Console.Write("New user surname: ");
string newSurname = Console.ReadLine() ?? "";

// 2. Add the entries to the section
newSection.Add("Name", newName).Add("Surname", newSurname);

// 3. Add the section to the ini file
parsedIni.AddSection(newSection);

// Write the changes
parsedIni.Save();

