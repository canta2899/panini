using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Panini.Parser
{
    internal static class IniParser
    {

        // Writes the ini file to the given path
        internal static void Write(IniFile file, string path)
        {
            StringBuilder sb = new StringBuilder();

            file.GetAllSections().ForEach(x => WriteSection(x, ref sb));

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(sb.ToString());
            }
        }

        // Writes the ini file to the given path
        internal static void Write(IniFile file, TextWriter sw)
        {
            StringBuilder sb = new StringBuilder();

            file.GetAllSections().ForEach(x => WriteSection(x, ref sb));

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
        internal static IList<IniSection> Parse(string path)
        {

            // For each Ini Section
            IList<IniSection> parsedContent = new List<IniSection>();

            // Opens file for reading
            using (StreamReader fileIni = new StreamReader(path))
            {
                ParseFile(ref parsedContent, fileIni);
                return parsedContent;
            }
        }

        internal static IList<IniSection> Parse(TextReader sr)
        {
            IList<IniSection> parsedContent = new List<IniSection>();

            ParseFile(ref parsedContent, sr);

            return parsedContent;
        }


        // Performs the line by line parsing
        private static void ParseFile(ref IList<IniSection> parsedContent, TextReader fileIni)
        {
            // Params without sections are under ROOT sections
            string strLine = null;
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
                        parsedContent.Add(new IniSection(currentRoot, h, comments));
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

            if (currentRoot != null) parsedContent.Add(new IniSection(currentRoot, h, comments));
        }
    } // End class IniParser
}
