namespace AdventOfCode2023.Puzzle10.Models;

public abstract class Move(int x, int y)
{
    public int Y { get; set; } = y;
    public int X { get; set; } = x;
}