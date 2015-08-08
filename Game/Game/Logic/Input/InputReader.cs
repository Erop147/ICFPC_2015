using System.IO;
using Newtonsoft.Json;

namespace ICFPC2015.GameLogic.Logic.Input
{
    public class InputReader
    {
        public Input Read(string inputFilename)
        {
            using (var reader = new StreamReader(inputFilename))
            {
                var inputJson = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<Input>(inputJson);
            }
        }
    }
}