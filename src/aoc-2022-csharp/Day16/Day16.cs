namespace aoc_2022_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt");
    private static readonly List<Valve> Valves = GetValves();
    private static readonly Dictionary<(string cur, string opened, int minutesLeft), int> Memo = new();

    public static int Part1()
    {
        Memo.Clear();
        return MaxScore("AA", "", 30);
    }

    public static int Part2()
    {
        // Memo.Clear();
        // var a = MaxFlow("AA", "", 26, out _);
        // Memo.Clear();
        // var b = MaxFlow("AA", "BB,CC,DD,HH,JJ", 26, out _);

        // return a + b;

        return 2;
    }

    private static int MaxScore(string currentValve, string openedValves, int minutesLeft)
    {
        var key = (currentValve, openedValves, minutesLeft);

        if (Memo.TryGetValue(key, out var value))
        {
            return value;
        }

        if (minutesLeft <= 0)
        {
            return 0;
        }

        var best = 0;
        var valve = Valves.First(x => x.Name == currentValve);
        var score = (minutesLeft - 1) * valve.FlowRate;
        var curOpened = openedValves.Length > 0
            ? string.Join(',', openedValves.Split(',').Append(currentValve).Order())
            : currentValve;

        foreach (var child in valve.Children)
        {
            if (!openedValves.Contains(currentValve) && score > 0)
            {
                best = Math.Max(best, score + MaxScore(child.Valve.Name, curOpened, minutesLeft - child.Distance - 1));
            }

            best = Math.Max(best, MaxScore(child.Valve.Name, openedValves, minutesLeft - child.Distance));
        }

        Memo[key] = best;
        return best;
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
