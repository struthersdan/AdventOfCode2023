using NUnit.Framework;

namespace AdventOfCode2023.Puzzle16
{
    [TestFixture]
    internal class Tests
    {
        [Test]
        public void PartA_Sample()
        {
            var result = new Puzzle("sample.txt").PartA();
            Assert.That(result, Is.EqualTo(46));
            Console.WriteLine(result);
        }


        [Test]
        public void PartA_MainPuzzle()
        {
            var result = new Puzzle("input.txt").PartA();
            Assert.That(result, Is.EqualTo(6978));
            Console.WriteLine(result);
        }

        [Test]
        public void PartB_Sample()
        {
            var result = new Puzzle("sample.txt").PartB();
            Assert.That(result, Is.EqualTo(51));
            Console.WriteLine(result);
        }


        [Test]
        public void PartB_MainPuzzle()
        {
            var result = new Puzzle("input.txt").PartB();
            Assert.That(result, Is.EqualTo(7315));
            Console.WriteLine(result);
        }

        [Test]
        public void PartB()
        {
        }
    }
}