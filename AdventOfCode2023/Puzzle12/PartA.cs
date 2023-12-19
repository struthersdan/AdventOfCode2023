using System.Runtime.InteropServices.JavaScript;

namespace AdventOfCode2023.Puzzle12
{
    internal static class PartA
    {
        public static void Run()
        {
            var rows = File.ReadAllLines("Puzzle12/input.txt").Select(x => new ConditionMapA(x));

            Console.WriteLine(rows.Sum(x => x.PossibleConditions()));
        }
    }

    internal class ConditionMapA
    {
        public ConditionMapA(string s)
        {
            var parts = s.Split(" ");

            Condition = parts[0];
            Groups = parts[1];
        }

        public string Groups { get; set; }
        public string Condition { get; set; }

        public int[] QuestionMarkIndexes => Enumerable.Range(0, Condition.Length)
            .Where(i => Condition[i] == '?')
            .ToArray();

        public int PossibleConditions()
        {
            var possibilities = new List<string> {Condition};
            foreach (var t in QuestionMarkIndexes)
            {
                possibilities = possibilities.Select(x =>
                {
                    var list1 = x.ToCharArray();
                    var list2 = x.ToCharArray();
                    list1[t] = '.';
                    list2[t] = '#';


                    var newLists = new List<string>();
                    var isPartialMatch = PartialMatch(list1, out string newList1);
                    if (isPartialMatch) newLists.Add(newList1);
                    isPartialMatch = PartialMatch(list2, out string newList2);
                    if (isPartialMatch) newLists.Add(newList2);
                    return newLists;
                }).SelectMany(x => x).ToList();
            }

            var parsedPossibilities = possibilities.Select(x =>
                string.Join(",",
                    x.Split(".").Select(y => y.Length != 0 ? y.Length : default(int?))
                        .OfType<int>()));

            return parsedPossibilities.Count(x => x.Equals(Groups));
        }

        private bool PartialMatch(char[] list1, out string newList)
        {
            newList = new string(list1);
            var questionMarkIndex = newList.IndexOf('?');
            questionMarkIndex = questionMarkIndex == -1 ? newList.Length - 1 : questionMarkIndex;
            var partialList1 = newList[..(questionMarkIndex)];
            var localGroups = partialList1.Split(".").Select(y => y.Length != 0 ? y.Length : default(int?))
                .OfType<int>().ToList();
            if (localGroups.Any())
            {
                localGroups.RemoveAt(localGroups.Count-1);
            }
            var partialGroups = string.Join(",", localGroups);
            
            var isMatch = Groups.StartsWith(partialGroups);
           // Console.WriteLine($"{partialGroups} - {Groups} - {isMatch}");
            return isMatch;
        }
    }
}