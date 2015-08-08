namespace ICFPC2015.GameLogic.Logic
{
    public class HistoryValidateResult
    {
        public int? WrongMoveNumber { get; set; }
        public bool IsValid { get; set; }
        public string Reason { get; set; }
    }
}