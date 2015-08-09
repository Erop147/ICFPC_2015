using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ICFPC2015.GameLogic.Logic
{
    public static class MagicWordsStore
    {
        private const string resourceWithNamespace = "ICFPC2015.GameLogic.words.txt";
        public static string[] Words { get; private set; }

        static MagicWordsStore()
        {
            var resourceAssembly = Assembly.GetAssembly(typeof(MagicWordsStore));
            using (var stream = resourceAssembly.GetManifestResourceStream(resourceWithNamespace))
            {
                if (stream == null)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Can't get resource:{0} from assembly:{1}", resourceWithNamespace, resourceAssembly.FullName));
                }

                using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    var words = streamReader.ReadToEnd()
                        .ToLowerInvariant()
                        .Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Words = words;
                }
            }
        }

        public static void AddWords(IEnumerable<string> words)
        {
            Words = Words.Union(words).Select(w => w.ToLowerInvariant()).Where(IsGood).ToArray();
        }

        private static bool IsGood(string word)
        {
            var anyEmpty = word.Select(CommandConverter.Convert).Any(x => x == Command.Empty);
            var allHorizontal = word.Select(CommandConverter.Convert).All(x => x == Command.MoveEast || x == Command.MoveWest || x == Command.Empty);
            return !allHorizontal && !anyEmpty;
        }
    }
}