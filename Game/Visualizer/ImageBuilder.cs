using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ICFPC2015.GameLogic.Logic;
using Point = ICFPC2015.GameLogic.Logic.Point;

namespace ICFPC2015.Visualizer
{
    public class ImageBuilder
    {
        public Image Build(Game game, int multiplier)
        {
            var imageWidth = (4 * game.Board.Width + 2) * multiplier;
            var imageHeight = (3 * game.Board.Height + 1) * multiplier;

            Image boardImage = new Bitmap(imageWidth, imageHeight);
            using (var graphics = Graphics.FromImage(boardImage))
            using (Brush borderBrush = new SolidBrush(Color.Black))
            using (var borderPen = new Pen(borderBrush, 2))
            using (var unitBrush = new SolidBrush(Hexagon.UnitColor))
            using (var lockedBrush = new SolidBrush(Hexagon.LockedColor))
            {
                var hexagonFactory = new HexagonFactory(multiplier);
                for (var y = 0; y < game.Board.Field.Length; y++)
                {
                    for (int x = 0; x < game.Board.Field[y].Length; x++)
                    {
                        var cell = game.Board.Field[y][x];
                        var isLocked = cell == CellState.Busy;

                        var isCurrentUnitPoint = game.Current != null && IsCurrentUnitPoint(game.Current.GetAbsolutePoints(), x, y);
                        var isCurrentUnitPivot = game.Current != null && game.Current.UnitPosition.PivotLocation.Equals(y, x);

                        var hexagon = hexagonFactory.Create(x, y, isLocked, isCurrentUnitPoint, isCurrentUnitPivot);
                        DrawHexagon(hexagon, graphics, borderPen, unitBrush, lockedBrush, x, y);
                    }
                }
            }

            return boardImage;
        }

        private void DrawHexagon(Hexagon hexagon, Graphics graphics, Pen borderPen, Brush unitBrush, Brush lockedBrush, int x, int y)
        {
            if (hexagon.HasColor)
            {
                var brush = hexagon.Color == Hexagon.UnitColor ? unitBrush : lockedBrush;
                graphics.FillClosedCurve(brush, new[] { hexagon.Point1, hexagon.Point2, hexagon.Point3, hexagon.Point4, hexagon.Point5, hexagon.Point6 });
            }

            graphics.DrawLine(borderPen, hexagon.Point1, hexagon.Point2);
            graphics.DrawLine(borderPen, hexagon.Point2, hexagon.Point3);
            graphics.DrawLine(borderPen, hexagon.Point3, hexagon.Point4);
            graphics.DrawLine(borderPen, hexagon.Point4, hexagon.Point5);
            graphics.DrawLine(borderPen, hexagon.Point5, hexagon.Point6);
            graphics.DrawLine(borderPen, hexagon.Point6, hexagon.Point1);

            if (!string.IsNullOrEmpty(hexagon.Text))
            {
                graphics.DrawString(hexagon.Text, new Font(FontFamily.GenericMonospace, hexagon.FontSize), borderPen.Brush, hexagon.Point6.X, hexagon.Point6.Y);
            }

            if (hexagon.HasCircle)
            {
                graphics.DrawEllipse(borderPen, hexagon.Circle);
            }
        }

        private bool IsCurrentUnitPoint(IEnumerable<Point> points, int x, int y)
        {
            return points.Any(point => point.Col == x && point.Row == y);
        }
    }
}