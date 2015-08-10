using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public class Unit
    {
        public Unit(Point[] points, Point pivotPoint)
        {
            var row = points.Min(x => x.Row);
            var minCol = points.Min(x => x.Col);

            Points = points.Select(p => p.Move(new Point(minCol, row), new Point(0, 0))).ToArray();
            PivotPoint = pivotPoint.Move(new Point(minCol, row), new Point(0, 0));
        }

        public static Unit Create(Point pivotPoint, Point[] points)
        {
            return new Unit(points, pivotPoint);
        }

        public Point[] Points { get; private set; }
        public Point PivotPoint { get; private set; }
    }
}