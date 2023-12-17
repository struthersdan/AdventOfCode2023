using AdventOfCode2023.Puzzle10.Models;
using AdventOfCode2023.Puzzle8;

namespace AdventOfCode2023.Puzzle10
{
    internal static class PartB
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
                Left,
                Right,
                Up,
                Down,
            };


            foreach (var move in moves)
            {
                var currentMove = move;
                var currRow = row + currentMove.Y;
                var currCol = col + currentMove.X;
                if (currRow < 0 || currRow >= maze.Length || currCol < 0 || currCol >= maze[currRow].Length) continue;
                var curr = maze[currRow][currCol];
                if (curr.IsPipeChar() && IsValidStartingMove(currentMove, curr))
                {
                    var loopTiles = new List<(int row, int col)> { (currRow, currCol) };

                    while (curr != start && curr.IsPipeChar())
                    {
                        Console.Write(curr);
                        currentMove = curr switch
                        {
                            'F' when currentMove is Up => Right,
                            'F' when currentMove is Left => Down,
                            'J' when currentMove is Right => Up,
                            'J' when currentMove is Down => Left,
                            '7' when currentMove is Right => Down,
                            '7' when currentMove is Up => Left,
                            '-' when currentMove is Right => Right,
                            '-' when currentMove is Left => Left,
                            '|' when currentMove is Up => Up,
                            '|' when currentMove is Down => Down,
                            'L' when currentMove is Left => Up,
                            'L' when currentMove is Down => Right,
                            'L' when currentMove is Down => Right,
                            _ => currentMove
                        };


                        currRow += currentMove.Y;
                        currCol += currentMove.X;
                        curr = maze[currRow][currCol];

                        loopTiles.Add((currRow, currCol));
                    }

                    Console.WriteLine();


                    for (int i = 0; i < maze.Length; i++)
                    {
                        for (int j = 0; j < maze[i].Length; j++)
                        {
                            Console.Write(loopTiles.Contains((i, j)) ? maze[i][j] : " ");
                        }

                        Console.WriteLine();
                    }

                    if (curr != start) continue;
                    var area = GetArea(loopTiles.ToArray());
                    var interior = GetInteriorPoints(area, loopTiles.Count);
                    Console.WriteLine(interior);
                    break;
                }
            }
        }

        private static bool IsValidStartingMove(Move previousMove, char curr)
        {
            if (previousMove is Up && new[] { '-', 'J', 'L' }.Contains(curr)) return false;
            if (previousMove is Down && new[] { '-', '7', 'F' }.Contains(curr)) return false;
            if (previousMove is Left && new[] { '|', 'J', '7' }.Contains(curr)) return false;
            if (previousMove is Right && new[] { '|', 'F', 'L' }.Contains(curr)) return false;
            return true;
        }

        //Pick's Theorem
        private static int GetInteriorPoints(int area, int boundaryPoints)
        {
            var interiorPoints = area - boundaryPoints / 2 + 1;
            return interiorPoints;
        }

        //Shoelace Formula 
        private static int GetArea((int row, int col)[] loopTiles)
        {
            var area1 = 0;
            var area2 = 0;
            for (int i = 0; i < loopTiles.Length; i++)
            {
                if (i == loopTiles.Length - 1)
                {
                    area1 += loopTiles[i].row * loopTiles[0].col;
                    area2 += loopTiles[i].col * loopTiles[0].row;
                }
                else
                {
                    area1 += loopTiles[i].row * loopTiles[i + 1].col;
                    area2 += loopTiles[i].col * loopTiles[i + 1].row;
                }
            }


            return Math.Abs((area1 - area2) / 2);
        }


        private static bool IsPipeChar(this char c)
        {
            return new char[] { 'F', '7', '-', 'J', 'L', '|' }.Contains(c);
        }
    }
}