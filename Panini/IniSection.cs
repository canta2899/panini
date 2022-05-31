using System.Collections;
using System.Collections.Generic;

namespace Panini
{
    public class IniSection
    {
        // Represents the name of the section
        public string Name { get; internal set; }

        // Collection of comments inside the section
        public IList<string> Comments { get; internal set; }

        // Hashtable of the key-value pair of the section
        public Hashtable Params { get; }

        internal IniSection(string name, Hashtable parameters)
        {
            Name = name;
            Params = parameters;
            Comments = new List<string>();
        }

        internal IniSection(string name, Hashtable parameters, IList<string> comments)
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
        public IniSection TryAdd(string key, string value)
        {
            if (!Params.ContainsKey(key))
            {
                Params.Add(key, value);
                return this;
            }

            return null;
        }

        private IniSection Add(string key, string value)
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

        public IniSection TryRemove(string key)
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
            Comments.Add(comment);
            return this;
        }

        // Returns the value corresponding to the given key or null
        public string TryGet(string key)
        {
            switch (Params)
            {
                case null: return null;
                default: return Params.ContainsKey(key) ? (string)Params[key] : null;
            }
        }
    }
}
