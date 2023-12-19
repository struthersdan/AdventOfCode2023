using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2023.Puzzle8;
using NUnit.Framework;

namespace AdventOfCode2023.Puzzle13
{
    [TestFixture]
    internal class Challenge
    {
        [Test]
        public void PartA_Sample()
        {
            var result = new PartA("Puzzle13/sample.txt").Calculate();
            Assert.That(result, Is.EqualTo(405));
            Console.WriteLine(result);
        }

        
        [Test]
        public void PartA_MainPuzzle()
        {
           var result = new PartA("Puzzle13/input.txt").Calculate();
           Console.WriteLine(result);
        }

        [Test]
        public void PartB_Sample()
        {
            var result = new PartB("Puzzle13/sample.txt").Calculate();
            Assert.That(result, Is.EqualTo(400));
            Console.WriteLine(result);
        }

        
        [Test]
        public void PartB_MainPuzzle()
        {
            var result = new PartB("Puzzle13/input.txt").Calculate();
            Console.WriteLine(result);
        }

        [Test]
        public void PartB()
        {

        }
    }
}
