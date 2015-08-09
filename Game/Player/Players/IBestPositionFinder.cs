using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public interface IBestPositionFinder
    {
        GameUnit FindBest(GameUnit[] gameUnits, Game game);
        string Name { get; }
    }
}