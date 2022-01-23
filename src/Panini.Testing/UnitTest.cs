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
            IniFile f = new IniFile("./test.ini");
            f.AddSection("Profile").Add("Username", "Jacob").Add("Email", "jacob@gmail.com");

            IniSection s = new IniSection("General");

            s.Add("Address", "An address").Add("Telephone", "A telephone");

            f.AddSection(s);

            f.Save();

            // Checks if the file was created
            Assert.True(File.Exists("./test.ini"));
        }

        [Fact, Priority(1)]
        public void CheckParsing()
        {

            // Creates an Ini file by parsing the onew created in the 
            // previous test
            IniFile f = new IniFile("./test.ini");

            // Writes a section
            f.AddSection("New Section").Add("Username", "Jacob");
            f.Save();

            // Parses the same ini file again
            IniFile newf = new IniFile("./test.ini");

            // Removes the key added previously
            newf.GetSection("New Section")?.Remove("New key");

            // Tries to get ad unexistent key
            Assert.Null(newf.GetSection("New Section")?.Get("New key"));

            // Deletes the section
            newf.RemoveSection("New Section");

            newf.Save();
        }

        [Fact, Priority(2)]
        public void TryAddKey()
        {
            IniFile f = new IniFile("./test.ini");

            // TryAdd should give null, because the key already exists
            Assert.Null(f.GetSection("Profile")?.TryAdd("Username", "Pier"));

            // This then sould work
            f.GetSection("Profile")?.Set("Username", "Pier");

            Assert.Equal("Pier", f.GetSection("Profile")?.Get("Username"));
        }

    }
}
