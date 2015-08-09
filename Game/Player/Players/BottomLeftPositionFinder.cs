using System.Linq;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public class BottomLeftPositionFinder : IBestPositionFinder
    {
        public GameUnit FindBest(GameUnit[] gameUnits, Game game)
        {
            return gameUnits.Select(x => new {BottomLeft = x.BottomLeft(), GameUnit = x})
                            .OrderByDescending(x => x.BottomLeft.Row)
                            .ThenBy(x => x.BottomLeft.Col)
                            .First()
                            .GameUnit;
        }
    }
}