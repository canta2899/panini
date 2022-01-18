using System.Collections;

namespace Panini 
{
    public class IniSection
    {
        /// <summary>
        /// Represents the name of the section
        /// </summary>
        public string Name { get; internal set; }

        public List<string> Comments { get; internal set; }

        internal Hashtable? Params { get; }

        internal IniSection(string name, Hashtable parameters)
        {
            Name = name;
            Params = parameters;
            Comments = new List<string>();
        }

        internal IniSection(string name, Hashtable parameters, List<string> comments)
        {
            Name = name;
            Params = parameters;
            Comments = comments; 
        }

        /// <summary>
        /// Obtains the value corrisponding to the given key from the current section
        /// </summary>
        /// <param name="key">Name of the parameter</param>
        /// <returns>The corresponding value, as a string</returns>
        public string? Get(string key) => Params switch
        {
            null => "",
            _ => Params.ContainsKey(key) ? (string?)Params[key] : ""
        };        
    }
}
