namespace aoc_2022_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt");
    private static readonly List<Valve> Valves = GetValves();
    private static readonly Dictionary<(string, string, int), int> MemoPart1 = new();
    private static readonly Dictionary<(string, string, string, int), int> MemoPart2 = new();

    public static int Part1() => MaxScorePart1("AA", "", 30);

    public static int Part2() => MaxScorePart2("AA", "AA", "", 26);

    private static int MaxScorePart1(string player1, string openValves, int timeLeft)
    {
        var state = (player1, openValves, timeLeft);

        if (MemoPart1.TryGetValue(state, out var value))
        {
            return value;
        }

        if (timeLeft <= 0)
        {
            return 0;
        }

        if (Valves.All(v => v.FlowRate == 0 || openValves.Contains(v.Name)))
        {
            return 0;
        }

        var best = 0;
        var p1Valve = Valves.First(x => x.Name == player1);
        var p1Val = (timeLeft - 1) * p1Valve.FlowRate;

        if (!openValves.Contains(player1) && p1Valve.FlowRate > 0)
        {
            var p1Opened = openValves.Length > 0
                ? string.Join(',', openValves.Split(',').Append(player1).Order())
                : player1;

            best = Math.Max(best, p1Val + MaxScorePart1(player1, p1Opened, timeLeft - 1));
        }

        foreach (var p1Neighbor in p1Valve.Neighbors)
        {
            best = Math.Max(best, MaxScorePart1(p1Neighbor.Name, openValves, timeLeft - 1));
        }

        MemoPart1[state] = best;
        return best;
    }

    private static int MaxScorePart2(string player1, string player2, string openValves, int timeLeft)
    {
        var state = (player1, player2, openValves, timeLeft);

        if (MemoPart2.TryGetValue(state, out var value))
        {
            return value;
        }

        if (timeLeft <= 0)
        {
            return 0;
        }

        if (Valves.All(v => v.FlowRate == 0 || openValves.Contains(v.Name)))
        {
            return 0;
        }

        // TODO: remove hard coded timeLeft value
        if (player1 == player2 && timeLeft < 26)
        {
            return 0;
        }

        // TODO: are there other states that I can prune to make this run faster?

        var best = 0;
        var p1Valve = Valves.First(v => v.Name == player1);
        var p2Valve = Valves.First(v => v.Name == player2);
        var p1Val = (timeLeft - 1) * p1Valve.FlowRate;
        var p2Val = (timeLeft - 1) * p2Valve.FlowRate;

        if ((!openValves.Contains(player1) && p1Valve.FlowRate > 0) &&
            (!openValves.Contains(player2) && p2Valve.FlowRate > 0))
        {
            var bothOpened = openValves.Length > 0
                ? string.Join(',', openValves.Split(',').Append(player1).Append(player2).Order())
                : string.Join(',', new[] { player1, player2 }.Order());

            best = Math.Max(best, p1Val + p2Val + MaxScorePart2(player1, player2, bothOpened, timeLeft - 1));
        }

        if (!openValves.Contains(player1) && p1Valve.FlowRate > 0)
        {
            var p1Opened = openValves.Length > 0
                ? string.Join(',', openValves.Split(',').Append(player1).Order())
                : player1;

            foreach (var p2Neighbor in p2Valve.Neighbors)
            {
                best = Math.Max(best, p1Val + MaxScorePart2(player1, p2Neighbor.Name, p1Opened, timeLeft - 1));
            }
        }

        if (!openValves.Contains(player2) && p2Valve.FlowRate > 0)
        {
            var p2Opened = openValves.Length > 0
                ? string.Join(',', openValves.Split(',').Append(player2).Order())
                : player2;

            foreach (var p1Neighbor in p1Valve.Neighbors)
            {
                best = Math.Max(best, p2Val + MaxScorePart2(p1Neighbor.Name, player2, p2Opened, timeLeft - 1));
            }
        }

        foreach (var p1Neighbor in p1Valve.Neighbors)
        {
            foreach (var p2Neighbor in p2Valve.Neighbors)
            {
                best = Math.Max(best, MaxScorePart2(p1Neighbor.Name, p2Neighbor.Name, openValves, timeLeft - 1));
            }
        }

        MemoPart2[state] = best;
        return best;
    }

    private static List<Valve> GetValves()
    {
        var valves = CreateValves();
        AssignNeighbors(valves);
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

    private static void AssignNeighbors(IReadOnlyCollection<Valve> valves)
    {
        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var name = values[1];
            var neighbors = values[9..].Select(n => n.Replace(",", ""));
            var valve = valves.First(v => v.Name == name);

            foreach (var neighbor in neighbors)
            {
                valve.Neighbors.Add(valves.First(v => v.Name == neighbor));
            }
        }
    }

    private record Valve
    {
        public string Name { get; init; } = "";
        public int FlowRate { get; init; }
        public List<Valve> Neighbors { get; } = new();

        public override string ToString() => Name;
    }
}
