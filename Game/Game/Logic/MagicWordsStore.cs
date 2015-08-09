using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public static class MagicWordsStore
    {
        public static string[] Words { get; private set; }

        static MagicWordsStore()
        {
            //var path = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "words.txt");
            var path = "words.txt";
            using (var reader = new StreamReader(path))
            {
                var words = reader.ReadToEnd().Split(new [] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Words = words;
            }
        }

        public static void AddWords(IEnumerable<string> words)
        {
            Words = Words.Union(words).ToArray();
        }
    }
}