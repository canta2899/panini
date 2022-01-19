namespace Panini
{
    public class ParsedIni
    {
        private ILookup<string, IniSection> lookup;
    
        internal ParsedIni(ILookup<string, IniSection> lookup)
        {
            this.lookup = lookup;
        }

        public ParsedIni(List<IniSection> sections)
        {
            this.lookup = sections.ToLookup(
                iniSection => iniSection.Name, 
                iniSection => iniSection
            );
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
