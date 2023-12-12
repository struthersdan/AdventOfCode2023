using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle5
{
    internal class SeedFactory2
    {
        public List<SeedRangeInfo> Seeds { get; set; } = new();

        public SeedFactory2(string input)
        {
            var row = File.ReadAllLines(input);
            var rows = row[0].Split(":")[1].Split(" ")
                .Select(x => long.TryParse(x.Trim(), out var number) ? number : default(long?)).OfType<long>().ToList();

            for (var i = 0; i < rows.Count; i++)
            {
                var start = rows[i];
                var range = rows[++i];


                Seeds.Add(new SeedRangeInfo(start, range));
            }
        }

        public bool HasSeed(long locationInfoSeed)
        {
            return Seeds.Any(x => x.IsInSourceRange(locationInfoSeed));
        }
    }
}