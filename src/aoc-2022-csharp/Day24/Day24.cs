using System.Diagnostics.Contracts;

namespace aoc_2022_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt");
    private static readonly Dictionary<(int, int), char> Map = GetMap();
    private static readonly List<Blizzard> Blizzards = GetBlizzards(Map);
    private static readonly Dictionary<int, List<Blizzard>> BlizzardMemo = new();

    private static Dictionary<(Dictionary<(int, int), char>, List<Blizzard>, (int, int)), int> Memo = new();

    public static int Part1()
    {
        // HACK: I need to do this... but I would like to find a better way to do it...
        // maybe I can encapsulate this inside of GetBlizzards()
        RemoveBlizzardsFromMap(Map, Blizzards);

        return GetShortestPath(((Row: 0, Col: 1), Time: 0));
    }

    private static int GetShortestPath(((int Row, int Col) Player, int Time) initialState)
    {
        var distances = new Dictionary<((int Row, int Col) player, int time), int>();
        distances[initialState] = 0;

        var queue = new PriorityQueue<((int Row, int Col) Player, int Time), int>();
        queue.Enqueue(initialState, 0);

        var seen = new HashSet<((int Row, int Col) player, int time)>();

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (player, time) = state;

            if (seen.Contains(state))
            {
                continue;
            }

            seen.Add(state);

            if (seen.Count % 100 == 0)
            {
                Console.WriteLine($"seen: {seen.Count}, distance: {distances[state]}, time: {state.Time}");
            }

            var minRow = Map.Min(m => m.Key.Item1);
            var maxRow = Map.Max(m => m.Key.Item1);
            var minCol = Map.Min(m => m.Key.Item2);
            var maxCol = Map.Max(m => m.Key.Item2);
            var destination = (maxRow, maxCol - 1);

            if (player == destination)
            {
                // DrawMap(Map, blizzards, player);

                Console.WriteLine($"seen: {seen.Count}, distance: {distances[state]}, time: {state.Time}");

                // var query = distances.Select(x => (x.Key.time, x.Key.player.Row, x.Key.player.Col))
                //     .OrderBy(x => x.time)
                //     .ThenBy(x => x.Row)
                //     .ThenBy(x => x.Col)
                //     .Select(x => $"{x.time} => ({x.Row}, {x.Col})")
                //     .ToList();

                DrawMap(Map, GetBlizzards(time), player);

                return distances[state];
            }

            // this is actually fine... we just moved this blizzard on top of the player,
            // and they're immediately going to move out of it
            // if (blizzards.Any(b => b.Position == player))
            // {
            //     throw new Exception("The player is in a blizzard!");
            // }

            // get the blizzards for the NEXT minute, to determine where it will be safe to move
            var blizzards = GetBlizzards(time + 1);

            if (state.Time == 4)
            {
                // DrawMap(Map, blizzards, player);
            }

            if (state.Time == 4 && state.Player == (1, 1))
            {
                // DrawMap(Map, blizzards, player);
                var foo = "break here!";
            }

            var (row, col) = player;

            var neighbors = new[]
            {
                (Row: row + 1, Col: col),
                (Row: row, Col: col + 1),
                (Row: row - 1, Col: col),
                (Row: row, Col: col - 1),
            };

            var playerWasAbleToMove = false;

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

                var newState = (neighbor, time + 1);
                // var distance = distances.TryGetValue(newState, out var v) ? v : int.MaxValue;
                // distance = Math.Min(distance, distances[state] + 1);

                var distance = distances[state] + 1;

                distances[newState] = distance;
                queue.Enqueue(newState, distance);

                playerWasAbleToMove = true;
            }

            // if a blizzard moved on top of the player, the player MUST move
            // if the player was able to move, they should move and not stay in the same place
            if (blizzards.All(b => b.Position != player) && !playerWasAbleToMove)
            {
                var newState = (player, time + 1);
                // var distance = distances.TryGetValue(newState, out var v) ? v : int.MaxValue;
                // distance = Math.Min(distance, distances[state] + 1);

                var distance = distances[state] + 1;
                // var timesSeen = seenTimes.TryGetValue(newState, out var times) ? times : 0;

                // HACK: if we've seen this state more than N times, we don't want to consider it anymore
                // if (seen.Contains(newState) && timesSeen < 500)
                // {
                // HACK: if I'm not moving, it's going to look like the same state again, so remove it from `seen`
                // seen.Remove(newState);
                // seenTimes[newState] = timesSeen + 1;
                // }

                // don't move at all
                distances[newState] = distance;
                queue.Enqueue(newState, distance);
            }
            // else
            // {
            //     throw new Exception("something went horribly wrong");
            // }
        }

        return 0;
    }

    private static IEnumerable<Blizzard> MoveBlizzards(Dictionary<(int Row, int Col), char> map, IEnumerable<Blizzard> blizzards)
    {
        var result = new List<Blizzard>();

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

            if (map.TryGetValue(position, out var value) && value == '#')
            {
                var minRow = map.Min(n => n.Key.Row);
                var maxRow = map.Max(n => n.Key.Row);
                var minCol = map.Min(n => n.Key.Col);
                var maxCol = map.Max(n => n.Key.Col);

                position = direction switch
                {
                    '^' => (maxRow - 1, col),
                    'v' => (minRow + 1, col),
                    '<' => (row, maxCol - 1),
                    '>' => (row, minCol + 1),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            yield return new Blizzard(position, blizzard.Direction);
        }
    }

    private static void RemoveBlizzardsFromMap(Dictionary<(int Row, int Col), char> map, List<Blizzard> blizzards)
    {
        var temp = map
            .Where(x => blizzards.Any(b => (b.Position.Row, b.Position.Col) == (x.Key.Row, x.Key.Col)))
            .ToList();

        foreach (var t in temp)
        {
            map.Remove(t.Key);
        }
    }

    private static List<Blizzard> GetBlizzards(Dictionary<(int Row, int Col), char> map)
    {
        var blizzards = map
            .Where(x => x.Value is '^' or 'v' or '<' or '>')
            .Select(x => new Blizzard((x.Key.Row, x.Key.Col), x.Value))
            .ToList();

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
            // start from the closest value
            start = BlizzardMemo.Max(x => x.Key);
            blizzards = BlizzardMemo[start];
        }

        for (var i = start; i < time; i++)
        {
            blizzards = MoveBlizzards(Map, blizzards).ToList();
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

    public static int Part2() => 2;

    private static void DrawMap(
        IReadOnlyDictionary<(int Row, int Col), char> map,
        List<Blizzard> blizzards,
        (int Row, int Col) player)
    {
        var minRow = map.Min(n => n.Key.Row);
        var maxRow = map.Max(n => n.Key.Row);
        var minCol = map.Min(n => n.Key.Col);
        var maxCol = map.Max(n => n.Key.Col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                var value = map.TryGetValue((row, col), out var v) ? v : '.';

                if (value == '.')
                {
                    var b = blizzards.Where(b => b.Position == (row, col)).ToList();

                    // if (player == (row, col) && b.Any())
                    // {
                        // value = '!';
                        // throw new Exception("The player is in a blizzard!");
                    // }
                    if (player == (row, col) && !b.Any())
                    {
                        value = 'E';
                        // value = '.';
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

        public (int, int, char) Deconstruct()
        {
            return (Position.Row, Position.Col, Direction);
        }

        public void Deconstruct(out int row, out int col, out char direction)
        {
            (row, col, direction) = (Position.Row, Position.Col, Direction);
        }
    }

    // private class Position
    // {
    //     public Position(int row, int col)
    //     {
    //         Row = row;
    //         Col = col;
    //     }
    //
    //     public int Row { get; set; }
    //     public int Col { get; set; }
    // }
}
