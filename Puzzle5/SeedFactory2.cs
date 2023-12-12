using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle5
{
    internal class SeedFactory
    {
        public List<SeedInfo> Seeds { get; set; }
        public SeedFactory(string input)
        {
            var row = File.ReadAllLines(input);
            Seeds = row[0].Split(":")[1].Split(" ")
                .Select(x => long.TryParse(x.Trim(), out var number) ? number : default(long?)).OfType<long>()
                .Select(x => new SeedInfo(x)).ToList();
        }
    }
}

