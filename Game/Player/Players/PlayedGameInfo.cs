namespace ICFPC2015.Player.Players
{
    public class PlayedGameInfo
    {
        public string Commands { get; private set; }
        public int Score { get; private set; }
 
        public PlayedGameInfo(string commands, int score)
        {
            Commands = commands;
            Score = score;
        }
    }
}
