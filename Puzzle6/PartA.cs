using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle6
{
    internal static class PartA
    {
        public static void Run()
        {
            var input = File.ReadAllLines("input.txt");

            var times = input[0].Split(":")[1].GetNumbers();
            var distances = input[1].Split(":")[1].GetNumbers();

            var answer = 1;

            for (var i = 0; i < times.Length; i++)
            {
                var time = times[i];
                var distance = distances[i];

                var solutionCount = GetSolutionCount(time, distance);

                answer *= solutionCount;
            }

            Console.WriteLine(answer);
        }

        private static int GetSolutionCount(int time, int minDistance)
        {
            var successes = 0;
            for (var i = 0; i < time; i++)
            {
                var hold = i;
                var run = time - hold;

                var distance = hold * run;

                if (distance >= minDistance) successes++;
            }

            return successes;
        }


        private static int[] GetNumbers(this string numberString)
        {
            return numberString.Trim().Split(" ")
                .Select(x => int.TryParse(x.Trim(), out var number) ? number : default(int?)).OfType<int>().ToArray();
        }
    }



}
