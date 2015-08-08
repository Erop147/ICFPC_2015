using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public interface IPlayer
    {
        PlayedGameInfo Play(Game game);
    }
}