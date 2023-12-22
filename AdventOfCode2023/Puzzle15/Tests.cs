using NUnit.Framework;

namespace AdventOfCode2023.Puzzle15
{
    [TestFixture]
    internal class Tests
    {
        [Test]
        public void PartA_Sample()
        {
            var result = new Puzzle15("sample.txt").PartA();
            Assert.That(result, Is.EqualTo(1320));
            Console.WriteLine(result);
        }


        [Test]
        public void PartA_MainPuzzle()
        {
            var result = new Puzzle15("input.txt").PartA();
            Assert.That(result, Is.EqualTo(521341));
            Console.WriteLine(result);
        }

        [Test]
        public void PartB_Sample()
        {
            var result = new Puzzle15("sample.txt").PartB();
            Assert.That(result, Is.EqualTo(145));
            Console.WriteLine(result);
        }


        [Test]
        public void PartB_MainPuzzle()
        {
            var result = new Puzzle15("input.txt").PartB();
            Assert.That(result, Is.EqualTo(252782));
            Console.WriteLine(result);
        }

        [Test]
        public void PartB()
        {
        }
    }
}