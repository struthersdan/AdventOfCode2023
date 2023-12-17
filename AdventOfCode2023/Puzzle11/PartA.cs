using AdventOfCode2023.Puzzle10.Models;
using AdventOfCode2023.Puzzle8;

namespace AdventOfCode2023.Puzzle11
{
    internal static class PartA
    {
        private const char Galaxy = '#';

        public static void Run()
        {
            var rows = File.ReadAllLines("Puzzle11/input.txt").Select(x => x.ToCharArray()).ToArray();

            var space = GetExpandedSpace(rows);

            var galaxyCoordinates = GetGalaxyCoordinates(space);


            var sum = 0;

            for (int i = 0; i < galaxyCoordinates.Length; i++)
            {
                for (int j = i + 1; j < galaxyCoordinates.Length; j++)
                {
                    var distance = CalculateDistance(galaxyCoordinates[i], galaxyCoordinates[j]);
                    sum += distance;
                }
            }

            Console.WriteLine(sum);

            //foreach (var row in space)
            //{
            //    foreach (var c in row)
            //    {
            //        Console.Write(c);
            //    }

            //    Console.WriteLine();
            //}
        }

        private static int CalculateDistance((int x, int y) start, (int x, int y) end)
        {
            return Math.Abs(end.x - start.x) + Math.Abs(end.y - start.y);
        }

        private static (int x, int y)[] GetGalaxyCoordinates(char[][] space)
        {
            var coordinates = new List<(int x, int y)>();
            for (int i = 0; i < space.Length; i++)
            {
                for (int j = 0; j < space[i].Length; j++)
                {
                    if (space[i][j] == Galaxy) coordinates.Add((i, j));
                }
            }

            return coordinates.ToArray();
        }

        private static char[][] GetExpandedSpace(char[][] rows)
        {
            var newRows = new List<char[]>();
            foreach (var row in rows)
            {
                if (row.IsEmptySpace())
                {
                    newRows.Add(row);
                }

                newRows.Add(row);
            }

            var columns = Transpose(newRows);

            var newColumns = new List<char[]>();
            foreach (var column in columns)
            {
                if (column.IsEmptySpace())
                {
                    newColumns.Add(column);
                }

                newColumns.Add(column);
            }

            return Transpose(newColumns).ToArray();
        }

        private static List<char[]> Transpose(List<char[]> original)
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

            return transposed;
        }


        private static bool IsEmptySpace(this char[] space)
        {
            return !space.Contains(Galaxy);
        }
    }
}