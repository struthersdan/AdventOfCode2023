namespace AdventOfCode2023.Puzzle15
{
    internal class Puzzle15(string inputName)
    {
        private string[] Groups { get; set; } =
            File.ReadAllLines($"{nameof(Puzzle15)}/{inputName}")[0].Split(',').ToArray();


        public int PartA()
        {
            return Groups.Sum(GetGroupHash);
        }

        private readonly Dictionary<string, int> _memoizedGroupTotals = [];
        private int GetGroupHash(string group)
        {
            if (_memoizedGroupTotals.TryGetValue(group, out var total))
            {
                return total;
            }

            total = group.Aggregate(0, DetermineChange);
            _memoizedGroupTotals.Add(group, total);
            return total;
        }

        private readonly Dictionary<(int hash, int c), int> _memoizedChanges = [];
        private  int DetermineChange(int hash, char c)
        {
            var key = (hash, c);
            if (_memoizedChanges.TryGetValue(key, out var result))
            {
                return result;
            }

            hash += c;
            hash *= 17;
            hash %= 256;
            _memoizedChanges.Add(key, hash);
            return hash;
        }

        public int PartB()
        {
            var boxes = new List<Lens>[256].Select(x=> new List<Lens>()).ToArray();
            foreach (var group in Groups)
            {
                try
                {
                    var isEquals = group.Contains('=');
                    var sections = group.Split('-', '=');
                    var label = sections[0];
                    var boxId = GetGroupHash(label);

                    var box = boxes[boxId] ?? [];


                    var existingLens = box.FirstOrDefault(x => x.Label == label);
                    if (isEquals)
                    {
                        var focalLength = int.Parse(sections[1]);

                 
                        if ( existingLens == default)
                        {
                            box.Add(new Lens(label, focalLength));
                        }
                        else
                        {
                            existingLens.FocalLength = focalLength;
                        }
                    }
                    else if(existingLens != null)
                    {
                        box.Remove(existingLens);
                    }
                    boxes[boxId] = box;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return boxes.Select((box, i) => box.Select((lens, j) => (i + 1) * (j + 1) * lens.FocalLength).Sum()).Sum();

        }
    }

    public class Lens(string label, int focalLength)
    {
        public string Label { get; set; } = label;
        public int FocalLength { get; set; } = focalLength;
    }
}