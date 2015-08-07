using System;
using Game.Logic;
using NUnit.Framework;

namespace Game.Tests
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void TestGetPivotLocation()
        {
            var game = new Logic.Game
            {
                Board = Board.CreateEmpty(7, 6)
            };
            var unit = Unit.Create(new Point(0, 0), new[]
            {
                new Point(2, -2), new Point(-1, -1), new Point(0, -1),
                new Point(-1, 0), new Point(1, 0),
                new Point(0, 1), new Point(2, 1),
            });

            var actual = game.GetPivotLocation(unit);
            Assert.AreEqual(2, actual.Row);
            Assert.AreEqual(2, actual.Col);

            game.Current = new GameUnit { PivotLocation = actual, RotationCount = 0, Unit = unit };

            Assert.IsTrue(game.TryPlaceCurrent());
            Console.WriteLine(game.Board.ToString());
        }

        [Test]
        public void TestGetPivotLocation2()
        {
            var game = new Logic.Game
            {
                Board = Board.CreateEmpty(6, 7)
            };
            var unit = Unit.Create(new Point(-1, -1), new[]
            {
                new Point(2, 1), new Point(1, 2), new Point(1, 3), new Point(2, 3), new Point(2, 4), new Point(2, 5), 
            });

            var actual = game.GetPivotLocation(unit);
            Assert.AreEqual(0, actual.Row);
            Assert.AreEqual(2, actual.Col);

            game.Current = new GameUnit { PivotLocation = actual, RotationCount = 0, Unit = unit };

            Assert.IsTrue(game.TryPlaceCurrent());
            Console.WriteLine(game.Board.ToString());
        }

        [Test]
        public void TestGetPivotLocation3()
        {
            var game = new Logic.Game
            {
                Board = Board.CreateEmpty(5, 7)
            };
            var unit = Unit.Create(new Point(2, 4), new[]
            {
                new Point(2, 1), new Point(5, 1), new Point(1, 2), new Point(3, 2), new Point(4, 2), new Point(6, 2), new Point(7, 2), new Point(4, 3), new Point(5, 3), 
            });

            var actual = game.GetPivotLocation(unit);
            Assert.AreEqual(-5, actual.Row);
            Assert.AreEqual(-3, actual.Col);

            game.Current = new GameUnit { PivotLocation = actual, RotationCount = 0, Unit = unit };

            Assert.IsTrue(game.TryPlaceCurrent());
            Console.WriteLine(game.Board.ToString());
        }

        [Test]
        public void TestIsValid()
        {
            var game = new Logic.Game
            {
                Board = Board.Create(new[]
                {
                    ".........",
                    "....*....",
                    "...*.*...",
                    "....*....",
                    ".........",
                    ".........",
                })
            };

            var actual = game.IsValid(new GameUnit
            {
                RotationCount = 0,
                PivotLocation = new Point(0, 0),
                Unit = Unit.Create(new Point(0, 0), new[] {new Point(3, 1), new Point(5, 1), new Point(4, 2), new Point(3, 3), new Point(5, 3)})
            });
            Assert.IsTrue(actual);

            actual = game.IsValid(new GameUnit
            {
                RotationCount = 0,
                PivotLocation = new Point(0, 0),
                Unit = Unit.Create(new Point(0, 0), new[] { new Point(3, 1), new Point(5, 1), new Point(4, 2), new Point(3, 3), new Point(5, 3), new Point(3, 2),  })
            });
            Assert.IsFalse(actual);
        }
    }
}