namespace aoc_2022_csharp.Day07;

public static class Day07
{
    private static readonly string[] Lines = File.ReadAllLines("Day07/day07.txt");

    public static int Part1() => GetDirectories().Where(x => x.TotalSize <= 100_000).Sum(x => x.TotalSize);

    public static int Part2()
    {
        var directories = GetDirectories();

        return directories
            .Where(x => x.TotalSize + 40_000_000 >= directories.Max(y => y.TotalSize))
            .OrderBy(x => x.TotalSize)
            .First()
            .TotalSize;
    }

    private static List<Directory> GetDirectories()
    {
        var root = new Directory { Name = "/" };
        var directories = new List<Directory> { root };
        var currentNode = root;

        foreach (var line in Lines)
        {
            if (line is "$ cd /" or "$ ls")
            {
                continue;
            }

            var values = line.Split(' ');

            if (line.StartsWith("$ cd"))
            {
                currentNode = line.EndsWith("..")
                    ? currentNode.Parent!
                    : currentNode.Children.Single(x => x.Name == values.Last());
            }
            else if (line.StartsWith("dir"))
            {
                var directory = new Directory { Name = values.Last(), Parent = currentNode };
                currentNode.Children.Add(directory);
                directories.Add(directory);
            }
            else
            {
                currentNode.Size += int.Parse(values.First());
            }
        }

        return directories;
    }

    private class Directory
    {
        public string Name { get; init; } = "";
        public int Size { get; set; }
        public Directory? Parent { get; init; }
        public List<Directory> Children { get; } = new();
        public int TotalSize => Size + Children.Sum(x => x.TotalSize);
    }
}
