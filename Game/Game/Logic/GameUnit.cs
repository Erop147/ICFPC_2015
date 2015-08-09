using System;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public class GameUnit
    {
        public Unit Unit { get; private set; }
        public UnitPosition UnitPosition { get; private set; }

        private readonly Lazy<Point[]> orderedPoints;
        private readonly Lazy<Point[]> absolutePoints;
        private readonly Lazy<int> hashCode;

        public GameUnit(Unit unit, UnitPosition unitPosition)
        {
            Unit = unit;
            UnitPosition = unitPosition;

            absolutePoints = new Lazy<Point[]>(() => Unit.Points.Select(p => p.Rotate(UnitPosition.RotationCount, Unit.PivotPoint).Move(Unit.PivotPoint, UnitPosition.PivotLocation)).ToArray());
            orderedPoints = new Lazy<Point[]>(() => GetAbsolutePoints().OrderBy(x => x.Col).ThenBy(x => x.Row).Concat(new[] { UnitPosition.PivotLocation }).ToArray());
            hashCode = new Lazy<int>(() => GetOrderedPoints().Select((x, i) => ((x.Row * 1) ^ (x.Col * 3571)) * i).Aggregate(0, (x, y) => x ^ y));
        }

        public Point[] GetAbsolutePoints()
        {
            return absolutePoints.Value;
        }

        private Point[] GetOrderedPoints()
        {
            return orderedPoints.Value;
        }

        public override int GetHashCode()
        {
            return hashCode.Value;
        }

        public GameUnit MakeStep(Command command)
        {
            return new GameUnit(Unit, UnitPosition.MakeStep(command));
        }

        public Point BottomLeft()
        {
            return GetAbsolutePoints().OrderByDescending(x => x.Row).ThenBy(x => x.Col).First();
        }

        public override bool Equals(object obj)
        {
            var other = obj as GameUnit;
            if (other == null)
                return false;
            if (other.Unit.Points.Length != Unit.Points.Length)
                return false;

            var points = GetOrderedPoints();
            var otherPoints = other.GetOrderedPoints();
            for (var i = 0; i < Unit.Points.Length; i++)
            {
                var point = points[i];
                var otherPoint = otherPoints[i];
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
    }
}