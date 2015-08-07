using System;
using System.Linq;

namespace Game.Logic
{
    public class Game
    {
        public Board Board { get; private set; }
        public GameUnit Current { get; private set; }

        public Game(Board board, GameUnit gameUnit)
        {
            Board = board;
            Current = gameUnit;
        }

        public GameStepResult TrySpawn(Unit unit)
        {
            if (Current != null)
            {
                throw new Exception("Current unit is not null");
            }

            var pivot = GetPivotLocation(unit);
            var gameUnit = new GameUnit(unit, pivot, 0);

            if (!IsValid(gameUnit))
            {
                return Result(null, StepResult.GameOver);
            }

            return Result(gameUnit, StepResult.Ok);
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

        public bool TryPlaceCurrent()
        {
            if (!IsValid(Current))
            {
                return false;
            }
            foreach (var point in Current.GetAbsolutePoints())
            {
                Board.Field[point.Row][point.Col] = CellState.Busy;
            }
            Current = null;
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
                return Result(Current, StepResult.Lock);
            }
        }

        private GameStepResult Result(GameUnit gameUnit, StepResult result)
        {
            var game = new Game(Board.Clone(), gameUnit);
            return new GameStepResult(game, result);
        }
    }
}