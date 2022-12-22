using System.Diagnostics;

namespace aoc_2022_csharp.Day16;

public static class Day16
{
    private static readonly string[] Input = File.ReadAllLines("Day16/day16.txt");
    private static readonly List<Valve> Valves = GetValves();
    private static readonly Dictionary<(string cur, string opened, int minutesLeft), int> Memo = new();

    public static int Part1()
    {
        Memo.Clear();

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        MaxScoreRecursive("AA", "", 30);
        var recursive = Memo.Max(m => m.Value);
        var top10ScoresRecursive = Memo.OrderByDescending(m => m.Value).Take(10).ToList();
        var query = Memo.OrderByDescending(m => m.Key.minutesLeft).ThenBy(m => m.Key.cur).Take(100).ToList();
        stopwatch.Stop();
        Console.WriteLine($"MaxScoreRecursive took {stopwatch.Elapsed} to run! Result: {recursive}");

        stopwatch.Reset();
        stopwatch.Start();
        var bfs = MaxScoreBfs(("AA", "", 30));
        stopwatch.Stop();
        Console.WriteLine($"MaxScoreBfs took {stopwatch.Elapsed} to run! Result: {bfs}");

        return 1;
    }

    public static int Part2()
    {
        Memo.Clear();

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        MaxScoreRecursive("AA", "", 26);
        var recursive = Memo.Max(m => m.Value);
        var top10ScoresRecursive = Memo.OrderByDescending(m => m.Value).Take(10).ToList();
        var query = Memo.OrderByDescending(m => m.Key.minutesLeft).ThenBy(m => m.Key.cur).Take(100).ToList();
        stopwatch.Stop();
        Console.WriteLine($"MaxScoreRecursive took {stopwatch.Elapsed} to run! Result: {recursive}");

        stopwatch.Reset();
        stopwatch.Start();
        var bfs = MaxScoreBfs(("AA", "", 26));
        stopwatch.Stop();
        Console.WriteLine($"MaxScoreBfs took {stopwatch.Elapsed} to run! Result: {bfs}");

        stopwatch.Reset();
        stopwatch.Start();
        var elephant = MaxScoreBfs(("AA", "BB,CC,DD,EE,HH,JJ", 26));
        stopwatch.Stop();
        Console.WriteLine($"Elephant took {stopwatch.Elapsed} to run! Result: {elephant}");

        return 2;
    }

    private static int MaxScoreRecursive(string currentValve, string openedValves, int minutesLeft)
    {
        // this is the state we are currently in
        var state = (currentValve, openedValves, minutesLeft);

        // if we've already seen this state before, return the value we got last time
        if (Memo.TryGetValue(state, out var value))
        {
            return value;
        }

        // if there's no time left, return 0
        if (minutesLeft <= 0)
        {
            return 0;
        }

        // valve is the valve we are currently looking at
        var valve = Valves.First(x => x.Name == currentValve);

        // still not sure what this is...
        var best = 0;

        // this is the value that we could add to our score if we open the current valve on this turn
        // this is NOT the score for the current state
        var val = (minutesLeft - 1) * valve.FlowRate;

        // this is the list of valves that would be open on the next turn if we open the current valve on this turn
        var curOpened = openedValves.Length > 0
            ? string.Join(',', openedValves.Split(',').Append(currentValve).Order())
            : currentValve;

        // for each neighbor of the current valve
        foreach (var neighbor in valve.Neighbors)
        {
            // if the current valve is already open, or has a a flow rate of 0, don't try to open it
            if (!openedValves.Contains(currentValve) && val > 0)
            {
                // minutesLeft - 2 because it takes 1 minute to open the valve, and 1 minute to move to the next valve
                var temp = val + MaxScoreRecursive(neighbor.Name, curOpened, minutesLeft - 2);
                if (best > temp)
                {
                    // this can happen because we're considering every neighbor,
                    // and updating `best` with the best neighbor's score
                    var foo = "wtf, how did we get here?";
                }

                best = Math.Max(best, temp);
            }

            // minutesLeft - 1 because it takes 1 minute to move to the next valve, and we didn't open anything
                best = Math.Max(best, 000 + MaxScoreRecursive(neighbor.Name, openedValves, minutesLeft - 1));
        }

        // save the score for this state so we don't calculate it again
        Memo[state] = best;

        // return the score back to the previous call on the call stack
        return best;
    }

