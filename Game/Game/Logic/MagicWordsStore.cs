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
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var path = assembly.FullName.Contains("ConsoleRunner")
                ? Path.Combine(Path.GetDirectoryName(assembly.Location), "words.txt")
                : "words.txt";

            using (var reader = new StreamReader(path))
            {
                var words = reader.ReadToEnd().Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                Words = words;
            }
        }

        public static void AddWords(IEnumerable<string> words)
        {
            Words = Words.Union(words).Where(IsGood).ToArray();
        }

        private static bool IsGood(string word)
        {
            var anyEmpty = word.Select(CommandConverter.Convert).Any(x => x == Command.Empty);
            var allHorizontal = word.Select(CommandConverter.Convert).All(x => x == Command.MoveEast || x == Command.MoveWest || x == Command.Empty);
            return !allHorizontal && !anyEmpty;
        }
    }
}