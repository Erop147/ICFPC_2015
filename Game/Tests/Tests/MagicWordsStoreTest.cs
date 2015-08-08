using System;
using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests
{
    [TestFixture]
    public class MagicWordsStoreTest
    {
        [Test]
        public void TestRead()
        {
            Console.WriteLine(string.Join(" ", MagicWordsStore.Words));
            Assert.That(MagicWordsStore.Words.Length > 0);
        }
    }
}