    private static int MaxScoreBfs((string, string, int) initialState)
    {
        // this is a queue of states that we still need to consider
        var queue = new Queue<(string, string, int)>();
        queue.Enqueue(initialState);

        // this is a memo of all the states we've already calculated scores for
        var scores = new Dictionary<(string CurrentValve, string OpenedValves, int TimeLeft), (int FlowRate, int Score)>();
        scores[initialState] = (0,0);

        // this is a collection of all the states we've already visited
        var seen = new HashSet<(string, string, int)>();

        while (queue.Count > 0)
        {
            // this is the state we are currently in
            var state = queue.Dequeue();

            // if we've already seen this state before, don't process it again
            if (seen.Contains(state))
            {
                continue;
            }

            // mark this state as "seen"
            seen.Add(state);

            if (state is { Item1: "EE", Item2: "BB,DD,JJ", Item3: 17 })
            {
                var foo = "break here!";
            }

            // this is (another representation of) the state we are currently in
            var (currentValveName, openedValves, minutesLeft) = state;

            // if there's no time left, don't do anything, the score for this state should already be finalized
            // HACK: since we compute the score for the NEXT state, we need to make sure that we don't compute a score
            // for -1 minutes left, it should stop at 0 minutes left
            if (minutesLeft == 1)
            {
                continue;
            }

            // TODO: calculate the score for the current state AND the next state...
            // TODO: I'm not exactly clear on how to do that

            // valve is the valve we are currently looking at
            var valve = Valves.First(v => v.Name == currentValveName);

            // we have two things we can do in this minute...
            // we can open the current valve, or we can move to a neighbor

            // open the current valve (if it's not already open, and it has a non-zero flow rate)
            if (!openedValves.Contains(currentValveName) && valve.FlowRate > 0)
            {
                var currentOpened = openedValves.Length > 0
                    ? string.Join(',', openedValves.Split(',').Append(currentValveName).Order())
                    : currentValveName;

                var nextState = (currentValveName, currentOpened, minutesLeft - 1);

                var flowRate = GetFlowRate(currentOpened);
                var previousScore = scores[state].Score;
                var score = previousScore + flowRate;

                if (scores.ContainsKey(nextState))
                {
                    score = Math.Max(score, scores[nextState].Score);
                    scores[nextState] = (flowRate, score);
                }
                else
                {
                    scores[nextState] = (flowRate, score);
                }

                queue.Enqueue(nextState);
            }

            // don't use else block here because we need to consider the case where opening the valve is a bad choice
            {
                // move to each neighbor
                foreach (var neighbor in valve.Neighbors)
                {
                    var nextState = (neighbor.Name, openedValves, minutesLeft - 1);

                    if (nextState is { Item1: "AA", Item2: "BB,DD", Item3: 24 })
                    {
                        var bar = "break here!";
                    }

                    var flowRate = GetFlowRate(openedValves);
                    var previousScore = scores[state].Score;
                    var score = previousScore + flowRate;

                    if (scores.ContainsKey(nextState))
                    {
                        score = Math.Max(score, scores[nextState].Score);
                        scores[nextState] = (flowRate, score);
                    }
                    else
                    {
                        scores[nextState] = (flowRate, score);
                    }

                    queue.Enqueue(nextState);
                }
            }
        }

        var top10ScoresRecursive = Memo.OrderByDescending(m => m.Value).Take(10).ToList();
        var queryRecursive = Memo
            .OrderByDescending(m => m.Key.minutesLeft)
            .ThenBy(m => m.Key.cur)
            // .Take(100)
            .ToList();

        var top10ScoresBfs = scores.OrderByDescending(m => m.Value).Take(10).ToList();
        var queryBfs = scores
            .OrderByDescending(m => m.Key.Item3)
            .ThenBy(m => m.Key.Item1)
            // .Take(100)
            .ToList();

        var temp01 = scores.SingleOrDefault(s => s.Key is { Item1: "AA", Item2: "", Item3: 30 });
        var temp02 = scores.SingleOrDefault(s => s.Key is { Item1: "DD", Item2: "", Item3: 29 });
        var temp03 = scores.SingleOrDefault(s => s.Key is { Item1: "DD", Item2: "DD", Item3: 28 });
        var temp04 = scores.SingleOrDefault(s => s.Key is { Item1: "CC", Item2: "DD", Item3: 27 });
        var temp05 = scores.SingleOrDefault(s => s.Key is { Item1: "BB", Item2: "DD", Item3: 26 });
        var temp06 = scores.SingleOrDefault(s => s.Key is { Item1: "BB", Item2: "BB,DD", Item3: 25 });
        var temp07 = scores.SingleOrDefault(s => s.Key is { Item1: "AA", Item2: "BB,DD", Item3: 24 });
        var temp08 = scores.SingleOrDefault(s => s.Key is { Item1: "II", Item2: "BB,DD", Item3: 23 });
        var temp09 = scores.SingleOrDefault(s => s.Key is { Item1: "JJ", Item2: "BB,DD", Item3: 22 });
        var temp10 = scores.SingleOrDefault(s => s.Key is { Item1: "JJ", Item2: "BB,DD,JJ", Item3: 21 });
        var temp11 = scores.SingleOrDefault(s => s.Key is { Item1: "II", Item2: "BB,DD,JJ", Item3: 20 });
        var temp12 = scores.SingleOrDefault(s => s.Key is { Item1: "AA", Item2: "BB,DD,JJ", Item3: 19 });
        var temp13 = scores.SingleOrDefault(s => s.Key is { Item1: "DD", Item2: "BB,DD,JJ", Item3: 18 });
        var temp14 = scores.SingleOrDefault(s => s.Key is { Item1: "EE", Item2: "BB,DD,JJ", Item3: 17 });
        var temp15 = scores.SingleOrDefault(s => s.Key is { Item1: "FF", Item2: "BB,DD,JJ", Item3: 16 });
        var temp16 = scores.SingleOrDefault(s => s.Key is { Item1: "GG", Item2: "BB,DD,JJ", Item3: 15 });
        var temp17 = scores.SingleOrDefault(s => s.Key is { Item1: "HH", Item2: "BB,DD,JJ", Item3: 14 });
        var temp18 = scores.SingleOrDefault(s => s.Key is { Item1: "HH", Item2: "BB,DD,HH,JJ", Item3: 13 });
        var temp19 = scores.SingleOrDefault(s => s.Key is { Item1: "GG", Item2: "BB,DD,HH,JJ", Item3: 12 });
        var temp20 = scores.SingleOrDefault(s => s.Key is { Item1: "FF", Item2: "BB,DD,HH,JJ", Item3: 11 });
        var temp21 = scores.SingleOrDefault(s => s.Key is { Item1: "EE", Item2: "BB,DD,HH,JJ", Item3: 10 });
        var temp22 = scores.SingleOrDefault(s => s.Key is { Item1: "EE", Item2: "BB,DD,EE,HH,JJ", Item3: 9 });
        var temp23 = scores.SingleOrDefault(s => s.Key is { Item1: "DD", Item2: "BB,DD,EE,HH,JJ", Item3: 8 });
        var temp24 = scores.SingleOrDefault(s => s.Key is { Item1: "CC", Item2: "BB,DD,EE,HH,JJ", Item3: 7 });
        var temp25 = scores.SingleOrDefault(s => s.Key is { Item1: "CC", Item2: "BB,CC,DD,EE,HH,JJ", Item3: 6 });
        var temp26 = scores.Where(s => s.Key is { Item2: "BB,CC,DD,EE,HH,JJ", Item3: 5 }).ToList();
        var temp27 = scores.Where(s => s.Key is { Item2: "BB,CC,DD,EE,HH,JJ", Item3: 4 }).ToList();
        var temp28 = scores.Where(s => s.Key is { Item2: "BB,CC,DD,EE,HH,JJ", Item3: 3 }).ToList();
        var temp29 = scores.Where(s => s.Key is { Item2: "BB,CC,DD,EE,HH,JJ", Item3: 2 }).ToList();
        var temp30 = scores.Where(s => s.Key is { Item2: "BB,CC,DD,EE,HH,JJ", Item3: 1 }).ToList();
        var temp31 = scores.Where(s => s.Key is { Item2: "BB,CC,DD,EE,HH,JJ", Item3: 0 }).ToList();

        var query = scores.OrderByDescending(s => s.Value.Score).ToList();
        return scores.Max(s => s.Value.Score);
    }

    private static int GetFlowRate(string openedValves)
    {
        return Valves.Where(v => openedValves.Contains(v.Name)).Sum(v => v.FlowRate);
    }

    private static List<Valve> GetValves()
    {
        var valves = CreateValves();
        AssignNeighbors(valves);
        // PruneNeighbors(valves);

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
                var c = valves.First(v => v.Name == neighbor);
                valve.Neighbors.Add(c);
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
