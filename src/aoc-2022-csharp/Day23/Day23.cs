namespace aoc_2022_csharp.Day23;

public static class Day23
{
    private static readonly string[] Input = File.ReadAllLines("Day23/day23.txt");

    public static int Part1() => Solve(1);

    public static int Part2() => Solve(2);

    private static int Solve(int part)
    {
        var map = GetMap();
        var rounds = part == 2 ? int.MaxValue : 10;

        for (var round = 0; round < rounds; round++)
        {
            var moves = GetProposedMoves(map, round).ToList();
            RemoveRejectedMoves(moves);

            if (part == 2 && moves.Count == 0)
            {
                return round + 1;
            }

            var stationaryElves = GetStationaryElves(map, moves);
            UpdateMap(map, moves, stationaryElves);
        }

        return CountEmptyGroundTiles(map);
    }

    private static HashSet<(int Row, int Col)> GetMap()
    {
        var map = new HashSet<(int Row, int Col)>();

        for (var i = 0; i < Input.Length; i++)
        {
            for (var j = 0; j < Input[i].Length; j++)
            {
                if (Input[i][j] == '#')
                {
                    map.Add((i, j));
                }
            }
        }

        return map;
    }

    private static IEnumerable<(int Index, (int Row, int Col) Position)> GetProposedMoves(
        HashSet<(int Row, int Col)> map,
        int round)
    {
        for (var i = 0; i < map.Count; i++)
        {
            var (row, col) = map.ElementAt(i);

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

            if (AllNeighborsAreEmpty(map, neighbors))
            {
                continue;
            }

            if (round % 4 == 0)
            {
                if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[1]) && !map.Contains(neighbors[2]))
                {
                    // north
                    yield return (i, (row - 1, col));
                }
                else if (!map.Contains(neighbors[5]) && !map.Contains(neighbors[6]) && !map.Contains(neighbors[7]))
                {
                    // south
                    yield return (i, (row + 1, col));
                }
                else if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[3]) && !map.Contains(neighbors[5]))
                {
                    // west
                    yield return (i, (row, col - 1));
                }
                else if (!map.Contains(neighbors[2]) && !map.Contains(neighbors[4]) && !map.Contains(neighbors[7]))
                {
                    // east
                    yield return (i, (row, col + 1));
                }
            }
            else if (round % 4 == 1)
            {
                if (!map.Contains(neighbors[5]) && !map.Contains(neighbors[6]) && !map.Contains(neighbors[7]))
                {
                    // south
                    yield return (i, (row + 1, col));
                }
                else if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[3]) && !map.Contains(neighbors[5]))
                {
                    // west
                    yield return (i, (row, col - 1));
                }
                else if (!map.Contains(neighbors[2]) && !map.Contains(neighbors[4]) && !map.Contains(neighbors[7]))
                {
                    // east
                    yield return (i, (row, col + 1));
                }
                else if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[1]) && !map.Contains(neighbors[2]))
                {
                    // north
                    yield return (i, (row - 1, col));
                }
            }
            else if (round % 4 == 2)
            {
                if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[3]) && !map.Contains(neighbors[5]))
                {
                    // west
                    yield return (i, (row, col - 1));
                }
                else if (!map.Contains(neighbors[2]) && !map.Contains(neighbors[4]) && !map.Contains(neighbors[7]))
                {
                    // east
                    yield return (i, (row, col + 1));
                }
                else if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[1]) && !map.Contains(neighbors[2]))
                {
                    // north
                    yield return (i, (row - 1, col));
                }
                else if (!map.Contains(neighbors[5]) && !map.Contains(neighbors[6]) && !map.Contains(neighbors[7]))
                {
                    // south
                    yield return (i, (row + 1, col));
                }
            }
            else if (round % 4 == 3)
            {
                if (!map.Contains(neighbors[2]) && !map.Contains(neighbors[4]) && !map.Contains(neighbors[7]))
                {
                    // east
                    yield return (i, (row, col + 1));
                }
                else if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[1]) && !map.Contains(neighbors[2]))
                {
                    // north
                    yield return (i, (row - 1, col));
                }
                else if (!map.Contains(neighbors[5]) && !map.Contains(neighbors[6]) && !map.Contains(neighbors[7]))
                {
                    // south
                    yield return (i, (row + 1, col));
                }
                else if (!map.Contains(neighbors[0]) && !map.Contains(neighbors[3]) && !map.Contains(neighbors[5]))
                {
                    // west
                    yield return (i, (row, col - 1));
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    private static void RemoveRejectedMoves(List<(int Index, (int Row, int Col) Position)> proposedMoves)
    {
        var rejectedMoves = proposedMoves
            .GroupBy(move => move.Position)
            .Select(group => (Position: group.Key, Count: group.Count()))
            .Where(group => group.Count > 1)
            .Select(group => group.Position)
            .ToList();

        proposedMoves.RemoveAll(move => rejectedMoves.Contains(move.Position));
    }

    private static List<(int Row, int Col)> GetStationaryElves(
        HashSet<(int Row, int Col)> map,
        List<(int Index, (int Row, int Col) Position)> proposedMoves) =>
        map.Where((_, index) => proposedMoves.All(move => move.Index != index)).ToList();

    private static void UpdateMap(
        HashSet<(int Row, int Col)> map,
        List<(int Index, (int Row, int Col) Position)> proposedMoves,
        List<(int Row, int Col)> stationaryElves)
    {
        var combined = proposedMoves
            .Select(move => move.Position)
            .Union(stationaryElves);

        map.Clear();

        foreach (var position in combined)
        {
            map.Add(position);
        }
    }

    private static int CountEmptyGroundTiles(HashSet<(int Row, int Col)> map)
    {
        var count = 0;

        var minRow = map.Min(n => n.Row);
        var maxRow = map.Max(n => n.Row);
        var minCol = map.Min(n => n.Col);
        var maxCol = map.Max(n => n.Col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                count += map.Contains((row, col)) ? 0 : 1;
            }
        }

        return count;
    }

    private static bool AllNeighborsAreEmpty(HashSet<(int Row, int Col)> map, IEnumerable<(int, int)> neighbors) =>
        !neighbors.Any(map.Contains);
}
