namespace AdventOfCode2023.Puzzle8
{
    internal class Node
    {
        public Node(string row)
        {
            var rowParts = row.Split("=").Select(x => x.Trim()).ToArray();
            Label = rowParts[0];
            var turns = rowParts[1].Replace("(", "").Replace(")", "").Split(",").Select(x => x.Trim()).ToArray();
            Left = turns[0];
            Right = turns[1];
        }
        public string Label { get; set; }
        public string Left { get; set; }
        public string Right  { get; set; }
    }
}
