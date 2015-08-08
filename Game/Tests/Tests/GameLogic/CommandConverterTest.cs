using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class CommandConverterTest
    {
        [Test]
        [TestCase('p', Command.MoveWest)]
        [TestCase('3', Command.MoveWest)]
        [TestCase('c', Command.MoveEast)]
        [TestCase('y', Command.MoveEast)]
        [TestCase('h', Command.MoveSouthWest)]
        [TestCase('4', Command.MoveSouthWest)]
        [TestCase('l', Command.MoveSouthEast)]
        [TestCase(' ', Command.MoveSouthEast)]
        [TestCase('d', Command.TurnClockWise)]
        [TestCase('1', Command.TurnClockWise)]
        [TestCase('s', Command.TurnCounterClockWise)]
        [TestCase('w', Command.TurnCounterClockWise)]
        public void TestConvert(char command, Command expected)
        {
            Assert.AreEqual(expected, CommandConverter.Convert(command));
        }
    }
}
