using System.Linq;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public class BottomPositionFinder : IBestPositionFinder
    {
        public GameUnit FindBest(GameUnit[] gameUnits, Game game)
        {
            return
                gameUnits.Select(
                            x => new
                                {
                                    Score = x.GetAbsolutePoints().Sum(point => point.Row*point.Row),
                                    GameUnit = x,
                                    BottomLeft = x.BottomLeft()
                                })
                         .OrderByDescending(x => x.BottomLeft.Row)
                         .ThenByDescending(x => x.Score)
                         .First()
                         .GameUnit;
        }
    }
}
