using System;
using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class GameHistoryValidatorTest
    {
        [Test]
        [TestCase("lll", false)]
        [TestCase("d", false)]
        [TestCase("k", false)]
        [TestCase("llllll", true)]
        [TestCase("lllllll", false)]
        public void TestValidate(string commands, bool expected)
        {
            var validator = new GameHistoryValidator();

            var board = Board.Create(new[]
            {
                "...",
                "...",
                "..."
            });
            var unit = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var unit2 = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var unit3 = Unit.Create(new Point(0, 0), new[] { new Point(0, 0), });
            var game = new Game(board, null, new[] { unit, unit2, unit3 }, 0, 0, 0, -1, -1, string.Empty, 0).TrySpawnNew();

            var actual = validator.Validate(game, commands);
            Assert.AreEqual(expected, actual.IsValid);
        }

        [Test]
        public void TestValidateInputJson()
        {
            var game = new GameBuilder().Build("Problems\\problem_0.json")[0];
            var answer = "aaaaaaaaaaaaaaaaaei!44444444444444444";

            var result = new GameHistoryValidator().Validate(game, answer);

            Assert.IsTrue(result.IsValid);
        }
    }
}