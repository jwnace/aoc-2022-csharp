namespace aoc_2022_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt");

    public static int Part1()
    {
        var valves = GetValves();
        var firstValve = valves.First(v => v.Name == "AA");

        var possibilities = Step(1, firstValve.Name, new List<Valve>(valves));

        return possibilities.Max();
    }

    public static int Part2() => 2;

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
            possibilities.AddRange(Step(minute + 1, child.Name, new List<Valve>(valves)).Select(x => x + flowRate));
        }

        return possibilities;
    }

    private static List<Valve> GetValves()
    {
        var valves = CreateValves();

        AssignChildren(valves);

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
                valve.Children.Add(c);
            }
        }
    }

    private record Valve
    {
        public string Name { get; init; }
        public int FlowRate { get; init; }
        public ValveState State { get; init; } = ValveState.Closed;
        public List<Valve> Children { get; } = new();

        public Valve()
        {
        }

        public Valve(Valve other)
        {
            Name = other.Name;
            FlowRate = other.FlowRate;
            State = other.State;
            // TODO: is this going to be a problem because the children are the same instances across the board?
            Children = new List<Valve>(other.Children);
        }

        public override string ToString() => Name;
    }

    private enum ValveState
    {
        Closed,
        Open
    }
}
