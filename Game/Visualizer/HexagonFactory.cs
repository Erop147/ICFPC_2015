using System.Drawing;

namespace ICFPC2015.Visualizer
{
    public class HexagonFactory
    {
        private readonly int multiplier;

        public HexagonFactory(int multiplier)
        {
            this.multiplier = multiplier;
        }

        public Hexagon Create(int x, int y, bool isLocked, bool isCurrentUnitPoint, bool isCurrentUnitPivot)
        {
            var x1 = (2 + 4*x + Fix(y))*multiplier;
            var y1 = (3*y)*multiplier;
            var point1 = new Point(x1, y1);

            var x2 = (4 + 4*x + Fix(y))*multiplier;
            var y2 = (1 + 3*y)*multiplier;
            var point2 = new Point(x2, y2);

            var x3 = (4 + 4*x + Fix(y))*multiplier;
            var y3 = (3 + 3*y)*multiplier;
            var point3 = new Point(x3, y3);

            var x4 = (2 + 4*x + Fix(y))*multiplier;
            var y4 = (4 + 3*y)*multiplier;
            var point4 = new Point(x4, y4);

            var x5 = (4*x + Fix(y))*multiplier;
            var y5 = (3 +3*y)*multiplier;
            var point5 = new Point(x5, y5);

            var x6 = (4*x + Fix(y))*multiplier;
            var y6 = (1 + 3*y)*multiplier;
            var point6 = new Point(x6, y6);

            var xCenter = (2 + 4*x + Fix(y) - 1)*multiplier;
            var yCenter = (2 + 3*y - 1)*multiplier;
            var center = new Point(xCenter, yCenter);

            var color = isLocked
                ? Hexagon.LockedColor 
                : (isCurrentUnitPoint ? Hexagon.UnitColor : Hexagon.DefaultColor);
            var radius = isCurrentUnitPivot ? multiplier : 0;

            var text = string.Format("{0};{1}", x, y);

            return new Hexagon(point1, point2, point3, point4, point5, point6, center, 2 * multiplier, color, radius, text, multiplier);
        }

        private int Fix(int y)
        {
            return y%2 == 1 ? 2 : 0;;
        }
    }
}