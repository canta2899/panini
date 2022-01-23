using System.Collections;
using System.Collections.Generic;

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
        public IniSection? TryAdd(string key, string value)
        {
            if (!Params.ContainsKey(key))
            {
                Params.Add(key, value);
                return this;
            }

            return null;
        }

        public IniSection Add(string key, string value)
        {
            Params.Add(key, value);
            return this;
        }

        public IniSection Set(string key, string value)
        {
            if (!Params.ContainsKey(key))
            {
                this.Add(key, value);
            }
            else
            {
                this.Params[key] = value;
            }

            return this;
        }

        public IniSection Remove(string key)
        {
            if (Params.ContainsKey(key))
            {
                Params.Remove(key);
            }

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
            null => null,
            _ => Params.ContainsKey(key) ? (string?)Params[key] : null
        };
    }
}
