namespace aoc_2022_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt");
    private static readonly Dictionary<(Valve a, Valve b), int> Distances = new();
    private static readonly List<Valve> Valves = GetValves();

    public static int Part1()
    {
        var valves = GetValves();
        var start = valves.First(v => v.Name == "AA");

        return GetHighestScore(start, valves);
    }

    public static int Part2() => 2;

    private static int GetHighestScore(Valve s, List<Valve> v)
    {
        var scores = new Dictionary<(int t, Valve currentValve, List<Valve> valves, (int t, string Name)[] openValves), int>();
        scores[(1, s, v, Array.Empty<(int, string)>())] = 0;

        var queue = new PriorityQueue<(int t, Valve currentValve, List<Valve> valves, (int t, string Name)[] openValves), int>();
        queue.Enqueue((1, s, v, Array.Empty<(int, string)>()), 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            var (time, valve, valves, openValves) = node;

            var flowRate = valves.Where(x => openValves.Any(y => x.Name == y.Name)).Sum(x => x.FlowRate);

            if (!scores.ContainsKey(node))
            {
                scores[node] = flowRate * (31 - time);
            }

            if (valve.FlowRate > 0 && openValves.All(x => x.Name != valve.Name))
            {
                openValves = openValves.Append((time, valve.Name)).ToArray();
                time++;
            }

            var query = valves.Where(x => x.FlowRate > 0 && openValves.All(y => y.Name != x.Name))
                .Select(x => (Valve: x, Distance: GetDistance(valve, x)))
                .Select(x => (Valve: x.Valve, Distance: x.Distance, PotentialScore: (31 - time - x.Distance - 1) * x.Valve.FlowRate))
                .ToList();

            foreach (var q in query)
            {
                var next = (time + q.Distance, q.Valve, new List<Valve>(valves), openValves);

                scores[next] = scores[node] + q.PotentialScore;
                queue.Enqueue(next, int.MaxValue - q.PotentialScore);
            }
        }

        return scores.Max(x => x.Value);
    }

    private static int GetDistance(Valve a, Valve b)
    {
        if (Distances.TryGetValue((a, b), out var value))
        {
            return value;
        }

        var distances = new Dictionary<string, int>();
        distances[a.Name] = 0;

        var queue = new PriorityQueue<Valve, int>();
        queue.Enqueue(a, 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node == b)
            {
                Distances[(a, b)] = distances[b.Name];
                return distances[b.Name];
            }

            foreach (var child in node.Children.Where(child => distances.All(d => d.Key != child.Valve.Name)))
            {
                distances[child.Valve.Name] = distances[node.Name] + child.Distance;
                queue.Enqueue(child.Valve, child.Distance);
            }
        }

        Distances[(a, b)] = distances[b.Name];
        return distances[b.Name];
    }

    private static List<Valve> GetValves()
    {
        var valves = CreateValves();

        AssignChildren(valves);

        PruneChildren(valves);

        return valves;
    }

    private static List<Valve> CreateValves()
    {
        var valves = new List<Valve>();

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var name = values[1];
            var flowRate = int.Parse(values[4][5..^1]);

            valves.Add(new Valve
            {
                Name = name,
                FlowRate = flowRate,
            });
        }

        return valves;
    }

    private static void AssignChildren(IReadOnlyCollection<Valve> valves)
    {
        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var name = values[1];
            var children = values[9..].Select(c => c.Replace(",", ""));
            var valve = valves.First(v => v.Name == name);

            foreach (var child in children)
            {
                var c = valves.First(v => v.Name == child);
                valve.Children.Add((c, 1));
            }
        }
    }

    private static void PruneChildren(List<Valve> valves)
    {
        foreach (var valve in valves)
        {
            var seen = new HashSet<Valve>();

            while (valve.Children.Any(v => v.Valve.FlowRate == 0))
            {
                var childrenToAdd = new List<(Valve Valve, int Distance)>();
                var childrenToRemove = new List<(Valve Valve, int Distance)>();

                foreach (var child in valve.Children.Where(child => child.Valve.FlowRate == 0))
                {
                    seen.Add(child.Valve);
                    childrenToRemove.Add(child);
                    childrenToAdd.AddRange(
                        child.Valve.Children
                            .Where(c => c.Valve != valve)
                            .Where(c => valve.Children.All(v => v.Valve != c.Valve))
                            .Where(c => seen.All(s => s != c.Valve))
                            .Select(c => (c.Valve, child.Distance + c.Distance)));
                }

                valve.Children.RemoveAll(c => childrenToRemove.Contains(c));
                valve.Children.AddRange(childrenToAdd);
            }
        }
    }

    private record Valve
    {
        public string Name { get; init; } = "";
        public int FlowRate { get; init; }
        public List<(Valve Valve, int Distance)> Children { get; } = new();

        public override string ToString() => Name;
    }
}
