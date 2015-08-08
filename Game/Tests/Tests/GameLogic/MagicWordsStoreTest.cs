using System;
using ICFPC2015.GameLogic.Logic;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests.GameLogic
{
    [TestFixture]
    public class MagicWordsStoreTest
    {
        [Test]
        public void TestRead()
        {
            var store = new MagicWordsStore();
            Console.WriteLine(string.Join(" ", store.Words));
            Assert.That(store.Words.Length > 0);
        }
    }
}