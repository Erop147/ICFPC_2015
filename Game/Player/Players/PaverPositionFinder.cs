using System.Linq;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public class PaverPositionFinder : IBestPositionFinder
    {
        public GameUnit FindBest(GameUnit[] positions, Game game)
        {
            var orderedPositions = positions
                .Select(x =>
                    new
                    {
                        GameUnit = x,
                        Profit = GetScore(x.UnitPosition, game)
                    })
                .OrderByDescending(x => x.Profit.BusyRows)
                .ThenByDescending(x => x.Profit.DiverScore + x.Profit.ReachableCount + x.Profit.DensityScore);

            return orderedPositions
                .First()
                .GameUnit;
        }

        public string Name { get { return "Paver"; } }

        private class Profit
        {
            public int BusyRows { get; set; }
            public int DiverScore { get; set; }
            public int ReachableCount { get; set; }
            public int DensityScore { get; set; }
        }

        private Profit GetScore(UnitPosition position, Game game)
        {
            var unit = game.Current.Unit;
            var points = new GameUnit(unit, position).GetAbsolutePoints();
            var board = game.Board.Clone();

            foreach (var point in points)
            {
                board.Fill(point);
            }

            var diverScore = 0;
            for (int i = 0; i < board.Height; i++)
            {
                for (int j = 0; j < board.Width; j++)
                {
                    if (board.Field[i][j] == CellState.Busy)
                    {
                        diverScore += i * i;
                    }
                }
            }

            var busyCount = 0;
            for (var i = 0; i < board.Height; i++)
            {
                if (board.Field[i].All(x => x == CellState.Busy))
                {
                    busyCount++;
                }
            }

            return new Profit
            {
                BusyRows = busyCount,
                DiverScore = diverScore,
                DensityScore = CalcDensity(board, points)
            };
        }

        private int CalcDensity(Board board, Point[] points)
        {
            var ret = 0;
            for (int i = 0; i < points.Length; i++)
            {
                var point = points[i];
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int ni = point.Row + dx, nj = point.Col + dy;
                        if (0 <= ni && ni < board.Height &&
                            0 <= nj && nj < board.Width)
                        {
                            if (board.Field[ni][nj] == CellState.Free)
                                continue;
                            if (IsNeighbors(point, nj, ni))
                            {
                                ret++;
                            }
                        }
                    }
                }
            }

            return ret;
        }

        private bool IsNeighbors(Point point1, int col2, int row2)
        {
            var point2 = new Point(col2, row2);
            if (Commands.Any(c => point1.MakeStep(c).Equals(point2.Row, point2.Col)))
                return true;
            if (Commands.Any(c => point2.MakeStep(c).Equals(point1.Row, point1.Col)))
                return true;
            return false;
        }

        private static readonly Command[] Commands = { Command.MoveEast, Command.MoveWest, Command.MoveSouthEast, Command.MoveSouthWest };
    }
}