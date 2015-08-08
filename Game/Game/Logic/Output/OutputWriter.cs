using Newtonsoft.Json;

namespace ICFPC2015.GameLogic.Logic.Output
{
    public class OutputWriter
    {
        public string GenWriteString(Output[] output)
        {
            return JsonConvert.SerializeObject(output);
        }
    }
}