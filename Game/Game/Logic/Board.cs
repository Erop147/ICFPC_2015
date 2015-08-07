using System;
using System.Linq;

namespace ICFPC2015.Game.Logic
{
    public class Board
    {
        public int Height { get; private set; }
        public int Width { get; private set; }
        public CellState[][] Field { get; private set; }

        public Board Clone()
        {
            var cells = new CellState[Height][];
            for (var i = 0; i < Height; i++)
            {
                cells[i] = new CellState[Width];
                for (int j = 0; j < Width; j++)
                {
                    cells[i][j] = CellState.Free;
                }
            }

            return new Board
            {
                Height = Height,
                Field = cells,
                Width = Width
            };
        }

        public bool InField(Point point)
        {
            return 0 <= point.Row && point.Row < Height &&
                   0 <= point.Col && point.Col < Width;
        }

        public static Board CreateEmpty(int height, int width)
        {
            var cells = new CellState[height][];
            for (var i = 0; i < height; i++)
            {
                cells[i] = new CellState[width];
                for (int j = 0; j < width; j++)
                {
                    cells[i][j] = CellState.Free;
                }
            }

            return new Board
            {
                Height = height,
                Width = width,
                Field = cells
            };
        }

        public static Board Create(string[] field)
        {
            var n = field.Length;
            var m = field[0].Length;

            var cells = new CellState[n][];
            for (var i = 0; i < n; i ++)
            {
                cells[i] = new CellState[m];
                for (int j = 0; j < m; j ++)
                {
                    cells[i][j] = field[i][j] == '.' ? CellState.Free : CellState.Busy;
                }
            }

            return new Board
            {
                Height = n,
                Width = m,
                Field = cells
            };
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, Field.Select(x => string.Join("", x.Select(y => y == CellState.Busy ? "*" : "."))));
        }

        public int Update()
        {
            var busyRowCount = 0;
            for (var row = Height - 1; row >= 0; row --)
            {
                if (RowIsBusy(row))
                {
                    busyRowCount++;
                }
                else
                {
                    ShiftRowDown(row, busyRowCount);
                }
            }

            for (var row = 0; row < busyRowCount; row ++)
            {
                ClearRow(row);
            }

            return busyRowCount;
        }

        private bool RowIsBusy(int row)
        {
            return Field[row].All(x => x == CellState.Busy);
        }

        private void ClearRow(int row)
        {
            for (int col = 0; col < Width; col ++)
            {
                Field[row][col] = CellState.Free;
            }
        }

        private void ShiftRowDown(int row, int delta)
        {
            if (delta == 0)
                return;

            for (var col = 0; col < Width; col ++)
            {
                Field[row + delta][col] = Field[row][col];
            }
        }
    }
}