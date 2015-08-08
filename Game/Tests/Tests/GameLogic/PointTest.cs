using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class PointTest
    {
        [Test]
        public void TestRotate()
        {
            var point = new Point(-1, 0);
            var pivot = new Point(0, 0);

            var actual = point.RotateClockwise(pivot);
            Assert.AreEqual(-1, actual.Col);
            Assert.AreEqual(-1, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(0, actual.Col);
            Assert.AreEqual(-1, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(1, actual.Col);
            Assert.AreEqual(0, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(0, actual.Col);
            Assert.AreEqual(1, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(-1, actual.Col);
            Assert.AreEqual(1, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(-1, actual.Col);
            Assert.AreEqual(0, actual.Row);
        }

        [Test]
        public void TestRotate2()
        {
            var point = new Point(2, 3);
            var pivot = new Point(2, 4);

            var actual = point.RotateClockwise(pivot);
            Assert.AreEqual(3, actual.Col);
            Assert.AreEqual(4, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(2, actual.Col);
            Assert.AreEqual(5, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(1, actual.Col);
            Assert.AreEqual(5, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(1, actual.Col);
            Assert.AreEqual(4, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(1, actual.Col);
            Assert.AreEqual(3, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(2, actual.Col);
            Assert.AreEqual(3, actual.Row);
        }

        [Test]
        public void TestRotate3()
        {
            var point = new Point(4, 3);
            var pivot = new Point(2, 4);

            var actual = point.RotateClockwise(pivot);
            Assert.AreEqual(4, actual.Col);
            Assert.AreEqual(6, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(1, actual.Col);
            Assert.AreEqual(7, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(-1, actual.Col);
            Assert.AreEqual(5, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(0, actual.Col);
            Assert.AreEqual(2, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(2, actual.Col);
            Assert.AreEqual(1, actual.Row);

            actual = actual.RotateClockwise(pivot);
            Assert.AreEqual(4, actual.Col);
            Assert.AreEqual(3, actual.Row);
        }
    }
}