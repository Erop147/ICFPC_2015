using System.Drawing;
using System.Windows.Forms;
using ICFPC2015.GameLogic.Logic;
using Point = ICFPC2015.GameLogic.Logic.Point;

namespace ICFPC2015.Visualizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeBoard();
            InitializeComponent();
        }

        private void InitializeBoard()
        {
            var board = Board.CreateEmpty(10, 5);
            var unit = new Unit(new[] {new Point(-1, 0), new Point(1, 0)}, new Point(0, 0));
            var game = new Game(board, null, new[] {unit}, 0, 0, 0, 0, 0, string.Empty, 0);

            DrowBoard(game);
        }

        private void DrowBoard(Game game)
        {
            Image board = new Bitmap(500, 1000);
            using (var graphics = Graphics.FromImage(board))
            using (Brush borderBrush = new SolidBrush(Color.Black))
            using (var borderPen = new Pen(borderBrush, 2))
            using (var unitBrush = new SolidBrush(Hexagon.UnitColor))
            using (var lockedBrush = new SolidBrush(Hexagon.LockedColor))
            {
                var hexagonFactory = new HexagonFactory(10);
                for (var y = 0; y < game.Board.Field.Length; y++)
                {
                    for (int x = 0; x < game.Board.Field[y].Length; x++)
                    {
                        var cell = game.Board.Field[y][x];
                        var isLocked = cell == CellState.Busy;

                        var isCurrentUnitPoint = IsCurrentUnitPoint(game.Current.Unit.Points, x, y);
                        var isCurrentUnitPivot = game.Current.Unit.PivotPoint.Equals(y, x);

                        var hexagon = hexagonFactory.Create(x, y, isLocked, isCurrentUnitPoint, isCurrentUnitPivot);
                        DrawHexagon(hexagon, graphics, borderPen, unitBrush, lockedBrush);
                    }
                }
            }
        }

        private void DrawHexagon(Hexagon hexagon, Graphics graphics, Pen borderPen, Brush unitBrush, Brush lockedBrush)
        {
            graphics.DrawLines(borderPen, new[] {hexagon.Point1, hexagon.Point2, hexagon.Point3, hexagon.Point4, hexagon.Point5, hexagon.Point6});

            if (hexagon.HasColor)
            {
                var brush = hexagon.Color == Hexagon.UnitColor ? unitBrush : lockedBrush;
                graphics.FillRectangle(brush, hexagon.Rectangle);
            }

            if (hexagon.HasCircle)
            {
                graphics.DrawEllipse(borderPen, hexagon.Circle);
            }
        }

        private bool IsCurrentUnitPoint(Point[] points, int x, int y)
        {
            for (var i = 0; i < points.Length; i++)
            {
                var point = points[i];
                if (point.Col == x && point.Row == y)
                    return true;
            }

            return false;
        }
    }
}
