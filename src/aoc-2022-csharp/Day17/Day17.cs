namespace aoc_2022_csharp.Day17;

public static class Day17
{
    private static readonly char[] Jets = File.ReadAllText("Day17/day17.txt").ToArray();
    private const int ShapeCount = 5;

    public static long Part1() => Solve(2022);

    public static long Part2() => Solve(1_000_000_000_000);

    private static long Solve(long shapesToDrop)
    {
        var seen = new Dictionary<(long shapeIndex, long jetIndex, string formation), (long t, long top)>();
        var top = 0L;
        var added = 0L;
        var map = new HashSet<(long X, long Y)>();
        var currentJetIndex = 0;

        for (var i = 0L; i < shapesToDrop; i++)
        {
            DropNextShape(i, map, ref currentJetIndex);

            top = map.Max(m => m.Y);
            var shapeIndex = (int)(i % ShapeCount);
            var jetIndex = currentJetIndex % Jets.Length; // i think this `jetIndex` is off by one
            var formation = new string(new[]
            {
                (char)('a' + top - (map.Any(m => m.X == 0) ? map.Where(m => m.X == 0).Max(m => m.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.X == 1) ? map.Where(m => m.X == 1).Max(m => m.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.X == 2) ? map.Where(m => m.X == 2).Max(m => m.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.X == 3) ? map.Where(m => m.X == 3).Max(m => m.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.X == 4) ? map.Where(m => m.X == 4).Max(m => m.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.X == 5) ? map.Where(m => m.X == 5).Max(m => m.Y) : 0)),
                (char)('a' + top - (map.Any(m => m.X == 6) ? map.Where(m => m.X == 6).Max(m => m.Y) : 0)),
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

            var minX = map.Min(m => m.X);
            var maxX = map.Max(m => m.X);
            var minY = map.Min(m => m.Y);
            var maxY = map.Max(m => m.Y);

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
        }

        return top + added + 1;
    }

    private static void DropNextShape(long i, HashSet<(long X, long Y)> map, ref int currentJetIndex)
    {
        var shape = GetNextShape(i, map);

        while (true)
        {
            currentJetIndex %= Jets.Length;
            var jet = Jets[currentJetIndex];
            currentJetIndex++;

            if (jet == '<')
            {
                TryMove(shape, map, (-1, 0));
            }
            else if (jet == '>')
            {
                TryMove(shape, map, (1, 0));
            }

            if (!TryMove(shape, map, (0, -1)))
            {
                AddShapeToMap(map, shape);
                break;
            }
        }
    }

    private static void AddShapeToMap(ISet<(long X, long Y)> map, IEnumerable<(long, long)> shape)
    {
        foreach (var point in shape)
        {
            map.Add(point);
        }
    }

    private static bool TryMove((long X, long Y)[] shape, IReadOnlySet<(long X, long Y)> map, (long X, long Y) movement)
    {
        if (WouldCauseCollision(shape, map, movement))
        {
            return false;
        }

        for (var i = 0; i < shape.Length; i++)
        {
            shape[i].X += movement.X;
            shape[i].Y += movement.Y;
        }

        return true;
    }

    private static (long, long)[] GetNextShape(long i, IReadOnlyCollection<(long X, long Y)> map)
    {
        var (x, y) = (2L, map.Any() ? map.Max(m => m.Y) + 4L : 3L);

        return (i % ShapeCount) switch
        {
            0 => new[] { (x, y), (x + 1, y), (x + 2, y), (x + 3, y), },
            1 => new[] { (x + 1, y + 2), (x, y + 1), (x + 1, y + 1), (x + 2, y + 1), (x + 1, y), },
            2 => new[] { (x + 2, y + 2), (x + 2, y + 1), (x, y), (x + 1, y), (x + 2, y), },
            3 => new[] { (x, y + 3), (x, y + 2), (x, y + 1), (x, y), },
            4 => new[] { (x, y + 1), (x + 1, y + 1), (x, y), (x + 1, y), },
            _ => throw new InvalidOperationException(),
        };
    }

    private static bool WouldCauseCollision(
        IEnumerable<(long X, long Y)> shape,
        IReadOnlySet<(long X, long Y)> map,
        (long X, long Y) movement) =>
        shape.Select(point => (X: point.X + movement.X, Y: point.Y + movement.Y))
            .Any(point => point.X is < 0 or > 6 || point.Y < 0 || map.Contains(point));
}
