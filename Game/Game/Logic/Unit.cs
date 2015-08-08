using System.Linq;

namespace Game.Logic
{
    public struct Unit
    {
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
                Points = Points.Select(x => x).ToArray(),
                PivotPoint = PivotPoint
            };
        }

        public Point[] Points { get; private set; }
        public Point PivotPoint { get; private set; }
    }
}