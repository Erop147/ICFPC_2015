using System;
using System.IO;

namespace ICFPC2015.GameLogic.Logic
{
    public class MagicWordsStore
    {
        public string[] Words { get; private set; }

        public MagicWordsStore()
        {
            using (var reader = new StreamReader("words.txt"))
            {
                var words = reader.ReadToEnd().Split(new [] { " ", "\n", "\r\n", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                Words = words;
            }
        }
    }
}