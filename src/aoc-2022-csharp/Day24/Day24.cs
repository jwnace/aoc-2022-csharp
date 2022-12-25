namespace aoc_2022_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt");
    private static readonly Dictionary<(int Row, int Col), char> Map = GetMap();
    private static readonly List<Blizzard> Blizzards = GetBlizzards();
    private static readonly Dictionary<int, List<Blizzard>> BlizzardMemo = new();

    public static int Part1() => GetShortestPathForPart1((Player: (Row: 0, Col: 1), Time: 0));

    public static int Part2() => GetShortestPathForPart2(
        (Player: (Row: 0, Col: 1), Time: 0, HasReachedEnd: false, HasReachedStart: false));

    private static int GetShortestPathForPart1(((int Row, int Col) Player, int Time) initialState)
    {
        var states = new HashSet<((int Row, int Col) Player, int Time)>();
        states.Add(initialState);

        var queue = new PriorityQueue<((int Row, int Col) Player, int Time), int>();
        queue.Enqueue(initialState, 0);

        var seen = new HashSet<((int Row, int Col) Player, int Time)>();

        var minRow = Map.Min(m => m.Key.Item1);
        var maxRow = Map.Max(m => m.Key.Item1);
        var minCol = Map.Min(m => m.Key.Item2);
        var maxCol = Map.Max(m => m.Key.Item2);
        var destination = (maxRow, maxCol - 1);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (player, time) = state;
            var (row, col) = player;

            if (seen.Contains(state))
            {
                continue;
            }

            seen.Add(state);

            if (player == destination)
            {
                return state.Time;
            }

            var blizzards = GetBlizzards(time + 1);

            var neighbors = new[]
            {
                (Row: row + 1, Col: col),
                (Row: row, Col: col + 1),
                (Row: row - 1, Col: col),
                (Row: row, Col: col - 1),
            };

            foreach (var neighbor in neighbors)
            {
                // don't move into a wall
                if (Map.TryGetValue(neighbor, out var value) && value == '#')
                {
                    continue;
                }

                // don't move above the Map
                if (neighbor.Row < minRow)
                {
                    continue;
                }

                // don't move below the Map
                if (neighbor.Row > maxRow)
                {
                    continue;
                }

                // don't move into a blizzard
                if (blizzards.Any(b => b.Position == neighbor))
                {
                    continue;
                }

                var newState = (Player: neighbor, Time: time + 1);
                states.Add(newState);
                queue.Enqueue(newState, newState.Time);
            }

            // if a blizzard moved on top of the player, the player MUST move
            if (blizzards.All(b => b.Position != player))
            {
                var newState = (Player: player, Time: time + 1);
                states.Add(newState);
                queue.Enqueue(newState, newState.Time);
            }
        }

        return 0;
    }

    private static int GetShortestPathForPart2(
        ((int Row, int Col) Player, int Time, bool HasReachedEnd, bool HasReachedStart) initialState)
    {
        var states = new HashSet<((int Row, int Col) Player, int Time, bool HasReachedEnd, bool HasReachedStart)>();
        states.Add(initialState);

        var queue = new PriorityQueue<
            ((int Row, int Col) Player, int Time, bool HasReachedEnd, bool HasReachedStart), int>();
        queue.Enqueue(initialState, 0);

        var seen = new HashSet<((int Row, int Col) Player, int Time, bool HasReachedEnd, bool HasReachedStart)>();

        var minRow = Map.Min(m => m.Key.Item1);
        var maxRow = Map.Max(m => m.Key.Item1);
        var minCol = Map.Min(m => m.Key.Item2);
        var maxCol = Map.Max(m => m.Key.Item2);
        var destination = (maxRow, maxCol - 1);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (player, time, hasReachedEnd, hasReachedStart) = state;
            var (row, col) = player;

            if (seen.Contains(state))
            {
                continue;
            }

            seen.Add(state);

            // this is the first time reaching the start, AFTER reaching the end
            if (player == (0, 1) && hasReachedEnd && !hasReachedStart)
            {
                hasReachedStart = true;
            }

            if (player == destination)
            {
                // this is the first time reaching the end
                if (!hasReachedEnd)
                {
                    hasReachedEnd = true;
                }

                // this is the first time reaching the end, AFTER reaching the start
                if (hasReachedEnd && hasReachedStart)
                {
                    return state.Time;
                }
            }

            var blizzards = GetBlizzards(time + 1);

            var neighbors = new[]
            {
                (Row: row + 1, Col: col),
                (Row: row, Col: col + 1),
                (Row: row - 1, Col: col),
                (Row: row, Col: col - 1),
            };

            foreach (var neighbor in neighbors)
            {
                // don't move into a wall
                if (Map.TryGetValue(neighbor, out var value) && value == '#')
                {
                    continue;
                }

                // don't move above the Map
                if (neighbor.Row < minRow)
                {
                    continue;
                }

                // don't move below the Map
                if (neighbor.Row > maxRow)
                {
                    continue;
                }

                // don't move into a blizzard
                if (blizzards.Any(b => b.Position == neighbor))
                {
                    continue;
                }

                var newState = (Player: neighbor, Time: time + 1, HasReachedEnd: hasReachedEnd,
                    HasReachedStart: hasReachedStart);
                states.Add(newState);
                queue.Enqueue(newState, newState.Time);
            }

            // if a blizzard moved on top of the player, the player MUST move
            if (blizzards.All(b => b.Position != player))
            {
                var newState = (Player: player, Time: time + 1, HasReachedEnd: hasReachedEnd,
                    HasReachedStart: hasReachedStart);
                states.Add(newState);
                queue.Enqueue(newState, newState.Time);
            }
        }

        return 0;
    }

    private static IEnumerable<Blizzard> MoveBlizzards(IEnumerable<Blizzard> blizzards)
    {
        var minRow = Map.Min(n => n.Key.Row);
        var maxRow = Map.Max(n => n.Key.Row);
        var minCol = Map.Min(n => n.Key.Col);
        var maxCol = Map.Max(n => n.Key.Col);

        foreach (var blizzard in blizzards)
        {
            var (row, col, direction) = blizzard;

            var position = direction switch
            {
                '^' => (row - 1, col),
                'v' => (row + 1, col),
                '<' => (row, col - 1),
                '>' => (row, col + 1),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (Map.TryGetValue(position, out var value) && value == '#')
            {
                position = direction switch
                {
                    '^' => (maxRow - 1, col),
                    'v' => (minRow + 1, col),
                    '<' => (row, maxCol - 1),
                    '>' => (row, minCol + 1),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            // TODO: test if I can just return `blizzard` instead of instantiating a new one every time
            yield return new Blizzard(position, blizzard.Direction);
        }
    }

    private static void RemoveBlizzardsFromMap(List<Blizzard> blizzards)
    {
        var blizzardPositions = Map
            .Where(x => blizzards.Any(b => (b.Position.Row, b.Position.Col) == (x.Key.Row, x.Key.Col)))
            .ToList();

        foreach (var position in blizzardPositions)
        {
            Map.Remove(position.Key);
        }
    }

    private static List<Blizzard> GetBlizzards()
    {
        var blizzards = Map
            .Where(x => x.Value is '^' or 'v' or '<' or '>')
            .Select(x => new Blizzard((x.Key.Row, x.Key.Col), x.Value))
            .ToList();

        RemoveBlizzardsFromMap(blizzards);

        return blizzards;
    }

    private static List<Blizzard> GetBlizzards(int time)
    {
        if (BlizzardMemo.TryGetValue(time, out var value))
        {
            return value;
        }

        var start = 0;
        var blizzards = Blizzards;

        if (BlizzardMemo.Any())
        {
            start = BlizzardMemo.Max(x => x.Key);
            blizzards = BlizzardMemo[start];
        }

        for (var i = start; i < time; i++)
        {
            blizzards = MoveBlizzards(blizzards).ToList();
            BlizzardMemo[i + 1] = blizzards;
        }

        return blizzards;
    }

    private static Dictionary<(int Row, int Col), char> GetMap()
    {
        var map = new Dictionary<(int Row, int Col), char>();

        for (var i = 0; i < Input.Length; i++)
        {
            for (var j = 0; j < Input[i].Length; j++)
            {
                if (Input[i][j] != '.')
                {
                    map[(i, j)] = Input[i][j];
                }
            }
        }

        return map;
    }

    private static void DrawMap(IReadOnlyCollection<Blizzard> blizzards, (int Row, int Col) player)
    {
        var minRow = Map.Min(n => n.Key.Row);
        var maxRow = Map.Max(n => n.Key.Row);
        var minCol = Map.Min(n => n.Key.Col);
        var maxCol = Map.Max(n => n.Key.Col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                var value = Map.TryGetValue((row, col), out var v) ? v : '.';

                if (value == '.')
                {
                    var b = blizzards.Where(b => b.Position == (row, col)).ToList();

                    if (player == (row, col) && !b.Any())
                    {
                        value = 'E';
                    }
                    else if (b.Count == 1)
                    {
                        value = b.First().Direction;
                    }
                    else if (b.Count > 1)
                    {
                        value = b.Count < 10 ? char.Parse(b.Count.ToString()) : '*';
                    }
                }

                Console.Write(value);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private class Blizzard
    {
        public Blizzard((int Row, int Col) position, char direction)
        {
            Position = position;
            Direction = direction;
        }

        public (int Row, int Col) Position { get; set; }
        public char Direction { get; set; }

        public void Deconstruct(out int row, out int col, out char direction) =>
            (row, col, direction) = (Position.Row, Position.Col, Direction);
    }
}
