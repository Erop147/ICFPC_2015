namespace Game.Logic
{
    public struct BoardUpdateResult
    {
        public int RowsCleaned { get; set; }
        public Board NewBoard { get; set; }
    }
}