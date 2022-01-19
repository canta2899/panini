using System.Text;
using System.Collections;

namespace Panini 
{
    public static class IniParser
    {

        // Check if the ini file exists, otherwise throws an exception

        // Builds a section from a name, parameters and comments
        private static IniSection GetIniSection(string name, Hashtable pars, List<string> comments)
        {
            return new IniSection(name, pars, comments);
        }

        // Writes the ini file to the given path
        public static void Write(ParsedIni file)
        {
            StringBuilder sb = new StringBuilder();

            List<IniSection> sections = file.GetAllSections();

            foreach (IniSection s in sections)
            {
                WriteSection(s, ref sb);
            }

            WriteToFile(ref sb, file.Path);
        }

        // Performs the writing operation using a stream writer
        private static void WriteToFile(ref StringBuilder sb, string filePath)
        {
            using StreamWriter sw = new StreamWriter(filePath);

            sw.Write(sb.ToString());
        }

        // Builds a string with the corresponding section data
        private static void WriteSection(IniSection s, ref StringBuilder sb)
        {
            sb.AppendLine($"[{s.Name}]");

            foreach (string comment in s.Comments)
            {
                sb.AppendLine($"{comment}");
            }

            if (s.Params != null)
            {
                foreach (DictionaryEntry pair in s.Params)
                {
                    sb.AppendLine($"{pair.Key} = {pair.Value}");
                }
            }


            sb.AppendLine();
        }

        // Parses the Ini file at the given path
       public static ParsedIni Parse(string path)
       {

            // For each Ini Section
            List<IniSection> parsedContent = new List<IniSection>();

            // Opens file for reading
            using StreamReader fileIni = new StreamReader(path);

            ParseFile(ref parsedContent, fileIni);

            return new ParsedIni(parsedContent, path);
        }

        // Performs the line by line parsing
        private static void ParseFile(ref List<IniSection> parsedContent, StreamReader fileIni)
        {

            // Params without sections are under ROOT sections
            string? strLine = null;
            string currentRoot = "Root";
            string[] keyPair;
            List<string> comments = new List<string>();
            Hashtable h = new Hashtable();


            // Reads file line by line
            while ((strLine = fileIni.ReadLine()) != null)
            {
                strLine = strLine.Trim();

                if (strLine == "") continue;
                if (strLine.StartsWith("#"))
                {
                    comments.Add(strLine);
                }

                if (strLine.StartsWith("[") && strLine.EndsWith("]"))
                {
                    if (currentRoot != null && h.Count > 0)
                    {
                        // Current section done
                        parsedContent.Add(GetIniSection(currentRoot, h, comments));
                    }

                    // new section 
                    currentRoot = strLine.Substring(1, strLine.Length - 2);
                    h = new Hashtable();
                    comments = new List<string>();

                    continue;  // next line
                }

                // Splits key and value by '='
                try
                {
                    var rawPair = strLine.Split(new char[] { '=' }, 2);
                    keyPair = Array.ConvertAll(rawPair, el => el.Trim());

                    if (keyPair.Length > 1) h.Add(keyPair[0], keyPair[1]);
                }
                catch (Exception)
                {
                    // Skips line
                    continue;
                }
            }

            if (currentRoot != null) parsedContent.Add(GetIniSection(currentRoot, h, comments));
        }
    } // End class IniParser
}
