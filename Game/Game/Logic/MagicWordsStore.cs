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
            using (var reader = new StreamReader("words.txt"))
            {
                var words = reader.ReadToEnd().Split(new [] { " ", "\n", "\r\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                Words = words;
            }
        }

        public static void AddWords(IEnumerable<string> words)
        {
            Words = Words.Union(words).ToArray();
        }
    }
}