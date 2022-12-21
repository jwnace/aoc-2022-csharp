using System.Diagnostics;

namespace aoc_2022_csharp.Day19;

public static class Day19
{
    private static readonly string[] Input = File.ReadAllLines("Day19/day19.txt");
    private static readonly Blueprint[] Blueprints = GetBlueprints().ToArray();

    public static int Part1()
    {
        var maxScore = 0;
        var totalQuality = 0;
        var stopwatch = new Stopwatch();

        for (var i = 0; i < Blueprints.Length; i++)
        {
            Memo = new();

            // Console.WriteLine($"Testing Blueprint {i + 1}");
            // stopwatch.Start();

            var score = MaxScore(Blueprints[i], 24, 0, 0, 0, 0, 1, 0, 0, 0);
            var qualityLevel = (i + 1) * score;

            // stopwatch.Stop();
            // Console.WriteLine(stopwatch.Elapsed);
            // Console.WriteLine($"Score for Blueprint {i + 1}: {score} -> Quality Level: {qualityLevel}");

            maxScore = Math.Max(maxScore, score);
            totalQuality += qualityLevel;
        }

        return totalQuality;
    }

    public static long Part2()
    {
        var scores = new List<long>();
        var stopwatch = new Stopwatch();

        for (var i = 0; i < 3; i++)
        {
            Memo = new();

            Console.WriteLine($"Testing Blueprint {i + 1}");
            stopwatch.Start();

            var score = (long)MaxScore(Blueprints[i], 32, 0, 0, 0, 0, 1, 0, 0, 0);
            scores.Add(score);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Console.WriteLine($"Score for Blueprint {i + 1}: {score}");
        }

        var temp = scores.ToArray();

        return temp[0] * temp[1] * temp[2];
    }

    private static Dictionary<(Blueprint, int, int, int, int, int, int, int, int, int), int> Memo = new();

    private static int MaxScore(Blueprint blueprint, int minutesLeft, int ore, int clay, int obsidian, int geodes,
        int oreRobots, int clayRobots, int obsidianRobots, int geodeRobots)
    {
        var key = (blueprint, minutesLeft, ore, clay, obsidian, geodes, oreRobots, clayRobots, obsidianRobots,
            geodeRobots);

        if (Memo.TryGetValue(key, out var value))
        {
            return value;
        }

        if (minutesLeft <= 0)
        {
            return 0;
        }

        var maxOre = new[]
        {
            blueprint.OreRobotOreCost, blueprint.ClayRobotOreCost,
            blueprint.ObsidianRobotOreCost, blueprint.GeodeRobotOreCost
        }.Max();

        var maxClay = blueprint.ObsidianRobotClayCost;

        var maxObsidian = blueprint.GeodeRobotObsidianCost;

        var best = geodes + geodeRobots;

        // if o >= t*Core-r1*(t-1):
        // o = t*Core-r1*(t-1)
        // if c>=t*Co2-r2*(t-1):
        // c = t*Co2 - r2*(t-1)
        // if ob>=t*Cg2-r3*(t-1):
        // ob = t*Cg2-r3*(t-1)

        if (ore >= minutesLeft * maxOre - oreRobots * (minutesLeft - 1))
        {
            ore = minutesLeft * maxOre - oreRobots * (minutesLeft - 1);
        }

        if (clay >= minutesLeft * maxClay - clayRobots * (minutesLeft - 1))
        {
            clay = minutesLeft * maxClay - clayRobots * (minutesLeft - 1);
        }

        if (obsidian >= minutesLeft * maxObsidian - obsidianRobots * (minutesLeft - 1))
        {
            obsidian = minutesLeft * maxObsidian - obsidianRobots * (minutesLeft - 1);
        }

        best = Math.Max(best, MaxScore(
            blueprint,
            minutesLeft - 1,
            ore + oreRobots,
            clay + clayRobots,
            obsidian + obsidianRobots,
            geodes + geodeRobots,
            oreRobots,
            clayRobots,
            obsidianRobots,
            geodeRobots));

        if (CanBuildGeodeRobot(blueprint, ore, obsidian))
        {
            best = Math.Max(best, MaxScore(
                blueprint,
                minutesLeft - 1,
                ore + oreRobots - blueprint.GeodeRobotOreCost,
                clay + clayRobots,
                obsidian + obsidianRobots - blueprint.GeodeRobotObsidianCost,
                geodes + geodeRobots,
                oreRobots,
                clayRobots,
                obsidianRobots,
                geodeRobots + 1));
        }

        if (obsidianRobots < maxObsidian && CanBuildObsidianRobot(blueprint, ore, clay))
        {
            best = Math.Max(best, MaxScore(
                blueprint,
                minutesLeft - 1,
                ore + oreRobots - blueprint.ObsidianRobotOreCost,
                clay + clayRobots - blueprint.ObsidianRobotClayCost,
                obsidian + obsidianRobots,
                geodes + geodeRobots,
                oreRobots,
                clayRobots,
                obsidianRobots + 1,
                geodeRobots));
        }

        if (clayRobots < maxClay && CanBuildClayRobot(blueprint, ore))
        {
            best = Math.Max(best, MaxScore(
                blueprint,
                minutesLeft - 1,
                ore + oreRobots - blueprint.ClayRobotOreCost,
                clay + clayRobots,
                obsidian + obsidianRobots,
                geodes + geodeRobots,
                oreRobots,
                clayRobots + 1,
                obsidianRobots,
                geodeRobots));
        }

        if (oreRobots < maxOre && CanBuildOreRobot(blueprint, ore))
        {
            best = Math.Max(best, MaxScore(
                blueprint,
                minutesLeft - 1,
                ore + oreRobots - blueprint.OreRobotOreCost,
                clay + clayRobots,
                obsidian + obsidianRobots,
                geodes + geodeRobots,
                oreRobots + 1,
                clayRobots,
                obsidianRobots,
                geodeRobots));
        }

        Memo[key] = best;
        return best;
    }

