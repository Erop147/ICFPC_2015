using System.Linq;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public class PaverPositionFinder : IBestPositionFinder
    {
        public UnitPosition FindBest(UnitPosition[] positions, Game game)
        {
            return positions
                .Select(x =>
                    new
                    {
                        BottomLeft = new GameUnit(game.Current.Unit, x).BottomLeft(),
                        Position = x
                    })
                .OrderByDescending(x => x.BottomLeft.Row)
                .ThenBy(x => x.BottomLeft.Col)
                .First()
                .Position;
        }

        private int GetScore(UnitPosition position, Game game)
        {
            var unit = game.Current.Unit;
            var points = new GameUnit(unit, position).GetAbsolutePoints();
            var board = game.Board;

            foreach (var point in points)
            {
                board.Fill(point);
            }

            int score = 0;
            for (int i = 0; i < board.Height; i ++)
            {
                for (int j = 0; j < board.Width; j ++)
                {
                    if (board.Field[i][j] == CellState.Busy)
                    {
                        score += i * i;
                    }
                }
            }

            var busyCount = 0;
            for (var i = 0; i < board.Height; i ++)
            {
                if (board.Field[i].All(x => x == CellState.Busy))
                {
                    busyCount++;
                }
            }
            score += busyCount * 1000000;

            return score;
        }
    }
}