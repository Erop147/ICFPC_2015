using System;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public struct Game
    {
        public int ProblemId { get; set; }
        public long Seed { get; set; }

        public readonly Unit[] UnitsSequence;
        public int CurrentUnitNumber { get; set; }
        public Board Board { get; private set; }
        public GameUnit Current { get; private set; }
        public GameState State { get; private set; }

        public int LastUnitLinesCleared { get; private set; }
        public int Score { get; private set; }

        public Game(Board board, GameUnit current, Unit[] unitsSequence, int currentUnitNumber, int lastUnitLinesCleared, int score, int problemId, long seed)
            : this()
        {
            Board = board;
            Current = current;
            UnitsSequence = unitsSequence;
            CurrentUnitNumber = currentUnitNumber;
            LastUnitLinesCleared = lastUnitLinesCleared;
            Score = score;
            ProblemId = problemId;
            Seed = seed;
        }

        public Game TrySpawnNew()
        {
            if (Current != null)
            {
                throw new Exception("Current unit is not null");
            }
            if (CurrentUnitNumber == UnitsSequence.Length)
            {
                return GameOver();
            }
            var unit = UnitsSequence[CurrentUnitNumber];
            var pivot = GetPivotLocation(unit);
            var gameUnit = new GameUnit(unit, new UnitPosition(pivot, 0));

            if (!IsValid(gameUnit))
            {
                return GameOver();
            }

            return SpawnedNew(gameUnit);
        }

        public Point GetPivotLocation(Unit unit)
        {
            var row = - unit.Points.Min(x => x.Row);
            var minCol = unit.Points.Min(x => x.Col);
            var maxCol = unit.Points.Max(x => x.Col);
            var colShift = (Board.Width - (maxCol - minCol + 1)) / 2;
            var col = colShift - minCol;

            return new Point(col, row) - unit.PivotPoint;
        }

        public bool IsValid(GameUnit gameUnit)
        {
            return Board.IsValid(gameUnit);
        }

        public Game TryMakeStep(Command command)
        {
            if (Current == null)
            {
                throw new Exception("Current game unit is null");
            }

            if (!IsValid(Current))
            {
                throw new Exception("Current game unit is not valid, new step is impossible");
            }

            var newGameUnit = Current.MakeStep(command);
            if (!IsValid(newGameUnit))
            {
                var lockedCells = Current.GetAbsolutePoints();
                var updateResult = Board.Place(lockedCells).Update();
                var additionalScore = CalculateScore(LastUnitLinesCleared, updateResult.RowsCleaned, Current.Unit.Points.Length);
                var gameWithNewUnit = new Game(updateResult.NewBoard, null, UnitsSequence, CurrentUnitNumber + 1, updateResult.RowsCleaned, Score + additionalScore, ProblemId, Seed);
                return gameWithNewUnit.TrySpawnNew();
            }
            return MoveCurrent(newGameUnit);
        }

        private static int CalculateScore(int lastUnitRowsCleaned, int rowsCleaned, int size)
        {
            var points = size + 100 * (1 + rowsCleaned) * rowsCleaned / 2;
            var lineBonus = lastUnitRowsCleaned > 1 ? (lastUnitRowsCleaned - 1) * points / 10 : 0;
            return points + lineBonus;
        }

        private Game SpawnedNew(GameUnit gameUnit)
        {
            Current = gameUnit;
            State = GameState.NewIsSpawned;
            return this;
        }

        private Game MoveCurrent(GameUnit gameUnit)
        {
            Current = gameUnit;
            State = GameState.Ok;
            return this;
        }

        private Game GameOver()
        {
            State = GameState.GameOver;
            return this;
        }
    }
}