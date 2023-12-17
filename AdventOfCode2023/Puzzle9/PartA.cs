using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzle9
{
    internal static class PartA
    {
        public static void Run()
        {
            long sum = 0;
            var inputs = File.ReadAllLines("Puzzle9/input.txt");

            var sequences = inputs.Select(x => x.Split(" ").Select(int.Parse).ToArray());

            foreach (var sequence in sequences)
            {
                var endNumbers = new Stack<int>();
                endNumbers.Push(sequence[^1]);

                var curr = sequence;

                while (curr[^1] != 0)
                {
                    var newSequence = new List<int>();
                    for (int i = 1; i < curr.Length; i++)
                    {
                        newSequence.Add(curr[i]- curr[i-1]);
                    }
                    endNumbers.Push(newSequence.Last());
                    curr = newSequence.ToArray();
                }

                var previousEndNumber = 0;
                var topEndNumber = 0;
                foreach (var endNumber in endNumbers)
                {

                    topEndNumber = endNumber + previousEndNumber;
                    previousEndNumber = topEndNumber;
                }

                Console.WriteLine(topEndNumber);
                sum += topEndNumber;
            }

            Console.WriteLine(sum);
        }
    }
}