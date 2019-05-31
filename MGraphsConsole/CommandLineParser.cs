using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGraphsConsole
{
    public static class CommandLineParser
    {
        public static Dictionary<string, string> Parse(string[] args)
        {
            var result = new Dictionary<string, string>();
            foreach (var s in args)
            {
                if (!s.StartsWith("--"))
                    throw new Exception("Invalid arguments.");
                var tab = s.Substring(2).Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
                var key = tab[0];
                var value = tab.Length == 2 ? tab[1] : null;
                if (result.ContainsKey(key))
                    throw new Exception("Option repeats.");
                result.Add(key, value);
            }
            return result;
        }
    }
}