    private static bool CanBuildOreRobot(Blueprint blueprint, int ore) =>
        blueprint.OreRobotOreCost <= ore;

    private static bool CanBuildClayRobot(Blueprint blueprint, int ore) =>
        blueprint.ClayRobotOreCost <= ore;

    private static bool CanBuildObsidianRobot(Blueprint blueprint, int ore, int clay) =>
        blueprint.ObsidianRobotOreCost <= ore && blueprint.ObsidianRobotClayCost <= clay;

    private static bool CanBuildGeodeRobot(Blueprint blueprint, int ore, int obsidian) =>
        blueprint.GeodeRobotOreCost <= ore && blueprint.GeodeRobotObsidianCost <= obsidian;

    private record Blueprint(int OreRobotOreCost, int ClayRobotOreCost, int ObsidianRobotOreCost,
        int ObsidianRobotClayCost, int GeodeRobotOreCost, int GeodeRobotObsidianCost);

    private static IEnumerable<Blueprint> GetBlueprints()
    {
        foreach (var line in Input)
        {
            var i1 = line.IndexOf("Each ore robot costs", StringComparison.Ordinal);
            var i2 = line.IndexOf("Each clay robot costs", StringComparison.Ordinal);
            var i3 = line.IndexOf("Each obsidian robot costs", StringComparison.Ordinal);
            var i4 = line.IndexOf("Each geode robot costs", StringComparison.Ordinal);

            var s1 = line.Substring(i1, line.IndexOf('.', i1) - i1 + 1);
            var s2 = line.Substring(i2, line.IndexOf('.', i2) - i2 + 1);
            var s3 = line.Substring(i3, line.IndexOf('.', i3) - i3 + 1);
            var s4 = line.Substring(i4, line.IndexOf('.', i4) - i4 + 1);

            var t1 = s1.Split(null);
            var t2 = s2.Split(null);
            var t3 = s3.Split(null);
            var t4 = s4.Split(null);

            var oreRobotOreCost = int.Parse(t1[4]);
            var clayRobotOreCost = int.Parse(t2[4]);
            var obsidianRobotOreCost = int.Parse(t3[4]);
            var obsidianRobotClayCost = int.Parse(t3[7]);
            var geodeRobotOreCost = int.Parse(t4[4]);
            var geodeRobotObsidianCost = int.Parse(t4[7]);

            yield return new Blueprint(oreRobotOreCost, clayRobotOreCost, obsidianRobotOreCost, obsidianRobotClayCost,
                geodeRobotOreCost, geodeRobotObsidianCost);
        }
    }
}
