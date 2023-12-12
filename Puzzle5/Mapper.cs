using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Puzzle5
{
    internal class Mapper
    {
        private readonly List<RangeInfo> _ranges;

        public Mapper(string inputName)
        {
            var rows = File.ReadAllLines(inputName).Skip(1);
            _ranges = rows.Select(row => new RangeInfo(row)).OrderBy(x => x.SourceRangeStart).ToList();
        }

        public long GetDestination(long source)
        {
            return _ranges.FirstOrDefault(x => x.IsInSourceRange(source))?.GetDestinationNumber(source) ?? source;
        }


        public long GetSource(long destination)
        {
            return _ranges.FirstOrDefault(x => x.IsInDestinationRange(destination))?.GetSourceNumber(destination) ??
                   destination;
        }
    }
}