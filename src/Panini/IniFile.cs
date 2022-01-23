using Panini.Parser;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Panini
{
    public class IniFile
    {
        private ILookup<string, IniSection> lookup;
        private List<IniSection> sectionList;
        public string Path { get; set; }

        public IniFile(List<IniSection> sectionList, string path)
        {
            // Builds an INI from a list of sections
            Path = path;
            this.sectionList = sectionList;
            this.lookup = UpdateLookup();
        }

        public IniFile(string path)
        {
            // Builds an empty ini
            Path = path;
            this.sectionList = GetSectionsFromFile();
            this.lookup = UpdateLookup();
        }

        private ILookup<string, IniSection> UpdateLookup()
        {
            return sectionList.ToLookup(
                iniSection => iniSection.Name,
                iniSection => iniSection
            );
        }

        private List<IniSection> GetSectionsFromFile()
        {
            return File.Exists(Path) ? IniParser.Parse(Path)
                                    : new List<IniSection>();
        }

        public bool Parse(string? path = null)
        {
            string finalPath = (path == null) ? Path : path;
            if (!File.Exists(finalPath)) return false;

            IniParser.Parse(finalPath).ForEach(x => sectionList.Add(x));
            return true;
        }

        public void Clear() => sectionList.Clear();

        // Adds a new section to the ini file
        public IniFile AddSection(IniSection s)
        {
            this.sectionList.Add(s);
            this.lookup = UpdateLookup();

            // chainable method
            return this;
        }

        public IniSection AddSection(string sectionName)
        {

            IniSection newSection = new IniSection(sectionName);
            this.sectionList.Add(newSection);
            this.lookup = UpdateLookup();

            return newSection;
        }

        public IniFile RemoveSection(IniSection section)
        {
            this.sectionList.Remove(section);
            this.lookup = UpdateLookup();

            return this;
        }

        public IniFile RemoveSection(string sectionName)
        {
            List<IniSection> sections = this.sectionList.Where(x => x.Name == sectionName).ToList();
            this.sectionList = this.sectionList.Except(sections).ToList();

            return this;
        }


        // Returns all the Sections for a specific identifier
        public List<IniSection> GetSections(string name)
        {
            return lookup.Where(x => x.Key == name)
                            .SelectMany(x => x)
                            .ToList();
        }

        // Returns all the sections of the parsed file
        public List<IniSection> GetAllSections()
        {
            return this.lookup.SelectMany(x => x).ToList();
        }

        public IniSection? GetSection(string name)
        {
            if (!lookup.Contains(name)) return null;
            return lookup[name].First();
        }

        public void Save()
        {
            IniParser.Write(this);
        }

        public void Save(string path)
        {
            IniParser.Write(this, path: path);
        }
    }
}
