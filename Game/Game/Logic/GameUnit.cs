using System;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public class GameUnit
    {
        public Unit Unit { get; private set; }
        public Point PivotLocation { get; private set; }
        public int RotationCount { get; private set; }

        public GameUnit(Unit unit, Point pivotLocation, int rotationCount)
        {
            Unit = unit;
            PivotLocation = pivotLocation;
            RotationCount = rotationCount;
        }

        public Point[] GetAbsolutePoints()
        {
            return Unit.Points.Select(p => p.Rotate(RotationCount, Unit.PivotPoint) + Unit.PivotPoint + PivotLocation).ToArray();
        }

        public GameUnit Clone()
        {
            return new GameUnit(Unit.Clone(), PivotLocation, RotationCount);
        }

        public GameUnit MakeStep(Command command)
        {
            var result = Clone();
            switch (command)
            {
                case Command.MoveEast:
                {
                    result.PivotLocation += new Point(1, 0);
                    return result;
                }
                case Command.MoveWest:
                {
                    result.PivotLocation += new Point(-1, 0);
                    return result;
                }
                case Command.MoveSouthEast:
                {
                    if (result.PivotLocation.Row % 2 == 0)
                        result.PivotLocation += new Point(0, 1);
                    else
                        result.PivotLocation += new Point(1, 1);
                    return result;
                }
                case Command.MoveSouthWest:
                {
                    if (result.PivotLocation.Row % 2 == 0)
                        result.PivotLocation += new Point(-1, 1);
                    else
                        result.PivotLocation += new Point(0, 1);
                    return result;
                }
                case Command.TurnClockWise:
                {
                    result.RotationCount++;
                    return result;
                }
                case Command.TurnCounterClockWise:
                {
                    result.RotationCount--;
                    return result;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            }
            throw new NotImplementedException(string.Format("Command {0} is not implemented", command));
        }
    }
}