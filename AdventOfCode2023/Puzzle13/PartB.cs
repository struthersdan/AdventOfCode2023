namespace AdventOfCode2023.Puzzle13
{
    internal class PartB
    {
        public PartB(string inputName)
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

        private bool HasSmudge { get; set; }

        public int Calculate()
        {
            return Patterns.Sum(GetReflection);
        }

        private int GetReflection(char[][] rows)
        {
            if (FindReflection(rows, out var horizontal, 100)) return horizontal;

            var columns = rows.Transpose();

            if (FindReflection(columns, out var vertical, 1)) return vertical;

            return 0;
        }

        private bool FindReflection(char[][] rows, out int reflection, int multiplier)
        {
            reflection = 0;
            for (int curr = 1; curr < rows.Length; curr++)
            {
                HasSmudge = false;
                var leftIndex = curr - 1;
                var rightIndex = curr;

                var left = rows[leftIndex];
                var right = rows[rightIndex];

                var isMatch = RowsMatch(left, right);

                while (isMatch)
                {
                    if (leftIndex == 0 || rightIndex == rows.Length - 1)
                    {
                        if (!HasSmudge) isMatch = false;
                        else
                        {
                            reflection = curr * multiplier;
                            return true;
                        }
                     
                    }
                    else
                    {
                        isMatch = RowsMatch(rows[--leftIndex], rows[++rightIndex]);
                    }
                   
                }
            }

            return false;
        }

        private bool RowsMatch(char[] left, char[] right)
        {
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] == right[i]) continue;
                if (HasSmudge) return false;
                HasSmudge = true;
            }

            return true;
        }

    }
}