namespace Panini
{
    public class ParsedIni
    {
        private ILookup<string, IniSection> lookup;
        private List<IniSection> sectionList;

        public ParsedIni(List<IniSection> sectionList)
        {
            this.sectionList = sectionList;
            this.lookup = UpdateLookup();
        }

        public ParsedIni()
        {
            this.sectionList = new List<IniSection>();
            this.lookup = UpdateLookup();
        }

        private ILookup<string, IniSection> UpdateLookup()
        {
            return sectionList.ToLookup(
                iniSection => iniSection.Name,
                iniSection => iniSection
            );
        }

        public ParsedIni AddSection(IniSection s)
        {
            sectionList.Add(s);
            this.lookup = UpdateLookup();
            return this;
        }

        /// <summary>
        /// Returns all the Sections for a specific identifier
        /// </summary>
        /// <param name="name">Section name</param>
        /// <returns>List of Sections</returns>
        public List<IniSection> GetSections(string name)
        {
            return lookup.Where(x => x.Key == name)
                            .SelectMany(x => x)
                            .ToList();
        }

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
    }
}
