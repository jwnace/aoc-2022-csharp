using System.Diagnostics;
using System.Text;

namespace aoc_2022_csharp.Day17;

public static class Day17
{
    private static readonly char[] Jets = File.ReadAllText("Day17/day17.txt").ToArray();
    const int ShapeCount = 5;

    public static long Part1() => Solve(2022);

    public static long Part2() => Solve(1_000_000_000_000);

    private static void AddFloor(Dictionary<(long X, long Y), char> map)
    {
        map[(-1, -1)] = '+';
        map[(0, -1)] = '-';
        map[(1, -1)] = '-';
        map[(2, -1)] = '-';
        map[(3, -1)] = '-';
        map[(4, -1)] = '-';
        map[(5, -1)] = '-';
        map[(6, -1)] = '-';
        map[(7, -1)] = '+';
    }

    private static long Solve(long shapesToDrop)
    {
        var seen = new Dictionary<(long shapeIndex, long jetIndex, string formation), (long t, long top)>();
        var top = 0L;
        var added = 0L;
        var map = new Dictionary<(long X, long Y), char>();
        var currentJetIndex = 0;

        var stopwatch = new Stopwatch();
        stopwatch.Start();

        for (var i = 0L; i < shapesToDrop; i++)
        {
            DropNextShape(i, map, ref currentJetIndex);

            top = map.Max(m => m.Key.Y);
            var shapeIndex = (int)(i % ShapeCount);
            var jetIndex = currentJetIndex % Jets.Length; // i think this `jetIndex` is off by one
            var formation = new string(new[]
            {
                (char)('a' + top - (map.Any(m => m.Key.X == 0) ? map.Where(m => m.Key.X == 0).Max(m => m.Key.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.Key.X == 1) ? map.Where(m => m.Key.X == 1).Max(m => m.Key.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.Key.X == 2) ? map.Where(m => m.Key.X == 2).Max(m => m.Key.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.Key.X == 3) ? map.Where(m => m.Key.X == 3).Max(m => m.Key.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.Key.X == 4) ? map.Where(m => m.Key.X == 4).Max(m => m.Key.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.Key.X == 5) ? map.Where(m => m.Key.X == 5).Max(m => m.Key.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.Key.X == 6) ? map.Where(m => m.Key.X == 6).Max(m => m.Key.Y) : 0)),
            });

            var key = (shapeIndex, jetIndex, formation);

            if (seen.ContainsKey(key) && i >= 2022)
            {
                var (oldT, oldY) = seen[key];
                var dy = top - oldY;
                var dt = i - oldT;
                var amt = (shapesToDrop - i) / dt;
                added += amt * dy;
                i += amt * dt;
            }

            seen[key] = (i, top);

            var minX = map.Min(m => m.Key.X);
            var maxX = map.Max(m => m.Key.X);
            var minY = map.Min(m => m.Key.Y);
            var maxY = map.Max(m => m.Key.Y);

            if (map.Count > 1_000)
            {
                for (var y = minY; y < maxY - 10; y++)
                {
                    for (var x = minX; x <= maxX; x++)
                    {
                        map.Remove((x, y));
                    }
                }
            }

            // if (i % 100_000 == 0)
            // {
            //     var shapesPerMinute = Math.Floor(i / ((double)stopwatch.ElapsedMilliseconds / 1000 / 60));
            //     Console.WriteLine(
            //         $"i = {i}, map.Count = {map.Count}, {Math.Round((double)i / shapesToDrop * 100.0, 2)}%, {shapesPerMinute} shapes per minute");
            // }
        }

        // Console.WriteLine($"top + added + 1 = {top + added + 1}");

        return top + added + 1;
    }

    private static void DropNextShape(long i, Dictionary<(long X, long Y), char> map, ref int currentJetIndex)
    {
        var shape = GetNextShape(i, map);

        while (true)
        {
            currentJetIndex %= Jets.Length;
            var jet = Jets[currentJetIndex];
            currentJetIndex++;

            if (jet == '<')
            {
                TryMoveLeft(shape, map);
            }
            else if (jet == '>')
            {
                TryMoveRight(shape, map);
            }

            if (!TryMoveDown(shape, map))
            {
                AddShapeToMap(map, shape);
                break;
            }
        }
    }

    private static void AddShapeToMap(Dictionary<(long X, long Y), char> map, (long, long)[] shape)
    {
        foreach (var point in shape)
        {
            map[point] = '#';
        }
    }

    private static bool TryMoveDown((long, long)[] shape, Dictionary<(long X, long Y), char> map)
    {
        if (WouldCauseCollision(shape, map, (0, -1)))
        {
            return false;
        }

        for (var k = 0; k < shape.Length; k++)
        {
            shape[k].Item2--;
        }

        return true;
    }

    private static bool TryMoveRight((long, long)[] shape, Dictionary<(long X, long Y), char> map)
    {
        if (WouldCauseCollision(shape, map, (1, 0)))
        {
            return false;
        }

        for (var k = 0; k < shape.Length; k++)
        {
            shape[k].Item1++;
        }

        return true;
    }

    private static bool TryMoveLeft((long, long)[] shape, Dictionary<(long X, long Y), char> map)
    {
        if (WouldCauseCollision(shape, map, (-1, 0)))
        {
            return false;
        }

        for (var k = 0; k < shape.Length; k++)
        {
            shape[k].Item1--;
        }

        return true;
    }

    private static (long, long)[] GetNextShape(long i, Dictionary<(long X, long Y), char> map)
    {
        var (x, y) = (2L, map.Count > 0 ? map.Max(m => m.Key.Y) + 4L : 3L);

        return (i % ShapeCount) switch
        {
            0 => new[] { (x, y), (x + 1, y), (x + 2, y), (x + 3, y), },
            1 => new[]
            {
                (x + 1, y + 2),
                (x, y + 1), (x + 1, y + 1), (x + 2, y + 1),
                (x + 1, y),
            },
            2 => new[]
            {
                (x + 2, y + 2),
                (x + 2, y + 1),
                (x, y), (x + 1, y), (x + 2, y),
            },
            3 => new[]
            {
                (x, y + 3),
                (x, y + 2),
                (x, y + 1),
                (x, y),
            },
            4 => new[]
            {
                (x, y + 1), (x + 1, y + 1),
                (x, y), (x + 1, y),
            },
            _ => throw new InvalidOperationException(),
        };
    }

    private static bool WouldCauseCollision(
        (long X, long Y)[] shape,
        Dictionary<(long X, long Y), char> map,
        (long X, long Y) movement)
    {
        var newShape = shape.Select(point => (X: point.X + movement.X, Y: point.Y + movement.Y)).ToArray();

        // check for collision with the wall
        if (newShape.Any(p => p.X is < 0 or > 6))
        {
            return true;
        }

        // check for collision with the floor
        if (newShape.Any(p => p.Y < 0))
        {
            return true;
        }

        // check for collision with another shape
        return newShape.Any(map.ContainsKey);
    }

    private static IEnumerable<string> DrawMap(IReadOnlyDictionary<(long X, long Y), char> map)
    {
        var minY = map.Min(n => n.Key.Y);
        var maxY = map.Max(n => n.Key.Y);
        var minX = map.Min(n => n.Key.X);
        var maxX = map.Max(n => n.Key.X);

        for (var y = maxY; y >= minY; y--)
        {
            var builder = new StringBuilder();

            for (var x = minX; x <= maxX; x++)
            {
                var value = map.TryGetValue((x, y), out var v) ? v : (x == -1 || x == 7 ? '|' : '.');
                builder.Append(value);
            }

            yield return builder.ToString();
        }
    }
}
