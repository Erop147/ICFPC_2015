using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public interface IBestPositionFinder
    {
        UnitPosition FindBest(UnitPosition[] positions, Game game);
        string Name { get; }
    }
}