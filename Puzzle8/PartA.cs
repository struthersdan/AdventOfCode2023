using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle8
{
    internal static class PartA
    {
        public static void Run()
        {
            var inputs = File.ReadAllLines("input.txt");

            var directions = inputs[0].ToCharArray();

            var nodes = inputs.ToList().Skip(2).Select(x => new Node(x)).ToDictionary(x=>x.Label, x=>x);

            var curr = nodes["AAA"];

            var i = 0;
            var count = 0;
            Console.WriteLine(curr.Label);

            while(curr.Label != "ZZZ")
            {
               
                count++;
                var direction = directions[i++];
                curr = direction switch
                {
                    'L' => nodes[curr.Left],
                    'R' => nodes[curr.Right],
                    _ => curr
                };

                Console.WriteLine(curr.Label);

                if (i == directions.Length) i = 0;
            }

            Console.WriteLine(count);
        }
    }
}
