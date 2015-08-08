using System;
using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void TestGetPivotLocation()
        {
            var game = new Game(Board.CreateEmpty(7, 6), null, null, 0);
            var unit = Unit.Create(new Point(0, 0), new[]
            {
                new Point(2, -2), new Point(-1, -1), new Point(0, -1),
                new Point(-1, 0), new Point(1, 0),
                new Point(0, 1), new Point(2, 1),
            });

            var actual = game.GetPivotLocation(unit);
            Assert.AreEqual(2, actual.Row);
            Assert.AreEqual(2, actual.Col);

            var gameUnit = new GameUnit(unit, actual, 0);
            Assert.IsTrue(game.IsValid(gameUnit));
            Console.WriteLine(game.Board.Place(gameUnit.GetAbsolutePoints()).ToString());
        }

        [Test]
        public void TestGetPivotLocation2()
        {
            var game = new Game(Board.CreateEmpty(6, 7), null, null, 0);
            var unit = Unit.Create(new Point(-1, -1), new[]
            {
                new Point(2, 1), new Point(1, 2), new Point(1, 3), new Point(2, 3), new Point(2, 4), new Point(2, 5), 
            });

            var actual = game.GetPivotLocation(unit);
            Assert.AreEqual(0, actual.Row);
            Assert.AreEqual(2, actual.Col);

            var gameUnit = new GameUnit(unit, actual, 0);
            Assert.IsTrue(game.IsValid(gameUnit));
            Console.WriteLine(game.Board.Place(gameUnit.GetAbsolutePoints()).ToString());
        }

        [Test]
        public void TestGetPivotLocation3()
        {
            var game = new Game(Board.CreateEmpty(5, 7), null, null, 0);
            var unit = Unit.Create(new Point(2, 4), new[]
            {
                new Point(2, 1), new Point(5, 1), new Point(1, 2), new Point(3, 2), new Point(4, 2), new Point(6, 2), new Point(7, 2), new Point(4, 3), new Point(5, 3), 
            });

            var actual = game.GetPivotLocation(unit);
            Assert.AreEqual(-5, actual.Row);
            Assert.AreEqual(-3, actual.Col);

            var gameUnit = new GameUnit(unit, actual, 0);
            Assert.IsTrue(game.IsValid(gameUnit));
            Console.WriteLine(game.Board.Place(gameUnit.GetAbsolutePoints()).ToString());
        }

        [Test]
        public void TestIsValid()
        {
            var game = new Game(Board.Create(new[]
            {
                ".........",
                "....*....",
                "...*.*...",
                "....*....",
                ".........",
                ".........",
            }), null, null, 0);

            var actual = game.IsValid(new GameUnit(
                Unit.Create(new Point(0, 0), new[] { new Point(3, 1), new Point(5, 1), new Point(4, 2), new Point(3, 3), new Point(5, 3) }),
                new Point(0, 0),
                0
            ));
            Assert.IsTrue(actual);

            actual = game.IsValid(new GameUnit(
                Unit.Create(new Point(0, 0),
                    new[] { new Point(3, 1), new Point(5, 1), new Point(4, 2), new Point(3, 3), new Point(5, 3), new Point(3, 2), }),
                new Point(0, 0),
                0
                ));
            Assert.IsFalse(actual);
        }

        [Test]
        public void TestTrySpawnNew()
        {
            var board = Board.CreateEmpty(10, 5);
            var unit = Unit.Create(new Point(0, 0), new[] { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), });
            var game = new Game(board, null, new[] { unit }, 0);

            var actual = game.TrySpawnNew();

            Assert.AreEqual(StepResult.Ok, actual.Result);
        }

        [Test]
        public void TestTrySpawnNew2()
        {
            var board = Board.CreateEmpty(10, 5);
            var game = new Game(board, null, new Unit[0], 0);

            var actual = game.TrySpawnNew();

            Assert.AreEqual(StepResult.GameOver, actual.Result);
        }

        [Test]
        public void TestTrySpawnNew3()
        {
            var board = Board.Create(new[]
            {
                "..*...",
                "......",
                "......"
            });
            var unit = Unit.Create(new Point(0, 0), new[] { new Point(-1, 0), new Point(0, 0), new Point(1, 0), new Point(0, 1), });
            var game = new Game(board, null, new[] { unit }, 0);

            var actual = game.TrySpawnNew();

            Assert.AreEqual(StepResult.GameOver, actual.Result);
        }

        [Test]
        public void TestMakeCommand()
        {
            var board = Board.Create(new[]
            {
                "...",
                "...",
                "..."
            });
            var unit = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var unit2 = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var game = new Game(board, null, new[] { unit, unit2 }, 0);

            var actual = game.TrySpawnNew();
            Assert.AreEqual(StepResult.Ok, actual.Result);

            actual = actual.Game.TryMakeStep(Command.MoveWest);
            Assert.AreEqual(StepResult.Ok, actual.Result);

            actual = actual.Game.TryMakeStep(Command.MoveWest);
            Assert.AreEqual(StepResult.Lock, actual.Result);

            Console.WriteLine(actual.Game.Board);
        }
    }
}