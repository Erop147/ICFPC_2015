using System.Drawing;

namespace ICFPC2015.Visualizer
{
    public struct Hexagon
    {
        public static readonly Color DefaultColor = Color.White;
        public static readonly Color UnitColor = Color.YellowGreen;
        public static readonly Color LockedColor = Color.DeepSkyBlue;

        public Hexagon(
            Point point1,Point point2,Point point3,
            Point point4,Point point5,Point point6,
            Point circleStart, int circleHeight,
            Color color, int circleRadius,
            string text, int fontSize)
            : this()
        {
            Point1 = point1;
            Point2 = point2;
            Point3 = point3;
            Point4 = point4;
            Point5 = point5;
            Point6 = point6;
            Color = color;
            CircleRadius = circleRadius;
            Text = text;
            FontSize = fontSize;
            Circle = new Rectangle(circleStart.X, circleStart.Y, circleHeight, circleHeight);
        }

        public Point Point1 { get; private set; }
        public Point Point2 { get; private set; }
        public Point Point3 { get; private set; }
        public Point Point4 { get; private set; }
        public Point Point5 { get; private set; }
        public Point Point6 { get; private set; }
        public Point Center { get; private set; }

        public Color Color { get; private set; }
        public bool HasColor { get { return Color != DefaultColor; } }

        public int CircleRadius { get; private set; }
        public bool HasCircle { get { return CircleRadius > 0; } }
        public Rectangle Circle { get; private set; }

        public string Text { get; set; }
        public int FontSize { get; set; }
    }
}