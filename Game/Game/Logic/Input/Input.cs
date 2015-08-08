namespace ICFPC2015.GameLogic.Logic.Input
{
    public class Input
    {
        public int id { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public InputUnit[] units { get; set; }
        public InputCell[] filled { get; set; }
        public int sourceLength { get; set; }
        public int[] sourceSeeds { get; set; }
    }
}