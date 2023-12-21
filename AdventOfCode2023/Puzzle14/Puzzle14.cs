using System.Runtime.InteropServices;

namespace AdventOfCode2023.Puzzle14
{
    internal class Puzzle14(string inputName)
    {
        private string[] Rows { get; set; } = File.ReadAllLines($"{nameof(Puzzle14)}/{inputName}");


        public int PartA()
        {
            var columns = Rows.Select(x => x.ToCharArray()).ToArray().Transpose();

            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = SortColumn(columns[i]);
            }


            var rows = columns.Transpose();

            return rows.Select(t => t.Count(x => x == 'O')).Select((rocksInRow, i) => rocksInRow * (rows.Length - i))
                .Sum();
        }

        private readonly Dictionary<string, char[]> _sortingResults = new();

        private char[] SortColumn(char[] column)
        {
            var key = new string(column);
            if (_sortingResults.TryGetValue(key, out var sortedColumn))
            {
                return sortedColumn;
            }

            var sortedColumnList = new List<char>();
            var innerColum = new List<char>();
            foreach (var curr in column)
            {
                if (curr == '#')
                {
                    sortedColumnList.AddRange(GetOrSortRange(innerColum));
                    innerColum = [];
                    sortedColumnList.Add(curr);
                }
                else
                {
                    innerColum.Add(curr);
                }
            }

            sortedColumnList.AddRange(GetOrSortRange(innerColum));

            var result = sortedColumnList.ToArray();
            _sortingResults[key] = result;
            return result;
        }

        private readonly Dictionary<string, List<char>> _sortedSections = [];

        private IEnumerable<char> GetOrSortRange(List<char> innerColum)
        {
            var key = new string(innerColum.ToArray());
            if (_sortedSections.TryGetValue(key, out var sortedSection))
            {
                return sortedSection;
            }

            sortedSection = [.. innerColum.OrderByDescending(x => x == 'O')];
            _sortedSections[key] = sortedSection;
            return sortedSection;
        }

        private readonly List<int> _loads = [];
        public int PartB(int count)
        {
            var columns = Rows.Select(x => x.ToCharArray()).ToArray().Transpose();

            for (int i = 0; i < count; i++)
            {
                columns = RunSpinCycle(columns);

                var localRows = columns.Transpose();


                var load = localRows.Select(t => t.Count(x => x == 'O'))
                    .Select((rocksInRow, i) => rocksInRow * (localRows.Length - i))
                    .Sum();

                _loads.Add(load);

            }

            if (DetectCycle(_loads, out var dictionary))
            {
                Console.WriteLine(dictionary.Count);
                foreach (var column in dictionary)  
                {
                    Console.WriteLine($"{column.Key}, {column.Value}"); 
                }


                var location = ((1000000000 + dictionary.First().Key - 1) % dictionary.Count);
                Console.WriteLine(location);
                var answer = dictionary.ToArray()[location];
                Console.WriteLine();

                return answer.Value;
            }

            return 0;
        }

        private static bool DetectCycle(List<int> loads, out Dictionary<int, int> dictionary)
        {
            dictionary = new Dictionary<int, int>();
            var loadArray = loads.ToArray();
            for (int i = 4; i < loadArray.Length/2; i++)
            {
                var tortoise = loadArray[i];
                var hare = loadArray[2 * i];
                if (tortoise != hare) continue;

                var cycle = new Dictionary<int, int>();

                var innerT = i ;
                var innherH = (2 * i);
                while (tortoise == hare)
                {
                    cycle.Add(innerT, tortoise);
                    if (innerT == 2 * i - 1)
                    {
                        {
                            dictionary = cycle;
                            return true;
                        }
                    }
                    tortoise = loadArray[++innerT];
                    hare = loadArray[++innherH];
                
                }
            }

            return false;
        }


        private char[][] RunSpinCycle(char[][] columns)
        {
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < columns.Length; j++)
                {
                    columns[j] = SortColumn(columns[j]);
                }

                columns = Rotate(columns);
            }

            return columns;
        }


        private char[][] Rotate(char[][] columns)
        {


            var newColumns = new char[columns[0].Length][];
            for (var i = 0; i < columns[0].Length; i++)
            {
                var newCol = new char[columns.Length];
                for (int j = 0; j < columns.Length; j++)
                {
                    newCol[j] = columns[j][i];
                }

                newColumns[columns[0].Length - 1 - i] = newCol;
            }

            return newColumns;
        }
    }
}