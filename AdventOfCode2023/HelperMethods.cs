using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    public static class HelperMethods
    {
        public static char[][] Transpose(this char[][] original)
        {
            var transposed = new List<char[]>();

            for (int i = 0; i < original[0].Length; i++)
            {
                var transpose = new List<char>();
                foreach (var originalArray in original)
                {
                    transpose.Add(originalArray[i]);
                }

                transposed.Add(transpose.ToArray());
            }

            return transposed.ToArray();
        }
    }
}
