using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Puzzle13
{
    internal class PartA
    {
        public PartA(string inputName)
        {
            var rows = File.ReadAllLines(inputName);

            var patterns = new List<List<string>>();

            var pattern = new List<string>();
            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row))
                {
                    patterns.Add(pattern);
                    pattern = new List<string>();
                }
                else
                {
                    pattern.Add(row);
                }
            }

            patterns.Add(pattern);

            Patterns = patterns.Select(x => x.Select(y => y.ToCharArray()).ToArray()).ToList();
        }

        private List<char[][]> Patterns { get; set; }



        public  int Calculate()
        {
            return Patterns.Sum(GetReflection);

        }

        private int GetReflection(char[][] rows)
        {
            if (FindReflection(rows, out var horizontal, 100)) return horizontal;

            var columns = Transpose(rows);

            if (FindReflection(columns, out var vertical, 1)) return vertical;

            return 0;
        }

        private bool FindReflection(char[][] rows, out int reflection, int multiplier)
        {
            reflection = 0;
            for (int curr = 1; curr < rows.Length; curr++)
            {
                var leftIndex = curr - 1;
                var rightIndex = curr;

                var left = rows[leftIndex];
                var right = rows[rightIndex];

                var isMatch = RowsMatch(left, right);

                while (isMatch)
                {
                    if (leftIndex == 0 || rightIndex == rows.Length-1)
                    {
                        reflection = curr * multiplier;
                        return true;
                    }

                    isMatch = RowsMatch( rows[--leftIndex], rows[++rightIndex]);
                }
            }

            return false;
        }

        private bool RowsMatch(char[] left, char[] right)
        {
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i]) return false;
            }

            return true;
        }


        protected static char[][] Transpose(char[][] original)
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
