using System;
using System.Collections.Generic;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public class GameUnit
    {
        public Unit Unit { get; private set; }
        public UnitPosition UnitPosition { get; private set; }

        private readonly Lazy<IEnumerable<Point>> absolutePoints;
        private readonly Lazy<int> hashCode;

        public GameUnit(Unit unit, UnitPosition unitPosition)
        {
            Unit = unit;
            UnitPosition = unitPosition;

            absolutePoints = new Lazy<IEnumerable<Point>>(() => Unit.Points.Select(p => p.Rotate(UnitPosition.RotationCount, Unit.PivotPoint).Move(Unit.PivotPoint, UnitPosition.PivotLocation)));
            hashCode = new Lazy<int>(() => GetOrderedPoints().Select((x, i) => ((x.Row * 1433) ^ (x.Col * 3571)) * i).Aggregate(0, (x, y) => x ^ y));
        }

        public IEnumerable<Point> GetAbsolutePoints()
        {
            return absolutePoints.Value;
        }

        public GameUnit Clone()
        {
            return new GameUnit(Unit.Clone(), UnitPosition.Clone());
        }

        public GameUnit MakeStep(Command command)
        {
            switch (command)
            {
                case Command.MoveEast:
                {
                    return new GameUnit(Unit, UnitPosition.Move(new Point(1, 0)));
                }
                case Command.MoveWest:
                {
                    return new GameUnit(Unit, UnitPosition.Move(new Point(-1, 0)));
                }
                case Command.MoveSouthEast:
                {
                    if (UnitPosition.PivotLocation.Row % 2 == 0)
                        return new GameUnit(Unit, UnitPosition.Move(new Point(0, 1)));
                    return new GameUnit(Unit, UnitPosition.Move(new Point(1, 1)));
                }
                case Command.MoveSouthWest:
                {
                    if (UnitPosition.PivotLocation.Row % 2 == 0)
                        return new GameUnit(Unit, UnitPosition.Move(new Point(-1, 1)));
                    return new GameUnit(Unit, UnitPosition.Move(new Point(0, 1)));
                }
                case Command.TurnClockWise:
                {
                    return new GameUnit(Unit, UnitPosition.Rotate(1));
                }
                case Command.TurnCounterClockWise:
                {
                    return new GameUnit(Unit, UnitPosition.Rotate(-1));
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
            return hashCode.Value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as GameUnit;
            if (other == null)
                return false;
            if (other.Unit.Points.Length != Unit.Points.Length)
                return false;

            var pointEnumerator = GetOrderedPoints().GetEnumerator();
            var otherPointEnumerator = other.GetOrderedPoints().GetEnumerator();
            for (var i = 0; i < Unit.Points.Length; i ++)
            {
                pointEnumerator.MoveNext();
                otherPointEnumerator.MoveNext(); 
                
                var point = pointEnumerator.Current;
                var otherPoint = otherPointEnumerator.Current;
                if (!point.Equals(otherPoint.Row, otherPoint.Col))
                {
                    return false;
                }
            }
            return true;
        }

        public string ToCoordinatesString()
        {
            return string.Join(";", GetOrderedPoints().Select(x => x.ToString()));
        }

        private IEnumerable<Point> GetOrderedPoints()
        {
            return GetAbsolutePoints().OrderBy(x => x.Col).ThenBy(x => x.Row).Concat(new[] { UnitPosition.PivotLocation });
        }
    }
}