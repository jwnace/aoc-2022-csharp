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

        var startingRow = map.Min(m => m.Key.Row);
        var startingCol = map.Where(m => m.Key.Row == startingRow).Min(m => m.Key.Col);

        var state = (Row: startingRow, Col: startingCol, Facing: Facing.Right);
        map[(state.Row, state.Col)] = '>';

        foreach (var instruction in instructions)
        {
            if (instruction is int steps)
            {
                Move(ref state, map, steps);
            }

            if (instruction is char turn)
            {
                Turn(ref state, map, turn);
            }
        }

        var (row, col, facing) = state;
        return (1000 * row) + (4 * col) + (int)facing;
    }

    public static int Part2()
    {
        var values = Input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var map = GetMap(values[0]);
        var instructions = GetInstructions(values[1]);

        var startingRow = map.Min(m => m.Key.Row);
        var startingCol = map.Where(m => m.Key.Row == startingRow).Min(m => m.Key.Col);

        var state = (Row: startingRow, Col: startingCol, Facing: Facing.Right);
        map[(state.Row, state.Col)] = '>';

        foreach (var instruction in instructions)
        {
            if (instruction is int steps)
            {
                MovePart2(ref state, map, steps);
            }

            if (instruction is char turn)
            {
                Turn(ref state, map, turn);
            }
        }

        var (row, col, facing) = state;
        return (1000 * row) + (4 * col) + (int)facing;
    }

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

    private static void Move(ref (int, int, Facing) state, Dictionary<(int Row, int Col), char> map, int steps)
    {
        var (row, col, facing) = state;

        for (var i = 0; i < steps; i++)
        {
            if (!TryMove(ref state, map))
            {
                break;
            }

            // HACK: update row, col, and facing since they may have been changed by the above code
            // TODO: instead of using tuples everywhere, I should make a private record or class to track the state
            (row, col, facing) = state;

            map[(row, col)] = facing switch
            {
                Facing.Right => '>',
                Facing.Left => '<',
                Facing.Up => '^',
                Facing.Down => 'v',
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        state = (row, col, facing);
    }

    private static void MovePart2(ref (int, int, Facing) state, Dictionary<(int Row, int Col), char> map, int steps)
    {
        var (row, col, facing) = state;

        for (var i = 0; i < steps; i++)
        {
            if (!TryMovePart2(ref state, map))
            {
                break;
            }

            // HACK: update row, col, and facing since they may have been changed by the above code
            // TODO: instead of using tuples everywhere, I should make a private record or class to track the state
            (row, col, facing) = state;

            map[(row, col)] = facing switch
            {
                Facing.Right => '>',
                Facing.Left => '<',
                Facing.Up => '^',
                Facing.Down => 'v',
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        state = (row, col, facing);
    }

    private static bool TryMove(ref (int, int, Facing) state, Dictionary<(int Row, int Col), char> map)
    {
        var (row, col, facing) = state;

        var temp = facing switch
        {
            Facing.Right => (Row: row, Col: col + 1),
            Facing.Left => (Row: row, Col: col - 1),
            Facing.Up => (Row: row - 1, Col: col),
            Facing.Down => (Row: row + 1, Col: col),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (map.ContainsKey(temp))
        {
            if (map[temp] == '#')
            {
                return false;
            }

            state = (temp.Row, temp.Col, facing);
            return true;
        }

        switch (facing)
        {
            case Facing.Left:
                var maxCol = map.Where(m => m.Key.Row == temp.Row).Max(m => m.Key.Col);
                temp = (temp.Row, maxCol);
                break;
            case Facing.Right:
                var minCol = map.Where(m => m.Key.Row == temp.Row).Min(m => m.Key.Col);
                temp = (temp.Row, minCol);
                break;
            case Facing.Up:
                var maxRow = map.Where(m => m.Key.Col == temp.Col).Max(m => m.Key.Row);
                temp = (maxRow, temp.Col);
                break;
            case Facing.Down:
                var minRow = map.Where(m => m.Key.Col == temp.Col).Min(m => m.Key.Row);
                temp = (minRow, temp.Col);
                break;
            default:
                throw new InvalidOperationException();
        }

        if (map[temp] == '#')
        {
            return false;
        }

        state = (temp.Row, temp.Col, facing);
        return true;
    }

    private static bool TryMovePart2(ref (int, int, Facing) state, Dictionary<(int Row, int Col), char> map)
    {
        var (row, col, facing) = state;

        var temp = facing switch
        {
            Facing.Right => (Row: row, Col: col + 1, Facing: facing),
            Facing.Left => (Row: row, Col: col - 1, Facing: facing),
            Facing.Up => (Row: row - 1, Col: col, Facing: facing),
            Facing.Down => (Row: row + 1, Col: col, Facing: facing),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (map.ContainsKey((temp.Row, temp.Col)))
        {
            if (map[(temp.Row, temp.Col)] == '#')
            {
                return false;
            }

            state = temp;
            return true;
        }

        // we went over an edge, and we need to wrap
        switch (facing)
        {
            case Facing.Right when (row - 1) / 50 == 0:
            {
                var tempRow = 100 + (50 - row + 1);
                var tempCol = map.Where(m => m.Key.Row == tempRow).Max(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Left);
                break;
            }
            case Facing.Right when (row - 1) / 50 == 1:
            {
                var tempCol = 100 + (row - 50);
                var tempRow = map.Where(m => m.Key.Col == tempCol).Max(m => m.Key.Row);
                temp = (tempRow, tempCol, Facing.Up);
                break;
            }
            case Facing.Right when (row - 1) / 50 == 2:
            {
                var tempRow = 0 + (150 - row + 1);
                var tempCol = map.Where(m => m.Key.Row == tempRow).Max(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Left);
                break;
            }
            case Facing.Right when (row - 1) / 50 == 3:
            {
                var tempCol = 50 + (row - 150);
                var tempRow = map.Where(m => m.Key.Col == tempCol).Max(m => m.Key.Row);
                temp = (tempRow, tempCol, Facing.Up);
                break;
            }
            case Facing.Left when (row - 1) / 50 == 0:
            {
                var tempRow = 100 + (50 - row + 1);
                var tempCol = map.Where(m => m.Key.Row == tempRow).Min(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Right);
                break;
            }
            case Facing.Left when (row - 1) / 50 == 1:
            {
                var tempCol = row - 50;
                var tempRow = map.Where(m => m.Key.Col == tempCol).Min(m => m.Key.Row);
                temp = (tempRow, tempCol, Facing.Down);
                break;
            }
            case Facing.Left when (row - 1) / 50 == 2:
            {
                var tempRow = 150 - row + 1;
                var tempCol = map.Where(m => m.Key.Row == tempRow).Min(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Right);
                break;
            }
            case Facing.Left when (row - 1) / 50 == 3:
            {
                var tempCol = 50 + (row - 150);
                var tempRow = map.Where(m => m.Key.Col == tempCol).Min(m => m.Key.Row);
                temp = (tempRow, tempCol, Facing.Down);
                break;
            }
            case Facing.Up when (col - 1) / 50 == 0:
            {
                var tempRow = 50 + col;
                var tempCol = map.Where(m => m.Key.Row == tempRow).Min(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Right);
                break;
            }
            case Facing.Up when (col - 1) / 50 == 1:
            {
                var tempRow = 150 + col - 50;
                var tempCol = map.Where(m => m.Key.Row == tempRow).Min(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Right);
                break;
            }
            case Facing.Up when (col - 1) / 50 == 2:
            {
                var tempCol = col - 100;
                var tempRow = map.Where(m => m.Key.Col == tempCol).Max(m => m.Key.Row);
                temp = (tempRow, tempCol, Facing.Up);
                break;
            }
            case Facing.Down when (col - 1) / 50 == 0:
            {
                var tempCol = 100 + col;
                var tempRow = map.Where(m => m.Key.Col == tempCol).Min(m => m.Key.Row);
                temp = (tempRow, tempCol, Facing.Down);
                break;
            }
            case Facing.Down when (col - 1) / 50 == 1:
            {
                var tempRow = 150 + col - 50;
                var tempCol = map.Where(m => m.Key.Row == tempRow).Max(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Left);
                break;
            }
            case Facing.Down when (col - 1) / 50 == 2:
            {
                var tempRow = 50 + col - 100;
                var tempCol = map.Where(m => m.Key.Row == tempRow).Max(m => m.Key.Col);
                temp = (tempRow, tempCol, Facing.Left);
                break;
            }
            default:
            {
                throw new InvalidOperationException();
            }
        }

        if (map[(temp.Row, temp.Col)] == '#')
        {
            return false;
        }

        state = (temp.Row, temp.Col, temp.Facing);
        return true;
    }

    private static void Turn(ref (int, int, Facing) state, Dictionary<(int Row, int Col), char> map, char turn)
    {
        var (row, col, facing) = state;

        facing = facing switch
        {
            Facing.Right when turn == 'R' => Facing.Down,
            Facing.Right when turn == 'L' => Facing.Up,
            Facing.Left when turn == 'R' => Facing.Up,
            Facing.Left when turn == 'L' => Facing.Down,
            Facing.Up when turn == 'R' => Facing.Right,
            Facing.Up when turn == 'L' => Facing.Left,
            Facing.Down when turn == 'R' => Facing.Left,
            Facing.Down when turn == 'L' => Facing.Right,
            _ => throw new ArgumentOutOfRangeException()
        };

        map[(row, col)] = facing switch
        {
            Facing.Right => '>',
            Facing.Left => '<',
            Facing.Up => '^',
            Facing.Down => 'v',
            _ => throw new ArgumentOutOfRangeException()
        };

        state = (row, col, facing);
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

    private enum Facing
    {
        Right,
        Down,
        Left,
        Up
    }
}
