using System;
using ICFPC2015.Game.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests
{
    [TestFixture]
    public class BoardTest
    {
        [Test]
        public void TestUpdate()
        {
            var board = Board.Create(new[]
            {
                "*******",
                "*******",
                ".*..**.",
                "**.****",
                "..*....",
                "*******",
                "*******",
                "..*....",
            });

            var actual = board.Update();
            Assert.AreEqual(4, actual.RowsCleaned);

            var field = actual.NewBoard.ToString();
            Assert.AreEqual(string.Join(Environment.NewLine, new[]
            {
                ".......",
                ".......",
                ".......",
                ".......",
                ".*..**.",
                "**.****",
                "..*....",
                "..*...."
            }), field);
        }

        [Test]
        public void TestClone()
        {
            var board = Board.Create(new []
            {
                "..",
                ".*"
            });

            var board2 = board.Clone();
            board2.Field[0][0] = CellState.Busy;
            Assert.AreEqual(CellState.Free, board.Field[0][0]);
        }
    }
}