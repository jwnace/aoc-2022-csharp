using System.Diagnostics;

namespace aoc_2022_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt");

    // public static int Part0()
    // {
    //     var valves = GetValves();
    //     var total = 0;
    //     var currentValve = valves[0];
    //
    //     for (int i = 1; i <= 30; i++)
    //     {
    //         var openValves = valves.Where(v => v.State == ValveState.Open);
    //         var flowRate = openValves.Sum(v => v.FlowRate);
    //
    //         total += flowRate;
    //
    //         Console.WriteLine($"== Minute {i} ==");
    //
    //         var openValveString = openValves.Count() switch
    //         {
    //             0 => "No valves are open",
    //             1 => $"Valve {String.Join(", ", openValves)} is open",
    //             _ => $"Valves {String.Join(", ", openValves)} are open",
    //         };
    //
    //         Console.WriteLine($"{openValveString}, releasing {flowRate} pressure.");
    //
    //         if (i == 2)
    //         {
    //             valves.First(v => v.Name == "DD").Open();
    //             Console.WriteLine("You open valve DD");
    //         }
    //
    //         if (i == 5)
    //         {
    //             valves.First(v => v.Name == "BB").Open();
    //             Console.WriteLine("You open valve BB");
    //         }
    //
    //         if (i == 9)
    //         {
    //             valves.First(v => v.Name == "JJ").Open();
    //             Console.WriteLine("You open valve JJ");
    //         }
    //
    //         if (i == 17)
    //         {
    //             valves.First(v => v.Name == "HH").Open();
    //             Console.WriteLine("You open valve HH");
    //         }
    //
    //         if (i == 21)
    //         {
    //             valves.First(v => v.Name == "EE").Open();
    //             Console.WriteLine("You open valve EE");
    //         }
    //
    //         if (i == 24)
    //         {
    //             valves.First(v => v.Name == "CC").Open();
    //             Console.WriteLine("You open valve CC");
    //         }
    //
    //         Console.WriteLine();
    //     }
    //
    //     return total;
    // }

    public static int Part1()
    {
        var valves = GetValves();
        var firstValve = valves.First(v => v.Name == "AA");

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var possibilities = Step(1, firstValve.Name, new List<Valve>(valves));
        stopwatch.Stop();
        Console.WriteLine($"took {stopwatch.Elapsed} to run");

        return possibilities.Max();
    }

    public static int Part2() => 2;

    private static List<int> Step(int minute, string currentValveName, List<Valve> valves)
    {
        // Console.WriteLine($"simulating minute {minute}");

        var openValves = valves.Where(v => v.State == ValveState.Open).ToList();
        var flowRate = openValves.Sum(v => v.FlowRate);

        if (minute == 30)
        {
            // there is only one possibility for minute 30, we don't have time to move anywhere or open any valves
            return new List<int> { flowRate };
        }

        var possibilities = new List<int>();
        var currentValve = valves.First(v => v.Name == currentValveName);

        // if the current valve is closed, add a possibility where I open it
        if (currentValve.State == ValveState.Closed)
        {
            // HACK: this seems to work but I hate it... there has to be a better way to go about this...
            var temp = currentValve with { State = ValveState.Open };
            var tempValves = new List<Valve>(valves);
            tempValves.Remove(currentValve);
            tempValves.Add(temp);

            possibilities.AddRange(
                Step(minute + 1, currentValveName, new List<Valve>(tempValves)).Select(x => x + flowRate));
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

        public Valve() { }

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
