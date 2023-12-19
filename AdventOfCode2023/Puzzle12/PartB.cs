using System.Runtime.InteropServices.JavaScript;

namespace AdventOfCode2023.Puzzle12
{
    internal static class PartB
    {
        public static void Run()
        {
            var rows = File.ReadAllLines("Puzzle12/input.txt").Select(x => new ConditionMapB(x));

            Console.WriteLine(rows.Sum(x => x.Count()));
        }
    }

    internal class ConditionMapB
    {
        private readonly Dictionary<(int, int), long> _preCalculated = new();
        readonly int[] RequiredSpace ;


        public ConditionMapB(string s)
        {
            var parts = s.Split(" ");

            Conditions = $"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}".ToCharArray();
            Groups = $"{parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}".Split(",").Select(int.Parse).ToArray();
            RequiredSpace = new int[Groups.Length];
        }


        public int[] Groups { get; set; }
        public char[] Conditions { get; set; }

        public long Count()
        {
            return CountArrangements(0, 0);
        }

        private long CountArrangements(int index, int group)
        {
            var preIndex = (index, group);
            if (_preCalculated.TryGetValue(preIndex, out var result)) return result;

            _preCalculated[preIndex] = result = CountArrangementsCalculation(index, group);

            return result;
        }

        private long CountArrangementsCalculation(int index, int group)
        {
            if (group == Groups.Length)
                return RemainingCouldBeEmpty(index) ? 1 : 0;

            if (index + RequiredSpace[group] > Conditions.Length)
                return 0;

            long sum = 0;
            var groupValue = Groups[group];
            var nextIndex = index + groupValue + 1;

            if (CouldBeSpring(index, groupValue))
                sum += CountArrangements(nextIndex, group + 1);

            if (CouldBeEmpty(Conditions[index]))
                sum += CountArrangements(index + 1, group);

            return sum;
        }

        private bool CouldBeSpring(int index, int length)
        {
            if (IsSpring(index + length))
                return false;

            return Enumerable.Range(index, length)
                .All(i => CouldBeSpring(Conditions[i]));
        }

        private bool RemainingCouldBeEmpty(int index) =>
            Conditions.Skip(index).All(CouldBeEmpty);

        private bool IsSpring(int index) =>
            index >= 0 && index < Conditions.Length && Conditions[index] == '.';

        private static bool CouldBeSpring(char condition) => condition != '#';
        private static bool CouldBeEmpty(char condition) => condition != '.';


    }
}