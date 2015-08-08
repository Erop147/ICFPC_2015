using System;
using System.Linq;

namespace ICFPC2015.Game.Logic
{
    public struct Game
    {
        public readonly Unit[] UnitsSequence;
        public int CurrentUnitNumber { get; set; }
        public Board Board { get; private set; }
        public GameUnit Current { get; private set; }

        public Game(Board board, GameUnit current, Unit[] unitsSequence, int currentUnitNumber) 
            : this()
        {
            Board = board;
            Current = current;
            UnitsSequence = unitsSequence;
            CurrentUnitNumber = currentUnitNumber;
        }

        public GameStepResult TrySpawnNew()
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
            var gameUnit = new GameUnit(unit, pivot, 0);

            if (!IsValid(gameUnit))
            {
                return GameOver();
            }

            return MoveCurrent(gameUnit);
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
            foreach (var point in gameUnit.GetAbsolutePoints())
            {
                if (!Board.InField(point))
                {
                    return false;
                }
                if (Board.Field[point.Row][point.Col] == CellState.Busy)
                {
                    return false;
                }
            }
            return true;
        }

        public GameStepResult TryMakeStep(Command command)
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
                var newBoard = Board.Place(lockedCells).Update();
                var gameWithNewUnit = new Game(newBoard.NewBoard, null, UnitsSequence, CurrentUnitNumber + 1);
                return gameWithNewUnit.TrySpawnNew();
            }
            return MoveCurrent(newGameUnit);
        }

        private GameStepResult MoveCurrent(GameUnit gameUnit)
        {
            var game = this;
            game.Current = gameUnit;
            return new GameStepResult(game, StepResult.Ok);
        }

        private GameStepResult GameOver()
        {
            return new GameStepResult(this, StepResult.GameOver);
        }
    }
}