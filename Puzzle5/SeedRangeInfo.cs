namespace Puzzle5;

internal class SeedRangeInfo(long start, long range)
{
    public long SourceRangeStart { get; set; } = start;
    public long RangeLength { get; set; } = range;

    public long SourceRangeEnd => SourceRangeStart + RangeLength - 1;

    public bool IsInSourceRange(long number) => number >= SourceRangeStart && number <= SourceRangeEnd;
}