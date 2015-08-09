namespace ICFPC2015.Player.Implementation
{
    public class DpInfo
    {
        public DpInfo(int value, State? nextState, string word, int extraMoves)
        {
            Value = value;
            NextState = nextState;
            Word = word;
            ExtraMoves = extraMoves;
        }

        public int Value { get; set; }
        public int ExtraMoves { get; set; }
        public State? NextState { get; set; }
        public string Word { get; set; }
    }
}