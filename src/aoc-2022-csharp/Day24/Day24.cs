using System.ComponentModel.DataAnnotations.Schema;

namespace aoc_2022_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt");

    private static Dictionary<(Dictionary<(int, int), char>, List<Blizzard>, (int, int)), int> Memo = new();

    public static int Part1()
    {
        var map = GetMap();
        var blizzards = GetBlizzards(map);
        var player = (Row: 0, Col: 1);

        RemoveBlizzardsFromMap(map, blizzards);
        // DrawMap(map, blizzards, player);

        return GetShortestPath((map, blizzards, player));
    }

    private static int GetShortestPath((
        Dictionary<(int Row, int Col), char> Map,
        List<Blizzard> Blizzards,
        (int Row, int Col) Player) initialState)
    {
        var queue = new Queue<(
            Dictionary<(int Row, int Col), char> map,
            List<Blizzard> blizzards,
            (int Row, int Col) player)>();

        queue.Enqueue(initialState);

        var distances = new Dictionary<(
            Dictionary<(int Row, int Col), char> map,
            List<Blizzard> blizzards,
            (int Row, int Col) player), int>();

        distances[initialState] = 0;

        // HACK: I think I need to move the blizzards ahead of time because I'm computing the NEXT state...
        // not sure if that makes sense or not
        // MoveBlizzards(initialState.Map, initialState.Blizzards);

        while (queue.Any())
        {
            var state = queue.Dequeue();
            var (map, blizzards, player) = state;

            var minRow = map.Min(m => m.Key.Row);
            var maxRow = map.Max(m => m.Key.Row);
            var minCol = map.Min(m => m.Key.Col);
            var maxCol = map.Max(m => m.Key.Col);
            var destination = (maxRow, maxCol - 1);

            if (player == destination)
            {
                DrawMap(map, blizzards, player);
                return distances[state];
            }

            if (blizzards.Any(b => b.Position == player))
            {
                throw new Exception("The player is in a blizzard!");
            }

            MoveBlizzards(map, blizzards);

            var (row, col) = player;

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
                if (map.TryGetValue(neighbor, out var value) && value == '#')
                {
                    continue;
                }

                // don't move above the map
                if (neighbor.Row < minRow)
                {
                    continue;
                }

                // don't move below the map
                if (neighbor.Row > maxRow)
                {
                    continue;
                }

                // don't move into a blizzard
                if (blizzards.Any(b => b.Position == neighbor))
                {
                    continue;
                }

                distances[(map, blizzards, neighbor)] = distances[state] + 1;
                queue.Enqueue((map, blizzards, neighbor));
            }

            // don't move at all
            distances[(map, blizzards, player)] = distances[state] + 1;
            queue.Enqueue((map, blizzards, player));
        }

        return 0;
    }

    private static void MoveBlizzards(Dictionary<(int Row, int Col), char> map, List<Blizzard> blizzards)
    {
        foreach (var blizzard in blizzards)
        {
            var (row, col, direction) = blizzard;

            blizzard.Position = direction switch
            {
                '^' => (row - 1, col),
                'v' => (row + 1, col),
                '<' => (row, col - 1),
                '>' => (row, col + 1),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (map.TryGetValue(blizzard.Position, out var value) && value == '#')
            {
                var minRow = map.Min(n => n.Key.Row);
                var maxRow = map.Max(n => n.Key.Row);
                var minCol = map.Min(n => n.Key.Col);
                var maxCol = map.Max(n => n.Key.Col);

                blizzard.Position = direction switch
                {
                    '^' => (maxRow - 1, col),
                    'v' => (minRow + 1, col),
                    '<' => (row, maxCol - 1),
                    '>' => (row, minCol + 1),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
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

                    if (player == (row, col) && b.Any())
                    {
                        throw new Exception("The player is in a blizzard!");
                    }

                    if (player == (row, col))
                    {
                        value = 'E';
                    }

                    if (b.Count == 1)
                    {
                        value = b.First().Direction;
                    }

                    if (b.Count > 1)
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
