namespace aoc_2022_csharp.Day18;

public static class Day18
{
    private static readonly HashSet<(int X, int Y, int Z)> Cubes = File.ReadAllLines("Day18/day18.txt")
        .Select(line => line.Split(',').Select(int.Parse).ToArray())
        .Select(group => (group[0], group[1], group[2]))
        .ToHashSet();

    private static readonly HashSet<(int X, int Y, int Z)> Outside = new();
    private static readonly HashSet<(int X, int Y, int Z)> Inside = new();

    public static int Part1()
    {
        var result = 0;

        foreach (var (x, y, z) in Cubes)
        {
            result += Cubes.Contains((x + 1, y, z)) ? 0 : 1;
            result += Cubes.Contains((x - 1, y, z)) ? 0 : 1;
            result += Cubes.Contains((x, y + 1, z)) ? 0 : 1;
            result += Cubes.Contains((x, y - 1, z)) ? 0 : 1;
            result += Cubes.Contains((x, y, z + 1)) ? 0 : 1;
            result += Cubes.Contains((x, y, z - 1)) ? 0 : 1;
        }

        return result;
    }

    public static int Part2()
    {
        var result = 0;

        foreach (var (x, y, z) in Cubes)
        {
            result += ReachesOutside((x + 1, y, z)) ? 1 : 0;
            result += ReachesOutside((x - 1, y, z)) ? 1 : 0;
            result += ReachesOutside((x, y + 1, z)) ? 1 : 0;
            result += ReachesOutside((x, y - 1, z)) ? 1 : 0;
            result += ReachesOutside((x, y, z + 1)) ? 1 : 0;
            result += ReachesOutside((x, y, z - 1)) ? 1 : 0;
        }

        return result;
    }

    private static bool ReachesOutside((int X, int Y, int Z) cube)
    {
        if (Outside.Contains(cube))
        {
            return true;
        }

        if (Inside.Contains(cube))
        {
            return false;
        }

        var seen = new HashSet<(int X, int Y, int Z)>();
        var queue = new Queue<(int X, int Y, int Z)>();
        queue.Enqueue(cube);

        while(queue.Any())
        {
            var temp = queue.Dequeue();
            var (x, y, z) = temp;

            if (Cubes.Contains(temp))
            {
                continue;
            }

            if (seen.Contains(temp))
            {
                continue;
            }

            seen.Add(temp);

            if (seen.Count > 2_000)
            {
                foreach(var s in seen)
                {
                    Outside.Add(s);
                }

                return true;
            }

            queue.Enqueue((x + 1, y    , z    ));
            queue.Enqueue((x - 1, y    , z    ));
            queue.Enqueue((x    , y + 1, z    ));
            queue.Enqueue((x    , y - 1, z    ));
            queue.Enqueue((x    , y    , z + 1));
            queue.Enqueue((x    , y    , z - 1));
        }

        foreach (var s in seen)
        {
            Inside.Add(s);
        }

        return false;
    }
}
