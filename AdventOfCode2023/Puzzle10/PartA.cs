using AdventOfCode2023.Puzzle10.Models;
using AdventOfCode2023.Puzzle8;

namespace AdventOfCode2023.Puzzle10
{
    internal static class PartA
    {
        private static readonly Up Up = new();
        private static readonly Down Down = new();
        private static readonly Left Left = new();
        private static readonly Right Right = new();

        public static void Run()
        {
            var maze = File.ReadAllLines("Puzzle10/input.txt").Select(x => x.ToCharArray()).ToArray();

            var row = Array.FindIndex(maze, row => row.Contains('S'));
            var col = Array.FindIndex(maze[row], e => e == 'S');

            var start = maze[row][col];


            var moves = new List<Move>
            {
                Up,
                Down,
                Left,
                Right
            };


            foreach (var move in moves)
            {
                var previousMove = move;
                var currRow = row + previousMove.Y;
                var currCol = col + previousMove.X;
                if (currRow < 0 || currRow >= maze.Length || currCol < 0 || currCol >= maze[currRow].Length) continue;
                var curr = maze[currRow][currCol];
                if (curr.IsPipeChar())
                {
                    var moveCount = 1;

                    while (curr != start && curr.IsPipeChar())
                    {
                        Console.Write(curr);
                        previousMove = curr switch
                        {
                            'F' when previousMove is Up => Right,
                            'F' when previousMove is Left => Down,
                            'J' when previousMove is Right => Up,
                            'J' when previousMove is Down => Left,
                            '7' when previousMove is Right => Down,
                            '7' when previousMove is Up => Left,
                            '-' when previousMove is Right => Right,
                            '-' when previousMove is Left => Left,
                            '|' when previousMove is Up => Up,
                            '|' when previousMove is Down => Down,
                            'L' when previousMove is Left => Up,
                            'L' when previousMove is Down => Right,
                            _ => previousMove
                        };

                        currRow += previousMove.Y;
                        currCol += previousMove.X;
                        curr = maze[currRow][currCol];

                        moveCount++;
                    }

                    Console.WriteLine();

                    if (curr == start)
                    {
                        Console.WriteLine((moveCount + 1) / 2);
                        break;
                    }
                }
            }
        }


        private static bool IsPipeChar(this char c)
        {
            return new char[] { 'F', '7', '-', 'J', 'L', '|' }.Contains(c);
        }
    }
}