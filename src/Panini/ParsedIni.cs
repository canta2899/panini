using Panini.Parser;

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
            List<IniSection> parsed;

            if (IniParser.CheckPathExists(Path))
            {
                parsed = IniParser.Parse(Path);
            }
            else
            {
                parsed = new List<IniSection>();
            }

            return parsed;
        }

        public bool Parse()
        {
            if (!IniParser.CheckPathExists(Path)) return false;

            IniParser.Parse(Path).ForEach(x => sectionList.Add(x));
            return true;
        }

        public void Clear() => sectionList.Clear();

        // Adds a new section to the ini file
        public IniFile AddSection(IniSection s)
        {
            sectionList.Add(s);
            this.lookup = UpdateLookup();

            // chainable method
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

        /// <summary>
        /// Returns the first occurence of a section for the given identifier
        /// </summary>
        /// <param name="name">Section name</param>
        /// <returns>Section object, null if not found </returns>
        public IniSection? GetSection(string name)
        {
            if (!lookup.Contains(name))
                return null;
            return lookup[name].First();
        }

        public void Save()
        {
            IniParser.Write(this);
        }
    }
}
