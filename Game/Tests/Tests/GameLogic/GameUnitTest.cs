using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class GameUnitTest
    {
        [Test]
        public void TestBottomLeft()
        {
            var gameUnit = new GameUnit(Unit.Create(new Point(0, 0), new[]
                {
                    new Point(0, 0),
                    new Point(1, 0),
                    new Point(2, 1),
                    new Point(1, 1)
                }), new UnitPosition(new Point(0, 0), 0));
             var actual = gameUnit.BottomLeft();
             Assert.AreEqual(1, actual.Col);
             Assert.AreEqual(1, actual.Row);
         }
    }
}