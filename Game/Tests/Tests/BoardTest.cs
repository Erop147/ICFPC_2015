using System;
using ICFPC2015.Game.Logic;
using NUnit.Framework;

namespace Game.Tests
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
            Assert.AreEqual(4, actual);

            var field = board.ToString();
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
    }
}