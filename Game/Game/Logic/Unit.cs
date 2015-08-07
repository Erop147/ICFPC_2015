using System.Linq;

namespace ICFPC2015.Game.Logic
{
    public class Unit
    {
        private Unit()
        {
        }

        public static Unit Create(Point pivotPoint, Point[] points)
        {
            return new Unit
            {
                Points = points,
                PivotPoint = pivotPoint
            };
        }

        public Unit Clone()
        {
            return new Unit
            {
                Points = Points.Select(x => x.Clone()).ToArray(),
                PivotPoint = PivotPoint.Clone()
            };
        }

        public Point[] Points { get; protected set; }
        public Point PivotPoint { get; protected set; }
    }
}