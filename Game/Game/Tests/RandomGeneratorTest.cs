﻿using System.Linq;
using Game.Logic;
using NUnit.Framework;

namespace Game.Tests
{
    [TestFixture]
    public class RandomGeneratorTest
    {
        [Test]
        public void TestGetNext()
        {
            var generator = new RandomGenerator(17);
            var actuals = generator.Generate().Take(10).ToArray();
            CollectionAssert.AreEqual(new[] { 0, 24107, 16552, 12125, 9427, 13152, 21440, 3383, 6873, 16117 }, actuals);
        }
    }
}