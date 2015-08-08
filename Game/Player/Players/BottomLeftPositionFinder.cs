using System.Linq;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public class BottomLeftPositionFinder : IBestPositionFinder
    {
        public GameUnit FindBest(GameUnit[] gameUnits, Game game)
        {
            return gameUnits.Select(x => new {BottomLeft = new GameUnit(game.Current.Unit, x.UnitPosition).BottomLeft(), GameUnit = x})
                            .OrderByDescending(x => x.BottomLeft.Row)
                            .ThenBy(x => x.BottomLeft.Col)
                            .First()
                            .GameUnit;
        }
    }
}