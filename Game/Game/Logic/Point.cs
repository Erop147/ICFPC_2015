using System;

namespace ICFPC2015.GameLogic.Logic
{
    public struct Point
    {
        public Point(int col, int row) : this()
        {
            Row = row;
            Col = col;
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
            if ((newCol & 1) != 0)
            {
                newCol -= Mod2(newCol);
            }
            newCol /= 2;

            return new Point(newCol, newRow);
        }

        public Point Move(Point p, Point a)
        {
            var newRow = Row + (a.Row - p.Row);
            var newCol = 2 * Col + 2 * (a.Col - p.Col) + (Mod2(Row) + Mod2(a.Row) - Mod2(p.Row));
            if (newCol % 2 != 0)
            {
                newCol -= Mod2(newCol);
            }
            newCol /= 2;

            return new Point(newCol, newRow);
        }

        public bool Equals(int row, int col)
        {
            return Row == row && Col == col;
        }

        private static int Mod2(int x)
        {
            return x < 0 ? ((-x) & 1) : (x & 1);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Col, Row);
        }
    }
}