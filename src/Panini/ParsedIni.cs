namespace Panini
{
    public class ParsedIni
    {
        private ILookup<string, IniSection> lookup;
        private List<IniSection> sectionList;
        public string Path { get; set; }

        public ParsedIni(List<IniSection> sectionList, string path)
        {
            // Builds an INI from a list of sections
            this.sectionList = sectionList;
            this.lookup = UpdateLookup();
            Path = path;
        }
        
        public ParsedIni(string path)
        {
            // Builds an empty ini
            this.sectionList = new List<IniSection>();
            this.lookup = UpdateLookup();
            Path = path;
        }

        private static void CheckPathExists(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();
        }

        private ILookup<string, IniSection> UpdateLookup()
        {
            return sectionList.ToLookup(
                iniSection => iniSection.Name,
                iniSection => iniSection
            );
        }

        // Adds a new section to the ini file
        public ParsedIni AddSection(IniSection s)
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
