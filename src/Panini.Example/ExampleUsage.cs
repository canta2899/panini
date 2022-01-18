// using Panini;

// Console.WriteLine("!!Single section!!");

// ParsedIni parsedIni = IniParser.Parse("./test.ini");

// IniSection? currentSection = parsedIni.GetSection("main1");

// Console.WriteLine($"Test2: {currentSection?.Get("test2")}");
// Console.WriteLine($"Test1: {currentSection?.Get("test1")}");

// Console.WriteLine("\n\n!!Multiple sections!!");

// List<IniSection> sections = parsedIni.GetSections("main3");

// foreach (IniSection s in sections)
// {
//     Console.WriteLine($"Param1: {s.Get("test1")}");
//     Console.WriteLine($"Param2: {s.Get("test2")}");
// }
