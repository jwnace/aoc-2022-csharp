namespace aoc_2022_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt");

    public static int Part1()
    {
        var map = GetMap();

        var blizzards = map
            .Where(x => x.Value is '^' or 'v' or '<' or '>')
            .Select(x => new Blizzard((x.Key.Row, x.Key.Col), x.Value))
            .ToList();

        var temp = map
            .Where(x => blizzards.Any(b => (b.Position.Row, b.Position.Col) == (x.Key.Row, x.Key.Col)))
            .ToList();

        foreach (var t in temp)
        {
            map.Remove(t.Key);
        }

        DrawMap(map, blizzards);

        for (var i = 0; i < 5; i++)
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
                    var minRow = map.Min(n => n.Key.Row) + 1;
                    var maxRow = map.Max(n => n.Key.Row) - 1;
                    var minCol = map.Min(n => n.Key.Col) + 1;
                    var maxCol = map.Max(n => n.Key.Col) - 1;

                    blizzard.Position = direction switch
                    {
                        '^' => (maxRow, col),
                        'v' => (minRow, col),
                        '<' => (row, maxCol),
                        '>' => (row, minCol),
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            }

            DrawMap(map, blizzards);
        }

        return 1;
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

    private static void DrawMap(IReadOnlyDictionary<(int Row, int Col), char> map, List<Blizzard>? blizzards = null)
    {
        blizzards ??= new List<Blizzard>();

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
