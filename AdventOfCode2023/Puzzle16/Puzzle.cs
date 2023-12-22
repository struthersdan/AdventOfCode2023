namespace AdventOfCode2023.Puzzle16
{
    internal class Puzzle(string inputName)
    {
        private const char ForwardMirror = '/';
        private const char BackwardMirror = '\\';
        private const char HorizontalSplitter = '|';
        private const char VerticalSplitter = '-';
        private const char EmptySpace = '.';

        private char[][] Rows { get; set; } =
            File.ReadAllLines($"{nameof(Puzzle16)}/{inputName}").Select(x => x.ToCharArray()).ToArray();


        public int PartA()
        {
            return GetEnergizedCount(0, 0, Direction.Right);
        }

        private int GetEnergizedCount(int row, int column, Direction incoming)
        {
            var previousActions = new HashSet<PositionInfo>();
            var upcomingAreas = new Queue<PositionInfo>();
            upcomingAreas.Enqueue(new PositionInfo(row, column, incoming));

            while (upcomingAreas.Count != 0)
            {
                var position = upcomingAreas.Dequeue();
                if(position.Row < 0 || position.Row > Rows.Length  -1|| position.Column < 0 || position.Column > Rows[position.Row].Length -1) continue;
                if(!previousActions.Add(position)) continue;

                switch (Rows[position.Row][position.Column], position.Incoming)
                {
                    case (EmptySpace, Direction.Up):
                    case (HorizontalSplitter, Direction.Up):
                        upcomingAreas.Enqueue(position with {Row = position.Row - 1});
                        break;
                    case (EmptySpace, Direction.Down):
                    case (HorizontalSplitter, Direction.Down):
                        upcomingAreas.Enqueue(position with {Row = position.Row + 1});
                        break;
                    case (EmptySpace, Direction.Left):
                    case (VerticalSplitter, Direction.Left):
                        upcomingAreas.Enqueue(position with {Column = position.Column - 1});
                        break;
                    case (EmptySpace, Direction.Right):
                    case (VerticalSplitter, Direction.Right):
                        upcomingAreas.Enqueue(position with {Column = position.Column + 1});
                        break;
                    case (ForwardMirror, Direction.Up):
                        upcomingAreas.Enqueue(position with {Column = position.Column + 1, Incoming = Direction.Right});
                        break;
                    case (ForwardMirror, Direction.Down):
                        upcomingAreas.Enqueue(position with {Column = position.Column - 1, Incoming = Direction.Left});
                        break;
                    case (ForwardMirror, Direction.Left):
                        upcomingAreas.Enqueue(position with {Row = position.Row + 1, Incoming = Direction.Down});
                        break;
                    case (ForwardMirror, Direction.Right):
                        upcomingAreas.Enqueue(position with {Row = position.Row - 1, Incoming = Direction.Up});
                        break;
                    case (BackwardMirror, Direction.Up):
                        upcomingAreas.Enqueue(position with {Column = position.Column - 1, Incoming = Direction.Left});
                        break;
                    case (BackwardMirror, Direction.Down):
                        upcomingAreas.Enqueue(position with {Column = position.Column + 1, Incoming = Direction.Right});
                        break;
                    case (BackwardMirror, Direction.Left):
                        upcomingAreas.Enqueue(position with {Row = position.Row - 1, Incoming = Direction.Up});
                        break;
                    case (BackwardMirror, Direction.Right):
                        upcomingAreas.Enqueue(position with {Row = position.Row + 1, Incoming = Direction.Down});
                        break;
                    case (HorizontalSplitter, Direction.Left):
                        upcomingAreas.Enqueue(position with {Row = position.Row - 1, Incoming = Direction.Up});
                        upcomingAreas.Enqueue(position with {Row = position.Row + 1, Incoming = Direction.Down});
                        break;
                    case (HorizontalSplitter, Direction.Right):
                        upcomingAreas.Enqueue(position with {Row = position.Row - 1, Incoming = Direction.Up});
                        upcomingAreas.Enqueue(position with {Row = position.Row + 1, Incoming = Direction.Down});
                        break;
                    case (VerticalSplitter, Direction.Up):
                        upcomingAreas.Enqueue(position with {Column = position.Column + 1, Incoming = Direction.Right});
                        upcomingAreas.Enqueue(position with {Column = position.Column - 1, Incoming = Direction.Left});
                        break;
                    case (VerticalSplitter, Direction.Down):
                        upcomingAreas.Enqueue(position with {Column = position.Column - 1, Incoming = Direction.Left});
                        upcomingAreas.Enqueue(position with {Column = position.Column + 1, Incoming = Direction.Right});
                        break;
                }
            }

            return previousActions.Select(x => (x.Column, x.Row)).ToHashSet().Count;
        }


        public int PartB()
        {
            var totals = new List<int>();
            for (var i = 0; i < Rows.Length; i++)
            {
                totals.Add(GetEnergizedCount(i, 0, Direction.Right));
                totals.Add(GetEnergizedCount(i, Rows.Length -1, Direction.Left));
            }
            for (var i = 0; i < Rows[0].Length; i++)
            {
                totals.Add(GetEnergizedCount(0, i, Direction.Down));
                totals.Add(GetEnergizedCount(Rows[0].Length -1 , i, Direction.Up));
            }

            return totals.Max();
        }
    }

    public record PositionInfo(int Row, int Column, Direction Incoming);


    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };
}