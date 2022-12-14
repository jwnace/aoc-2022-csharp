namespace aoc_2022_csharp.Day14;

public static class Day14
{
    private static readonly string[] Input = File.ReadAllLines("Day14/day14.txt");

    public static int Part1()
    {
        var map = new Dictionary<(int X, int Y), char>();
        map[(500, 0)] = '+';

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

        // if any sand ever reaches this Y position, it will fall forever
        var bottomlessVoid = map.Max(n => n.Key.Y);

        var count = 0;

        // keep going until the first grain of sand reaches the bottomless void
        while (true)
        {
            var grain = (X: 500, Y: 0);


            // keep moving the grain of sand until it comes to rest
            while (TryMovePart1(map, grain, out var newPosition))
            {
                grain = newPosition;

                if (grain.Y >= bottomlessVoid)
                {
                    break;
                }
            }

            if (grain.Y >= bottomlessVoid)
            {
                break;
            }

            map[grain] = 'o';
            count++;
        }

        DrawMap(map);
        return count;
    }

    public static int Part2()
    {
        var map = new Dictionary<(int X, int Y), char>();
        map[(500, 0)] = '+';

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

        // if any sand ever reaches this Y position, it will fall forever
        var bottomlessVoid = map.Max(n => n.Key.Y);
        var floor = map.Max(n => n.Key.Y) + 2;

        var count = 0;

        // keep going until the first grain of sand reaches the bottomless void
        while (true)
        {
            var grain = (X: 500, Y: 0);

            // keep moving the grain of sand until it comes to rest
            while (TryMovePart2(map, floor, grain, out var newPosition))
            {
                grain = newPosition;

                if (grain == (500, 0))
                {
                    break;
                }
            }

            map[grain] = 'o';
            count++;

            if (grain == (500, 0))
            {
                break;
            }
        }

        // DrawMap(map);
        return count;
    }

    private static bool TryMovePart1(Dictionary<(int X, int Y), char> map, (int X, int Y) grain,
        out (int X, int Y) newPosition)
    {
        // TODO: try to move the grain (down, down-left, down-right)
        var n1 = (X: grain.X, Y: grain.Y + 1);
        var n2 = (X: grain.X - 1, Y: grain.Y + 1);
        var n3 = (X: grain.X + 1, Y: grain.Y + 1);

        var neighbors = new[] { n1, n2, n3 };

        var query = neighbors.Where(n => map.TryGetValue(n, out _) == false).ToList();

        if (!query.Any())
        {
            newPosition = grain;
            return false;
        }

        newPosition = query.First();
        return true;
    }

    private static bool TryMovePart2(Dictionary<(int X, int Y), char> map, int floor, (int X, int Y) grain,
        out (int X, int Y) newPosition)
    {
        var n1 = (X: grain.X, Y: grain.Y + 1);
        var n2 = (X: grain.X - 1, Y: grain.Y + 1);
        var n3 = (X: grain.X + 1, Y: grain.Y + 1);

        var neighbors = new[] { n1, n2, n3 };

        var query = neighbors.Where(n => n.Y < floor && map.TryGetValue(n, out _) == false).ToList();

        if (!query.Any())
        {
            newPosition = grain;
            return false;
        }

        newPosition = query.First();
        return true;
    }

    private static void DrawMap(Dictionary<(int X, int Y), char> map)
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
