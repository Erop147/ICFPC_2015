using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ICFPC2015.GameLogic.Logic
{
    public static class MagicWordsStore
    {
        public static string[] Words { get; private set; }

        static MagicWordsStore()
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationName.Contains("ConsoleRunner")
                ? Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "words.txt")
                : "words.txt";

            using (var reader = new StreamReader(path))
            {
                var words = reader.ReadToEnd().Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Words = words;
            }
        }

        public static void AddWords(IEnumerable<string> words)
        {
            Words = Words.Union(words).ToArray();
        }
    }
}