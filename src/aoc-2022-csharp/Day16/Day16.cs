namespace aoc_2022_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt");
    private static readonly Dictionary<(Valve a, Valve b), int> Distances = new();

    public static int Part1()
    {
        var valves = GetValves();
        var start = valves.First(v => v.Name == "AA");

        return GetHighestScore(start, valves);
    }

    public static int Part2() => 2;

    private static int GetHighestScore(Valve start, List<Valve> valves)
    {
        var queue = new PriorityQueue<Valve, int>();
        queue.Enqueue(start, 0);
        var t = 1;
        var tPrev = t;
        var score = 0;

        while (t <= 30)
        {
            var openValves = valves.Where(v => v.State == ValveState.Open).ToList();
            var currentFlowRate = openValves.Sum(v => v.FlowRate);
            score += currentFlowRate * (t - tPrev);
            tPrev = t;

            if (queue.Count == 0)
            {
                t++;
                continue;
            }

            var node = queue.Dequeue();

            Console.WriteLine($"== Minute {t} ==");
            var openValveString = openValves.Count switch
            {
                0 => "No valves are open",
                1 => $"Valve {string.Join(", ", openValves)} is open",
                _ => $"Valves {string.Join(", ", openValves)} are open",
            };
            Console.WriteLine($"{openValveString}, releasing {currentFlowRate} pressure.");

            if (node.FlowRate > 0 && node.State == ValveState.Closed)
            {
                Console.WriteLine($"You open valve {node.Name}.");
                node.Open();

                t++;
            }

            var query1 = valves
                .Where(v => v.FlowRate > 0 && v.State == ValveState.Closed)
                .Select(v => (Valve: v, Distance: GetDistance(node, v)))
                .ToList();

            if (!query1.Any())
            {
                t++;
                continue;
            }

            var query2 = query1.Select(c => (
                Valve: c.Valve,
                Distance: c.Distance,
                // total time, minus current time, minus distance, minus 1 step to turn it on
                PotentialScore: (31 - t - c.Distance - 1) * c.Valve.FlowRate)
            ).OrderByDescending(x => x.PotentialScore).ToList();

            var next = query2.MaxBy(v => v.PotentialScore);

            Console.WriteLine($"You move to valve {next.Valve.Name}.");
            Console.WriteLine();

            queue.Enqueue(next.Valve, int.MaxValue - next.Distance);
            t += next.Distance;
        }

        return score;
    }

    private static int GetDistance(Valve a, Valve b)
    {
        if (Distances.ContainsKey((a, b)))
        {
            return Distances[(a, b)];
        }

        var distances = new Dictionary<Valve, int>();
        distances[a] = 0;

        var queue = new PriorityQueue<Valve, int>();
        queue.Enqueue(a, 0);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (node == b)
            {
                Distances[(a, b)] = distances[b];
                return distances[b];
            }

            foreach (var child in node.Children)
            {
                if (distances.Any(d => d.Key == child.Valve))
                {
                    continue;
                }

                distances[child.Valve] = distances[node] + child.Distance;
                queue.Enqueue(child.Valve, child.Distance);
            }
        }

        Distances[(a, b)] = distances[b];
        return distances[b];
    }

    private static IEnumerable<int> Step(int minute, string name, IReadOnlyCollection<Valve> valves)
    {
        var openValves = valves.Where(v => v.State == ValveState.Open).ToList();
        var flowRate = openValves.Sum(v => v.FlowRate);

        if (minute == 30)
        {
            // there is only one possibility for minute 30, we don't have time to move anywhere or open any valves
            return new List<int> { flowRate };
        }

        var possibilities = new List<int>();
        var currentValve = valves.First(v => v.Name == name);

        // if the current valve is closed, add a possibility where I open it
        if (currentValve.State == ValveState.Closed)
        {
            // HACK: this seems to work but I hate it... there has to be a better way to go about this...
            var temp = currentValve with { State = ValveState.Open };
            var tempValves = new List<Valve>(valves);
            tempValves.Remove(currentValve);
            tempValves.Add(temp);

            possibilities.AddRange(Step(minute + 1, name, new List<Valve>(tempValves)).Select(x => x + flowRate));
        }

        // add possibilities to simulate moving to each child
        foreach (var child in currentValve.Children)
        {
            possibilities.AddRange(
                Step(minute + 1, child.Valve.Name, new List<Valve>(valves)).Select(x => x + flowRate));
        }

        return possibilities;
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

    private static void AssignChildren(List<Valve> valves)
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
                            .Where(c => !valve.Children.Any(v => v.Valve == c.Valve))
                            .Where(c => !seen.Any(s => s == c.Valve))
                            .Select(x => (x.Valve, x.Distance + 1)));
                }

                valve.Children.RemoveAll(c => childrenToRemove.Contains(c));
                valve.Children.AddRange(childrenToAdd);
            }
        }
    }

    private record Valve
    {
        public string Name { get; init; }
        public int FlowRate { get; init; }
        public ValveState State { get; set; } = ValveState.Closed;
        public List<(Valve Valve, int Distance)> Children { get; } = new();

        public Valve()
        {
        }

        public Valve(Valve other)
        {
            Name = other.Name;
            FlowRate = other.FlowRate;
            State = other.State;
            // TODO: is this going to be a problem because the children are the same instances across the board?
            Children = new List<(Valve Valve, int Distance)>(other.Children);
        }

        public void Open()
        {
            State = ValveState.Open;
        }

        public override string ToString() => Name;
    }

    private enum ValveState
    {
        Closed,
        Open
    }
}
