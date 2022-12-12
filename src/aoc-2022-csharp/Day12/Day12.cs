namespace aoc_2022_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt");

    public static int Part1()
    {
        var start = GetStartingPoint();
        return GetShortestPath(start);
    }

    public static int Part2()
    {
        var startingPoints = GetPossibleStartingPoints();
        return startingPoints.Min(GetShortestPath);
    }

    private static int GetShortestPath((int Row, int Col) start)
    {
        var queue = new PriorityQueue<(int Row, int Col), int>();
        var distances = InitializeDistances();
        var elevation = InitializeElevations();
        var end = GetEndingPoint();

        distances[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            var (row, col) = node;

            if (node == end)
            {
                return distances[end];
            }

            var neighbors = new[] { (row - 1, col), (row + 1, col), (row, col - 1), (row, col + 1) }
                .Where(n => elevation.ContainsKey(n) && elevation[n] - elevation[node] <= 1).ToArray();

            foreach (var neighbor in neighbors)
            {
                var distance = distances[node] + 1;

                if (distance < distances[neighbor])
                {
                    distances[neighbor] = distance;
                    queue.Enqueue(neighbor, distance);
                }
            }
        }

        return distances[end];
    }

    private static Dictionary<(int Row, int Col), int> InitializeDistances()
    {
        var distances = new Dictionary<(int Row, int Col), int>();

        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[0].Length; col++)
            {
                distances[(row, col)] = int.MaxValue;
            }
        }

        return distances;
    }

    private static Dictionary<(int Row, int Col), int> InitializeElevations()
    {
        var elevation = new Dictionary<(int Row, int Col), int>();

        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[0].Length; col++)
            {
                elevation[(row, col)] = Input[row][col] switch
                {
                    'S' => 0,
                    'E' => 25,
                    _ => Input[row][col] - 'a'
                };
            }
        }

        return elevation;
    }

    private static (int Row, int Col) GetStartingPoint()
    {
        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[0].Length; col++)
            {
                if (Input[row][col] == 'S')
                {
                    return (row, col);
                }
            }
        }

        return (0, 0);
    }

    private static (int Row, int Col) GetEndingPoint()
    {
        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[0].Length; col++)
            {
                if (Input[row][col] == 'E')
                {
                    return (row, col);
                }
            }
        }

        return (0, 0);
    }

    private static List<(int Row, int Col)> GetPossibleStartingPoints()
    {
        var startingPoints = new List<(int Row, int Col)>();

        for (var row = 0; row < Input.Length; row++)
        {
            for (var col = 0; col < Input[0].Length; col++)
            {
                if (Input[row][col] == 'S' || Input[row][col] == 'a')
                {
                    startingPoints.Add((row, col));
                }
            }
        }

        return startingPoints;
    }
}
