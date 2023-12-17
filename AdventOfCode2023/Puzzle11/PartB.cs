namespace AdventOfCode2023.Puzzle11
{
    internal static class PartB
    {
        private const char Galaxy = '#';

        public static void Run()
        {
            var rows = File.ReadAllLines("Puzzle11/input.txt").Select(x => x.ToCharArray()).ToArray();

            var emptyRows = GetEmptyRows(rows);
            var emptyColumns = GetEmptyColumns(rows);

            var galaxyCoordinates = GetGalaxyCoordinates(rows);


            long sum = 0;

            for (long i = 0; i < galaxyCoordinates.Length; i++)
            {
                for (long j = i + 1; j < galaxyCoordinates.Length; j++)
                {
                    var distance = CalculateDistance(galaxyCoordinates[i], galaxyCoordinates[j], emptyRows,
                        emptyColumns);
                    sum += distance;
                }
            }

            Console.WriteLine(sum);
        }

        private static long CalculateDistance((long x, long y) start, (long x, long y) end, List<int> emptyRows,
            List<int> emptyColumns)
        {
            var startX = Math.Min(end.x, start.x);
            var endX = Math.Max(end.x, start.x);
            var traversedEmptyRows = emptyRows.Count(x => x < endX && x > startX);

            var startY = Math.Min(start.y, end.y);
            var endY = Math.Max(start.y, end.y);
            var traversedEmptyCols = emptyColumns.Count(y => y < endY && y > startY);

            var across = endX - startX - traversedEmptyRows + traversedEmptyRows * 1000000;
            var down = endY - startY - traversedEmptyCols + traversedEmptyCols * 1000000;

            return across + down;
        }

        private static List<int> GetEmptyColumns(char[][] rows)
        {
            var columns = Transpose(rows);
            var emptyColumns = new List<int>();
            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                if (column.IsEmptySpace())
                {
                    emptyColumns.Add(i);
                }
            }

            return emptyColumns;
        }

        private static List<int> GetEmptyRows(char[][] rows)
        {
            var emptyRows = new List<int>();
            for (int i = 0; i < rows.Length; i++)
            {
                var row = rows[i];
                if (row.IsEmptySpace())
                {
                    emptyRows.Add(i);
                }
            }

            return emptyRows;
        }


        private static (long x, long y)[] GetGalaxyCoordinates(char[][] space)
        {
            var coordinates = new List<(long x, long y)>();
            for (long i = 0; i < space.Length; i++)
            {
                for (long j = 0; j < space[i].Length; j++)
                {
                    if (space[i][j] == Galaxy) coordinates.Add((i, j));
                }
            }

            return coordinates.ToArray();
        }


        private static char[][] Transpose(char[][] original)
        {
            var transposed = new List<char[]>();

            for (long i = 0; i < original[0].Length; i++)
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


        private static bool IsEmptySpace(this char[] space)
        {
            return !space.Contains(Galaxy);
        }
    }
}