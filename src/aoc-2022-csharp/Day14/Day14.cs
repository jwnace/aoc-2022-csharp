namespace aoc_2022_csharp.Day14;

public static class Day14
{
    private static readonly string[] Input = File.ReadAllLines("Day14/day14.txt");

    public static int Part1()
    {
        var map = BuildMap();
        var threshold = map.Max(n => n.Key.Y);
        var count = 0;

        while (true)
        {
            var grain = (X: 500, Y: 0);

            while (TryMove(map, grain, out var newPosition) && grain.Y < threshold)
            {
                grain = newPosition;
            }

            if (grain.Y >= threshold)
            {
                break;
            }

            map[grain] = 'o';
            count++;
        }

        return count;
    }

    public static int Part2()
    {
        var map = BuildMap();
        var floor = map.Max(n => n.Key.Y) + 2;
        var count = 0;

        while (true)
        {
            var grain = (X: 500, Y: 0);

            while (TryMove(map, grain, out var newPosition, floor) && newPosition != (500, 0))
            {
                grain = newPosition;
            }

            map[grain] = 'o';
            count++;

            if (grain == (500, 0))
            {
                break;
            }
        }

        return count;
    }

    private static Dictionary<(int X, int Y), char> BuildMap()
    {
        var map = new Dictionary<(int X, int Y), char> { [(500, 0)] = '+' };

        foreach (var line in Input)
        {
            var points = line.Split(" -> ");

            for (var i = 0; i < points.Length - 1; i++)
            {
                var start = points[i];
                var end = points[i + 1];
                var startValues = start.Split(',').Select(int.Parse).ToArray();
                var endValues = end.Split(',').Select(int.Parse).ToArray();
                var startX = new[] { startValues[0], endValues[0] }.Min();
                var endX = new[] { startValues[0], endValues[0] }.Max();
                var startY = new[] { startValues[1], endValues[1] }.Min();
                var endY = new[] { startValues[1], endValues[1] }.Max();

                for (var x = startX; x <= endX; x++)
                {
                    for (var y = startY; y <= endY; y++)
                    {
                        map[(x, y)] = '#';
                    }
                }
            }
        }

        return map;
    }

    private static bool TryMove(
        IReadOnlyDictionary<(int X, int Y), char> map,
        (int X, int Y) grain,
        out (int X, int Y) newPosition,
        int floor = 0)
    {
        var neighbors = new[]
        {
            (X: grain.X    , Y: grain.Y + 1),
            (X: grain.X - 1, Y: grain.Y + 1),
            (X: grain.X + 1, Y: grain.Y + 1)
        };

        var query = neighbors.Where(n => (floor == 0 || n.Y < floor) && map.TryGetValue(n, out _) == false).ToList();

        if (!query.Any())
        {
            newPosition = grain;
            return false;
        }

        newPosition = query.First();
        return true;
    }

    private static void DrawMap(IReadOnlyDictionary<(int X, int Y), char> map)
    {
        var minY = map.Min(n => n.Key.Y);
        var maxY = map.Max(n => n.Key.Y);
        var minX = map.Min(n => n.Key.X);
        var maxX = map.Max(n => n.Key.X);

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                var value = map.TryGetValue((x, y), out var v) ? v : ' ';
                Console.Write(value);
            }

            Console.WriteLine();
        }
    }
}
