using System;
using System.Linq;

namespace ICFPC2015.GameLogic.Logic
{
    public struct Board
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
                for (var j = 0; j < Width; j++)
                {
                    cells[i][j] = Field[i][j];
                }
            }

            return new Board
            {
                Height = Height,
                Field = cells,
                Width = Width
            };
        }

        public static Board CreateEmpty(int height, int width)
        {
            var cells = new CellState[height][];
            for (var i = 0; i < height; i++)
            {
                cells[i] = new CellState[width];
                for (var j = 0; j < width; j++)
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
            for (var i = 0; i < n; i++)
            {
                cells[i] = new CellState[m];
                for (int j = 0; j < m; j++)
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

        public Board Place(Point[] points)
        {
            var newBoard = Clone();
            foreach (var point in points)
            {
                newBoard.Fill(point);
            }
            return newBoard;
        }

        public void Fill(Point point)
        {
            Field[point.Row][point.Col] = CellState.Busy;
        }

        public BoardUpdateResult Update()
        {
            var newBoard = Clone();
            var busyRowCount = 0;
            for (var row = Height - 1; row >= 0; row--)
            {
                if (newBoard.RowIsBusy(row))
                {
                    busyRowCount++;
                }
                else
                {
                    newBoard.ShiftRowDown(row, busyRowCount);
                }
            }

            for (var row = 0; row < busyRowCount; row++)
            {
                newBoard.ClearRow(row);
            }

            return new BoardUpdateResult
            {
                NewBoard = newBoard,
                RowsCleaned = busyRowCount
            };
        }

        private bool RowIsBusy(int row)
        {
            return Field[row].All(x => x == CellState.Busy);
        }

        private void ClearRow(int row)
        {
            for (int col = 0; col < Width; col++)
            {
                Field[row][col] = CellState.Free;
            }
        }

        private void ShiftRowDown(int row, int delta)
        {
            if (delta == 0)
                return;

            for (var col = 0; col < Width; col++)
            {
                Field[row + delta][col] = Field[row][col];
            }
        }

        public bool InField(Point point)
        {
            return 0 <= point.Row && point.Row < Height &&
                   0 <= point.Col && point.Col < Width;
        }

        public bool IsValid(GameUnit gameUnit)
        {
            foreach (var point in gameUnit.GetAbsolutePoints())
            {
                if (!InField(point))
                {
                    return false;
                }
                if (Field[point.Row][point.Col] == CellState.Busy)
                {
                    return false;
                }
            }
            return true;
        }
    }
}