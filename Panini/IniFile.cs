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

        public IniFile(List<IniSection> sectionList)
        {
            // Builds an INI from a list of sections
            this.sectionList = sectionList;
            this.lookup = UpdateLookup();
        }

        public IniFile()
        {
            // Builds an empty ini
            this.sectionList = new List<IniSection>();
            this.lookup = UpdateLookup();
        }

        private ILookup<string, IniSection> UpdateLookup()
        {
            // Creates lookup
            return sectionList.ToLookup(
                iniSection => iniSection.Name,
                iniSection => iniSection
            );
        }

        public bool Parse(TextReader sr)
        {
            foreach (var x in IniParser.Parse(sr))
            {
               sectionList.Add(x);
            }
            this.lookup = UpdateLookup();
            return true;
        }

        public bool Parse(string path)
        {
            if (!File.Exists(path)) return false;

            foreach (var x in IniParser.Parse(path))
            {
                sectionList.Add(x);
            }
            this.lookup = UpdateLookup();
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

        public IniFile RemoveSectionByName(string sectionName)
        {
            List<IniSection> sections = this.sectionList.Where(x => x.Name == sectionName).ToList();
            this.sectionList = this.sectionList.Except(sections).ToList();

            return this;
        }


        // Returns all the Sections for a specific identifier
        public List<IniSection> GetSectionsByName(string name)
        {
            return lookup.Where(x => x.Key == name)
                            .SelectMany(x => x)
                            .ToList();
        }

        // Returns all the sections of the parsed file
        public List<IniSection> GetAllSections()
        {
            return this.sectionList;
        }

        public IniSection GetSectionByName(string name)
        {
            if (!lookup.Contains(name)) return null;
            return lookup[name].First();
        }

        public void Save(string path)
        {
            IniParser.Write(this, path: path);
        }

        public void Save(TextWriter tw)
        {
            IniParser.Write(this, tw);
        }
    }
}
