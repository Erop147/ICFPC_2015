using System.Collections.Generic;
using ICFPC2015.GameLogic.Logic;
using ICFPC2015.Player.Implementation;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.Player
{
    [TestFixture]
    public class ReachableStatesGetterTest
    {
        [Test]
        public void TestGet()
        {
            var board = Board.Create(new string[]
                {
                    "...",
                    "*.*",
                    "..."
                });
            var unit = Unit.Create(new Point(0, 0), new[] {new Point(0, 0), new Point(0, 1)});
            var gameUnit = new GameUnit(unit, new UnitPosition(new Point(1, 0), 0));
            var actual = ReachableStatesGetter.Get(board, gameUnit, false);

            CollectionAssert.AreEquivalent(new[]
                {
                    new UnitPosition(new Point(1, 0), 0),
                    new UnitPosition(new Point(1, 0), 5),
                    new UnitPosition(new Point(0, 0), 5),
                    new UnitPosition(new Point(1, 1), 0),
                    new UnitPosition(new Point(1, 1), 1)
                }, actual);
        }

        [Test]
        public void TestGetWithUsedPositions()
        {
            var board = Board.Create(new string[]
                {
                    "...",
                    "*.*",
                    "..."
                });
            var unit = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), new Point(0, 1) });
            var gameUnit = new GameUnit(unit, new UnitPosition(new Point(1, 0), 0));
            var usedPositions = new HashSet<UnitPosition>()
                {
                    new UnitPosition(new Point(1, 1), 0)
                };
            var actual = ReachableStatesGetter.Get(board, gameUnit, false, usedPositions);

            CollectionAssert.AreEquivalent(new[]
                {
                    new UnitPosition(new Point(1, 0), 0),
                    new UnitPosition(new Point(1, 0), 5),
                    new UnitPosition(new Point(0, 0), 5),
                }, actual);
        }
    }
}