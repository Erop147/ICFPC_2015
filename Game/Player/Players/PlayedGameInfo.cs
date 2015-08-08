namespace ICFPC2015.Player.Players
{
    public class PlayedGameInfo
    {
        public string Commands { get; private set; }
        public int Score { get; private set; }
        public string PlayerName { get; private set; }
 
        public PlayedGameInfo(string commands, int score, string playerName)
        {
            Commands = commands;
            Score = score;
            PlayerName = playerName;
        }
    }
}
