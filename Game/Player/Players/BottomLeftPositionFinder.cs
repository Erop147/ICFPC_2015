using System.Linq;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public class BottomLeftPositionFinder : IBestPositionFinder
    {
        public UnitPosition FindBest(UnitPosition[] positions, Game game)
        {
            return positions.Select(x => new {BottomLeft = new GameUnit(game.Current.Unit, x).BottomLeft(), Position = x})
                            .OrderByDescending(x => x.BottomLeft.Row)
                            .ThenBy(x => x.BottomLeft.Col)
                            .First()
                            .Position;
        }

        public string Name { get { return "BL"; } }
    }
}