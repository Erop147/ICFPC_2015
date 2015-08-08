using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class GameHistoryValidatorTest
    {
        [Test]
        public void TestValidate()
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
            var game = new Game(board, null, new[] { unit, unit2 }, 0, 0, 0, -1, -1, string.Empty, 0);

            //validator.Validate(game, "")
            var actual = game.TrySpawnNew();
            Assert.AreEqual(GameState.NewIsSpawned, actual.State);
        }
    }
}