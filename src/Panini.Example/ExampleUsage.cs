using Panini;

// Your ini path

var iniPath = "./Panini.Example/test.ini";

ParsedIni parsedIni = IniParser.Parse(iniPath);

Console.WriteLine("!!Single section!!");

IniSection? currentSection = parsedIni.GetSection("main1");

Console.WriteLine($"Test2: {currentSection?.Get("test2")}");
Console.WriteLine($"Test1: {currentSection?.Get("test1")}");

// Console.WriteLine("\n\n!!Multiple sections!!");

// List<IniSection> sections = parsedIni.GetSections("main3");

// foreach (IniSection s in sections)
// {
//     Console.WriteLine($"Param1: {s.Get("test1")}");
//     Console.WriteLine($"Param2: {s.Get("test2")}");
// }


// Now writing a new ini file...

IniSection newSection = new IniSection("User");

newSection.Add("name", "John");
newSection.Add("surname", "Kings");

List<IniSection> sections = new List<IniSection>();

sections.Add(newSection);

ParsedIni pi = new ParsedIni(sections);

IniParser.Write(pi, "./mynewini.ini");

