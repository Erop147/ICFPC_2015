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

            //var reachableCount = 0;
            foreach (var point in points)
            {
                board.Fill(point);
                //if (point.Row == 0)
                //{
                //    reachableCount--;
                //}
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

            //reachableCount += CountRechable(board);
            
            return new Profit
            {
                BusyRows = busyCount,
                DiverScore = diverScore,
                ReachableCount = 0,//reachableCount
                DensityScore = CalcDensity(board)
            };
        }

        private int CalcDensity(Board board)
        {
            var ret = 0;
            for (int i = 0; i < board.Height; i ++)
            {
                for (int j = 0; j < board.Width; j ++)
                {
                    if (board.Field[i][j] == CellState.Free)
                        continue;
                    for (int dx = -1; dx <= 1; dx ++)
                    {
                        for (int dy = -1; dy <= 1; dy ++)
                        {
                            int ni = i + dx, nj = j + dy;
                            if (0 <= ni && ni < board.Height &&
                                0 <= nj && nj < board.Width)
                            {
                                if (board.Field[ni][nj] == CellState.Free)
                                    continue;
                                if (IsNeighbors(j, i, nj, ni))
                                {
                                    ret++;
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private bool IsNeighbors(int col1, int row1, int col2, int row2)
        {
            var point1 = new Point(col1, row1);
            var point2 = new Point(col2, row2);
            if (Commands.Any(c => point1.MakeStep(c).Equals(point2.Row, point2.Col)))
                return true;
            if (Commands.Any(c => point2.MakeStep(c).Equals(point1.Row, point1.Col)))
                return true;
            return false;
        }

        private bool[,] used = new bool[1000,1000];
        private CellState[][] field;
        private int height, width;

        private int CountRechable(Board board)
        {
            field = board.Field;
            height = board.Height;
            width = board.Width;
            for (int i = 0; i < height; i ++)
            {
                for (int j = 0; j < width; j ++)
                {
                    used[i,j] = false;
                }
            }

            var count = Enumerable.Range(0, board.Width).Sum(col => Dfs(0, col));
            return count;
        }

        private static readonly Command[] Commands = {Command.MoveEast, Command.MoveWest, Command.MoveSouthEast, Command.MoveSouthWest};

        private int Dfs(int row, int col)
        {
            if (used[row, col])
                return 0;
            used[row, col] = true;

            var cur = new Point(col, row);
            return (row > 0 ? 1 : 0) + Commands.Select(cur.MakeStep)
                                               .Where(x => CanGo(x.Row, x.Col))
                                               .Sum(x => Dfs(x.Row, x.Col));
        }

        private bool CanGo(int row, int col)
        {
            return 0 <= row && row < height &&
                   0 <= col && col < width &&
                   !used[row, col] &&
                   field[row][col] == CellState.Free;
        }
    }
}