using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle8
{
    internal static class PartB
    {
        public static void Run()
        {
            var inputs = File.ReadAllLines("input.txt");

            var directions = inputs[0].ToCharArray();

            var nodes = inputs.ToList().Skip(2).Select(x => new Node(x)).ToDictionary(x=>x.Label, x=>x);

            var startingNodes = nodes.Where(x=>x.Key.EndsWith('A')).Select(x=>x.Value).ToList();

            var i = 0;
            var counts = new List<long>();

            foreach (var node in startingNodes)
            {
                var count = 0;
                var curr = node;
                while(!curr.Label.EndsWith('Z'))
                {
                    count++;
                    var direction = directions[i++];
                    curr = direction switch
                    {
                        'L' => nodes[curr.Left],
                        'R' => nodes[curr.Right],
                        _ => throw new ArgumentOutOfRangeException()
                    };
               

                    if (i == directions.Length) 
                        i = 0;
                }

                counts.Add(count);
            }

          

            Console.WriteLine(lcm(counts));
        }


        static long gcd(long n1, long n2)
        {
            while (true)
            {
                if (n2 == 0) return n1;
                var n3 = n1;
                n1 = n2;
                n2 = n3 % n2;
            }
        }

        static long lcm(IEnumerable<long> numbers)
        {
            return numbers.Aggregate((S, val) => S * val / gcd(S, val));
        }
    }
}
