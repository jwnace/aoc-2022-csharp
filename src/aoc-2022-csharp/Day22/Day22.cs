using System.Text;

namespace aoc_2022_csharp.Day22;

public static class Day22
{
    private static readonly string Input = File.ReadAllText("Day22/day22.txt");

    public static int Part1()
    {
        var values = Input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var map = GetMap(values[0]);
        var instructions = GetInstructions(values[1]);

        Console.WriteLine($"map.Count = {map.Count}");
        DrawMap(map);
        Console.WriteLine(string.Join(',', instructions));

        return 1;
    }

    public static int Part2() => 2;

    private static Dictionary<(int Row, int Col), char> GetMap(string input)
    {
        var map = new Dictionary<(int Row, int Col), char>();
        var lines = input.Split(Environment.NewLine);

        for (var i = 0; i < lines.Length; i++)
        {
            for (var j = 0; j < lines[i].Length; j++)
            {
                var (row, col) = (i + 1, j + 1);
                var c = lines[i][j];

                if (c != ' ')
                {
                    map[(row, col)] = lines[i][j];
                }
            }
        }

        return map;
    }

    private static List<object> GetInstructions(string input)
    {
        var builder = new StringBuilder();
        var instructions = new List<object>();

        foreach (var c in input)
        {
            switch (c)
            {
                case 'U':
                case 'D':
                case 'L':
                case 'R':
                    if (builder.Length > 0)
                    {
                        instructions.Add(int.Parse(builder.ToString()));
                        builder.Clear();
                    }

                    instructions.Add(c);
                    break;
                default:
                    builder.Append(c);
                    break;
            }
        }

        if (builder.Length > 0)
        {
            instructions.Add(int.Parse(builder.ToString()));
            builder.Clear();
        }

        return instructions;
    }

    private static void DrawMap(IReadOnlyDictionary<(int Row, int Col), char> map)
    {
        var minRow = map.Min(n => n.Key.Row);
        var maxRow = map.Max(n => n.Key.Row);
        var minCol = map.Min(n => n.Key.Col);
        var maxCol = map.Max(n => n.Key.Col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                var value = map.TryGetValue((row, col), out var v) ? v : ' ';
                Console.Write(value);
            }

            Console.WriteLine();
        }
    }
}
