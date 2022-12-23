namespace aoc_2022_csharp.Day23;

public static class Day23
{
    private static readonly string[] Input = File.ReadAllLines("Day23/day23.txt");

    public static int Part1()
    {
        var map = GetMap();

        // Console.WriteLine("== Initial State ==" );
        // DrawMap(map);
        // Console.WriteLine();

        for (var round = 0; round < 10; round++)
        {
            var proposedMoves = GetProposedMoves(map, round);

            var rejectedMoves = proposedMoves
                .GroupBy(move => move.Position)
                .Select(group => (Position: group.Key, Count: group.Count()))
                .Where(group => group.Count > 1)
                .Select(group => group.Position)
                .ToList();

            proposedMoves.RemoveAll(move => rejectedMoves.Contains(move.Position));

            var stationaryElves = map
                .Where((_, index) => !proposedMoves.Any(move => move.Index == index))
                .Select(x => x.Key)
                .ToList();

            var combined = proposedMoves.Select(move => move.Position).Union(stationaryElves);

            map.Clear();

            foreach (var elf in combined)
            {
                map[elf] = '#';
            }

            // Console.WriteLine($"== Round {round} ==");
            // DrawMap(map);
            // Console.WriteLine();
        }

        return CountEmptyGroundTiles(map);
    }

    public static int Part2()
    {
        var map = GetMap();

        for (var round = 0; round < int.MaxValue; round++)
        {
            Console.WriteLine($"round: {round}");

            var proposedMoves = GetProposedMoves(map, round);

            var rejectedMoves = proposedMoves
                .GroupBy(move => move.Position)
                .Select(group => (Position: group.Key, Count: group.Count()))
                .Where(group => group.Count > 1)
                .Select(group => group.Position)
                .ToList();

            proposedMoves.RemoveAll(move => rejectedMoves.Contains(move.Position));

            if (proposedMoves.Count == 0)
            {
                return round + 1;
            }

            var stationaryElves = map
                .Where((_, index) => !proposedMoves.Any(move => move.Index == index))
                .Select(x => x.Key)
                .ToList();

            var combined = proposedMoves.Select(move => move.Position).Union(stationaryElves);

            map.Clear();

            foreach (var elf in combined)
            {
                map[elf] = '#';
            }

            // Console.WriteLine($"== Round {round} ==");
            // DrawMap(map);
            // Console.WriteLine();
        }

        return 0;
    }

    private static int CountEmptyGroundTiles(Dictionary<(int Row, int Col), char> map)
    {
        var count = 0;

        var minRow = map.Min(n => n.Key.Row);
        var maxRow = map.Max(n => n.Key.Row);
        var minCol = map.Min(n => n.Key.Col);
        var maxCol = map.Max(n => n.Key.Col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                var value = map.TryGetValue((row, col), out var v) ? v : '.';
                count += value == '.' ? 1 : 0;
            }
        }

        return count;
    }

    private static List<(int Index, (int Row, int Col) Position)> GetProposedMoves(Dictionary<(int Row, int Col), char> map, int round)
    {
        var proposedMoves = new List<(int Index, (int Row, int Col))>();

        for (var i = 0; i < map.Keys.Count; i++)
        {
            var (row, col) = map.Keys.ElementAt(i);

            var neighbors = new[]
            {
                (row - 1, col - 1), // 0
                (row - 1, col),     // 1
                (row - 1, col + 1), // 2
                (row, col - 1),     // 3
                (row, col + 1),     // 4
                (row + 1, col - 1), // 5
                (row + 1, col),     // 6
                (row + 1, col + 1), // 7
            };

            if (!map.Any(m => neighbors.Contains(m.Key)))
            {
                continue;
            }

            if (round % 4 == 0)
            {
                // look north
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[1]) && !map.ContainsKey(neighbors[2]))
                {
                    // propose moving north
                    proposedMoves.Add((i, (row - 1, col)));
                    continue;
                }

                // look south
                if (!map.ContainsKey(neighbors[5]) && !map.ContainsKey(neighbors[6]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving south
                    proposedMoves.Add((i, (row + 1, col)));
                    continue;
                }

                // look west
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[3]) && !map.ContainsKey(neighbors[5]))
                {
                    // propose moving west
                    proposedMoves.Add((i, (row, col - 1)));
                    continue;
                }

                // look east
                if (!map.ContainsKey(neighbors[2]) && !map.ContainsKey(neighbors[4]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving east
                    proposedMoves.Add((i, (row, col + 1)));
                    continue;
                }
            }
            else if (round % 4 == 1)
            {
                // look south
                if (!map.ContainsKey(neighbors[5]) && !map.ContainsKey(neighbors[6]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving south
                    proposedMoves.Add((i, (row + 1, col)));
                    continue;
                }

                // look west
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[3]) && !map.ContainsKey(neighbors[5]))
                {
                    // propose moving west
                    proposedMoves.Add((i, (row, col - 1)));
                    continue;
                }

                // look east
                if (!map.ContainsKey(neighbors[2]) && !map.ContainsKey(neighbors[4]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving east
                    proposedMoves.Add((i, (row, col + 1)));
                    continue;
                }

                // look north
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[1]) && !map.ContainsKey(neighbors[2]))
                {
                    // propose moving north
                    proposedMoves.Add((i, (row - 1, col)));
                    continue;
                }
            }
            else if (round % 4 == 2)
            {
                // look west
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[3]) && !map.ContainsKey(neighbors[5]))
                {
                    // propose moving west
                    proposedMoves.Add((i, (row, col - 1)));
                    continue;
                }

                // look east
                if (!map.ContainsKey(neighbors[2]) && !map.ContainsKey(neighbors[4]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving east
                    proposedMoves.Add((i, (row, col + 1)));
                    continue;
                }

                // look north
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[1]) && !map.ContainsKey(neighbors[2]))
                {
                    // propose moving north
                    proposedMoves.Add((i, (row - 1, col)));
                    continue;
                }

                // look south
                if (!map.ContainsKey(neighbors[5]) && !map.ContainsKey(neighbors[6]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving south
                    proposedMoves.Add((i, (row + 1, col)));
                    continue;
                }
            }
            else if (round % 4 == 3)
            {
                // look east
                if (!map.ContainsKey(neighbors[2]) && !map.ContainsKey(neighbors[4]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving east
                    proposedMoves.Add((i, (row, col + 1)));
                    continue;
                }

                // look north
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[1]) && !map.ContainsKey(neighbors[2]))
                {
                    // propose moving north
                    proposedMoves.Add((i, (row - 1, col)));
                    continue;
                }

                // look south
                if (!map.ContainsKey(neighbors[5]) && !map.ContainsKey(neighbors[6]) && !map.ContainsKey(neighbors[7]))
                {
                    // propose moving south
                    proposedMoves.Add((i, (row + 1, col)));
                    continue;
                }

                // look west
                if (!map.ContainsKey(neighbors[0]) && !map.ContainsKey(neighbors[3]) && !map.ContainsKey(neighbors[5]))
                {
                    // propose moving west
                    proposedMoves.Add((i, (row, col - 1)));
                    continue;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        return proposedMoves;
    }

    private static Dictionary<(int Row, int Col), char> GetMap()
    {
        var map = new Dictionary<(int, int), char>();

        for (var i = 0; i < Input.Length; i++)
        {
            for (var j = 0; j < Input[i].Length; j++)
            {
                if (Input[i][j] == '#')
                {
                    map[(i, j)] = '#';
                }
            }
        }

        return map;
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
                var value = map.TryGetValue((row, col), out var v) ? v : '.';
                Console.Write(value);
            }

            Console.WriteLine();
        }
    }
}
