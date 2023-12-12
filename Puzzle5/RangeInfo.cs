namespace Puzzle5;

internal class RangeInfo
{
    public RangeInfo(string row)
    {
        var parts = row.Split(" ")
            .Select(x => long.TryParse(x.Trim(), out var number) ? number : default(long?)).OfType<long>()
            .ToArray();

        DestinationRangeStart = parts[0];
        SourceRangeStart = parts[1];
        RangeLength = parts[2];
    }

    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }

    public long SourceRangeEnd => SourceRangeStart + RangeLength - 1;

    public bool IsInSourceRange(long number) => number >= SourceRangeStart && number <= SourceRangeEnd;

    public long? GetDestinationNumber(long number)
    {
        if (!IsInSourceRange(number)) return null;

        return DestinationRangeStart + number - SourceRangeStart;
    }

    public long? GetSourceNumber(long destination)
    {
        if (!IsInDestinationRange(destination)) return null;

        return SourceRangeStart + destination - DestinationRangeStart;
    }

    public bool IsInDestinationRange(long number) =>
        number >= DestinationRangeStart && number <= DestinationRangeEnd;

    public long DestinationRangeEnd => DestinationRangeStart + RangeLength - 1;
}