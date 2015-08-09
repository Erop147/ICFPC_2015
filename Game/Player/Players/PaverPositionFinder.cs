using System.Linq;
using ICFPC2015.GameLogic.Logic;

namespace ICFPC2015.Player.Players
{
    public class PaverPositionFinder : IBestPositionFinder
    {
        public UnitPosition FindBest(UnitPosition[] positions, Game game)
        {
            var orderedPositions = positions
                .Select(x =>
                    new
                    {
                        Position = x,
                        Profit = GetScore(x, game)
                    })
                .OrderByDescending(x => x.Profit.BusyRows)
                .ThenByDescending(x => x.Profit.DiverScore + x.Profit.ReachableCount * game.Board.Height * game.Board.Height);

            return orderedPositions
                .First()
                .Position;
        }

        public string Name { get { return "Paver"; } }

        private class Profit
        {
            public int BusyRows { get; set; }
            public int DiverScore { get; set; }
            public int ReachableCount { get; set; }
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
            for (int i = 0; i < board.Height; i ++)
            {
                for (int j = 0; j < board.Width; j ++)
                {
                    if (board.Field[i][j] == CellState.Busy)
                    {
                        diverScore += i * i;
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

            var reachableCount = CountRechable(board);
            
            return new Profit
            {
                BusyRows = busyCount,
                DiverScore = diverScore,
                ReachableCount = reachableCount
            };
        }

        private int CountRechable(Board board)
        {
            var used = new bool[board.Height, board.Width];
            var count = Enumerable.Range(0, board.Width).Sum(col => Dfs(0, col, used, board.Height, board.Width));
            return count;
        }

        private static readonly int[] dx = {-1, 1, 0, 0};
        private static readonly int[] dy = {0, 0, -1, 1};

        private int Dfs(int row, int col, bool[,] used, int height, int width)
        {
            if (used[row, col])
                return 0;
            used[row, col] = true;
            return Enumerable.Range(0, 4)
                             .Select(i => new {Row = row + dx[i], Col = col + dy[i]})
                             .Where(x => CanGo(x.Row, x.Col, used, height, width))
                             .Sum(x => Dfs(x.Row, x.Col, used, height, width));
        }

        private bool CanGo(int row, int col, bool[,] used, int height, int width)
        {
            return 0 <= row && row < height &&
                   0 <= col && col < width &&
                   !used[row, col];
        }
    }
}