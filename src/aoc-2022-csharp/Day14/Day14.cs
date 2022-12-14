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

        DrawMap(map);

        return 1;
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
                var value = map.TryGetValue((x, y), out var v) ? v : '.';
                Console.Write(value);
            }

            Console.WriteLine();
        }
    }

    public static int Part2() => 2;
}
