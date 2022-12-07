namespace aoc_2022_csharp.Day07;

public static class Day07
{
    private static readonly string[] Lines = File.ReadAllLines("Day07/day07.txt");

    private class Directory
    {
        public string Name { get; init; } = "";
        public int Size { get; set; }
        public Directory? Parent { get; init; }
        public List<Directory> Children { get; } = new();
        public int TotalSize => Size + Children.Sum(x => x.TotalSize);
    }

    public static int Part1()
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

        return directories.Where(x => x.TotalSize <= 100000).Sum(x => x.TotalSize);
    }

    public static int Part2()
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

        return directories
            .Where(x => x.TotalSize + 40000000 >= root.TotalSize)
            .OrderBy(x => x.TotalSize)
            .First()
            .TotalSize;
    }
}
