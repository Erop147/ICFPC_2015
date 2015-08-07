using System;

namespace Game.Logic
{
    public class Point
    {
        private Point()
        {
            
        }

        public Point(int col, int row)
        {
            Row = row;
            Col = col;
        }

        public Point Clone()
        {
            return new Point(Col, Row);
        }

        public int Row { get; private set; }
        public int Col { get; private set; }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.Col + p2.Col, p1.Row + p2.Row);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.Col - p2.Col, p1.Row - p2.Row);
        }

        public Point Rotate(int count, Point pivot)
        {
            count = (count % 6 + 6) % 6;
            var result = this;
            for (var i = 0; i < count; i ++)
                result = result.RotateClockwise(pivot);
            return result;
        }

        public Point RotateClockwise(Point pivot)
        {
            var newRow = -pivot.Row - Col + pivot.Col + (pivot.Row + Mod2(pivot.Row) - Row - Mod2(Row)) / 2;
            newRow = -newRow;
            var newCol = 2 * pivot.Col + Mod2(pivot.Row) + (Col - pivot.Col) + (3 * pivot.Row - Mod2(pivot.Row) - 3 * Row + Mod2(Row)) / 2;
            if (newCol % 2 != 0)
            {
                newCol -= Mod2(newCol);
            }
            newCol /= 2;

            return new Point(newCol, newRow);
        }

        private static int Mod2(int x)
        {
            return Math.Abs(x) % 2;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Col, Row);
        }
    }
}