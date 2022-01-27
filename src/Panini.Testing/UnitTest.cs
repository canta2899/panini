using Xunit;
using System;
using System.IO;
using Xunit.Priority;

namespace Panini.Testing
{


    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    [DefaultPriority(0)]
    public class UnitTest1
    {

        [Fact, Priority(0)]
        public void CreateNewIni()
        {
            // Deletes file if it exists

            File.Delete("./test.ini");
            IniFile f = new IniFile();
            f.Parse("./test.ini");

            f.AddSection("Profile").TryAdd("Username", "Jacob")?.TryAdd("Email", "jacob@gmail.com");

            IniSection s = new IniSection("General");

            s.TryAdd("Address", "An address")?.TryAdd("Telephone", "A telephone");

            f.AddSection(s);

            // Overwrite the previously parsed file
            f.Save("./test.ini");

            // Checks if the file was created
            Assert.True(File.Exists("./test.ini"));
        }

        [Fact, Priority(1)]
        public void CheckParsing()
        {

            // Creates an Ini file by parsing the onew created in the 
            // previous test
            IniFile f = new IniFile();
            f.Parse("./test.ini"); ;

            // Writes a section
            f.AddSection("New Section").TryAdd("Username", "Jacob");
            f.Save("./test.ini");

            // Parses the same ini file again
            IniFile newf = new IniFile();
            newf.Parse("./test.ini");

            // Removes the key added previously
            newf.GetSectionByName("New Section")?.TryRemove("New key");

            // Tries to get ad unexistent key
            Assert.Null(newf.GetSectionByName("New Section")?.TryGet("New key"));

            // Deletes the section
            newf.RemoveSectionByName("New Section");

            newf.Save("./test.ini");
        }

        [Fact, Priority(2)]
        public void TryAddKey()
        {
            IniFile f = new IniFile();
            f.Parse("./test.ini");

            // TryAdd should give null, because the key already exists
            Assert.Null(f.GetSectionByName("Profile")?.TryAdd("Username", "Pier"));

            // This then sould work
            f.GetSectionByName("Profile")?.Set("Username", "Pier");

            Assert.Equal("Pier", f.GetSectionByName("Profile")?.TryGet("Username"));
        }

    }
}
