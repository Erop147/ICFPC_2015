using System;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public class GameUnit
    {
        public Unit Unit { get; private set; }
        public UnitPosition UnitPosition { get; set; }

        public GameUnit(Unit unit, UnitPosition unitPosition)
        {
            Unit = unit;
            UnitPosition = unitPosition;
        }

        public Point[] GetAbsolutePoints()
        {
            return Unit.Points.Select(p => p.Rotate(UnitPosition.RotationCount, Unit.PivotPoint) + Unit.PivotPoint + UnitPosition.PivotLocation).ToArray();
        }

        public GameUnit Clone()
        {
            return new GameUnit(Unit.Clone(), UnitPosition.Clone());
        }

        public GameUnit MakeStep(Command command)
        {
            var result = Clone();
            switch (command)
            {
                case Command.MoveEast:
                {
                    result.UnitPosition = result.UnitPosition.Move(new Point(1, 0));
                    return result;
                }
                case Command.MoveWest:
                {
                    result.UnitPosition = result.UnitPosition.Move(new Point(-1, 0));
                    return result;
                }
                case Command.MoveSouthEast:
                {
                    if (result.UnitPosition.PivotLocation.Row % 2 == 0)
                        result.UnitPosition = result.UnitPosition.Move(new Point(0, 1));
                    else
                        result.UnitPosition = result.UnitPosition.Move(new Point(1, 1));
                    return result;
                }
                case Command.MoveSouthWest:
                {
                    if (result.UnitPosition.PivotLocation.Row % 2 == 0)
                        result.UnitPosition = result.UnitPosition.Move(new Point(-1, 1));
                    else
                        result.UnitPosition = result.UnitPosition.Move(new Point(0, 1));
                    return result;
                }
                case Command.TurnClockWise:
                {
                    result.UnitPosition = result.UnitPosition.Rotate(1);
                    return result;
                }
                case Command.TurnCounterClockWise:
                {
                    result.UnitPosition = result.UnitPosition.Rotate(-1);
                    return result;
                }
                default:
                {
                    throw new NotImplementedException(string.Format("Command {0} is not implemented", command));
                }
            }
        }

        public Point BottomLeft()
        {
            return GetAbsolutePoints().OrderByDescending(x => x.Row).ThenBy(x => x.Col).First();
        }

        public override int GetHashCode()
        {
            return ToCoordinatesString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as GameUnit;
            if (other == null)
                return false;
            return ToCoordinatesString() == other.ToCoordinatesString();
        }

        public string ToCoordinatesString()
        {
            var points = GetAbsolutePoints();
            return string.Join(";", points.OrderBy(x => x.Col).ThenBy(x => x.Row).Concat(new[] { UnitPosition.PivotLocation }).Select(x => x.ToString()));
        }
    }
}