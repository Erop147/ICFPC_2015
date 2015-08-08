using System;
using ICFPC2015.GameLogic.Logic.Input;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ICFPC2015.Tests.Tests
{
    [TestFixture]
    public class InputReaderTest
    {
        [Test]
        public void TestRead()
        {
            var input = new InputReader().Read("testReader.txt");
            var expected = new Input
            {
                height = 10,
                width = 10,
                sourceSeeds = new [] { 0 },
                id = 0,
                filled = new InputCell[0],
                sourceLength = 100,
                units = new[]
                {
                    new InputUnit
                    {
                        pivot = new InputCell { x = 3, y = 4 },
                        members = new[] { new InputCell { x = 1, y = 2} }
                    }
                }
            };

            Console.WriteLine("Actual:");
            Console.WriteLine(Serialize(input));
            Console.WriteLine("Expected:");
            Console.WriteLine(Serialize(expected));

            Assert.AreEqual(Serialize(expected), Serialize(input));
        }

        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}