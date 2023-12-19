namespace AdventOfCode2023.Puzzle12;

public class ArrangementsCounter
{
    public static long CountArrangements(SpringInfo info)
    {
        var counter = new ArrangementsCounter(info.Conditions, info.SpringGroups);
        return counter.Count();
    }

    readonly Condition[] Conditions;
    readonly int[] Groups;
    readonly int[] RequiredSpace;

    readonly Dictionary<(int, int), long> PreCalculated = new();

    private ArrangementsCounter(Condition[] conditions, IEnumerable<int> groups)
    {
        Conditions = conditions;
        Groups = groups.ToArray();
        RequiredSpace = new int[Groups.Length];

        int space = 0;
        for(int i = Groups.Length - 1; i >= 0; i--)
        {
            space += Groups[i];
            RequiredSpace[i] = space;
            space += 1;
        }
    }

    public long Count()
    {
        return CountArrangements(0, 0);
    }

    private long CountArrangements(int index, int group)
    {
        var preIndex = (index, group);
        if (!PreCalculated.TryGetValue(preIndex, out var result))
        {
            result = CountArrangementsCalculation(index, group);
            PreCalculated[preIndex] = result;
        }
          
        return result;
    }

    private long CountArrangementsCalculation(int index, int group)
    {
        // if this is the last group, could it be empty? this is the only possible solution remaining
        if(group == Groups.Length)
            return RemainingCouldBeEmpty(index) ? 1 : 0;

        //there's not enough room left return 0
        if(index + RequiredSpace[group] > Conditions.Length)
            return 0;

        long sum = 0;
        var groupValue = Groups[group];
        var nextIndex = index + groupValue + 1;

        if (CouldGroupAllBeSprings(index, groupValue))
        {
            //skip this section and go look at the next section,  after the length of this group
            sum += CountArrangements(nextIndex, group + 1);
        }
            

        //if we leave this one empty, does the group starting at the next character satisfy the group length
        if(CouldBeEmpty(Conditions[index]))
            sum += CountArrangements(index + 1, group);

        return sum;
    }

    /// <summary>
    /// Could All Of the items in this section be springs?
    /// </summary>
    /// <param name="index"></param>
    /// <param name="groupLength"></param>
    /// <returns></returns>
    private bool CouldGroupAllBeSprings(int index, int groupLength)
    {
        // if there is a spring after this section, the group wouldn't end properly
        if(IsSpring(index + groupLength))
            return false;

        //all of the items in this section can be springs
        return Enumerable.Range(index, groupLength)
            .All(i => CouldBeSpring(Conditions[i]));
    }

    private bool RemainingCouldBeEmpty(int index) =>
        Conditions.Skip(index).All(CouldBeEmpty);

    private bool IsSpring(int index) =>
        index >= 0 && index < Conditions.Length && Conditions[index] == Condition.Spring;

    private static bool CouldBeSpring(Condition condition) => condition != Condition.Empty;
    private static bool CouldBeEmpty(Condition condition) => condition != Condition.Spring;
}

public enum Condition
{
    Empty = 0,
    Spring = 1,
    Unknown = 2
}

public record SpringInfo
{
    public required Condition[] Conditions { get; init; }
    public required List<int> SpringGroups { get; init; }
}