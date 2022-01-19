using System.Collections;

namespace Panini 
{
    public class IniSection
    {
        // Represents the name of the section
        public string Name { get; internal set; }

        // Collection of comments inside the section
        public List<string> Comments { get; internal set; }

        // Hashtable of the key-value pair of the section
        public Hashtable Params { get; }

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

        public IniSection(string name)
        {
            Name = name;
            Params = new Hashtable();
            Comments = new List<string>();
        }
        

        // Adds a new key, value pair to the section
        public IniSection Add(string key, string value)
        {
            Params.Add(key, value);
            return this;
        }

        // Adds a new comment to the section
        public IniSection AddComment(string comment)
        {
            string tmp = comment.StartsWith("#") ? comment : "# " + comment;
            Comments.Add(tmp);
            return this;
        }

        // Returns the value corresponding to the given key or null
        public string? Get(string key) => Params switch
        {
            null => "",
            _ => Params.ContainsKey(key) ? (string?)Params[key] : null
        };        
    }
}
