using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2023.Puzzle8;
using NUnit.Framework;

namespace AdventOfCode2023.Puzzle14
{
    [TestFixture]
    internal class Tests
    {
        [Test]
        public void PartA_Sample()
        {
            var result = new Puzzle14("sample.txt").PartA();
            Assert.That(result, Is.EqualTo(136));
            Console.WriteLine(result);
        }

        
        [Test]
        public void PartA_MainPuzzle()
        {
           var result = new Puzzle14("input.txt").PartA();
           Assert.That(result, Is.EqualTo(109638));
           Console.WriteLine(result);
        }

        [Test]
        public void PartB_Sample()
        {
            var result = new Puzzle14("sample.txt").PartB(100);
            Assert.That(result, Is.EqualTo(64));
            Console.WriteLine(result);
        }

        
        [Test]
        public void PartB_MainPuzzle()
        {
            var result = new Puzzle14("input.txt").PartB(500);
            Assert.That(result, Is.EqualTo(102657));
            Console.WriteLine(result);
        }

        [Test]
        public void PartB()
        {

        }
    }
}
