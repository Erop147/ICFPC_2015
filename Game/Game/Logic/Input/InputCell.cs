namespace ICFPC2015.GameLogic.Logic.Input
{
    public class InputCell
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point ToPoint()
        {
            return new Point(x, y);
        }
    }
}