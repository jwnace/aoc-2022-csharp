namespace aoc_2022_csharp.Day19;

public static class Day19
{
    private static readonly string[] Input = File.ReadAllLines("Day19/day19.txt");

    private static readonly (
        int OreRobotOreCost,
        int ClayRobotOreCost,
        int ObsidianRobotOreCost,
        int ObsidianRobotClayCost,
        int GeodeRobotOreCost,
        int GeodeRobotObsidianCost)[] Blueprints = GetBlueprints().ToArray();

    public static int Part1()
    {
        var blueprint = Blueprints.First();

        var score = MaxScore(blueprint);
        
        return score;
    }

    public static int Part2() => 2;

    private static int MaxScore((
        int OreRobotOreCost,
        int ClayRobotOreCost,
        int ObsidianRobotOreCost,
        int ObsidianRobotClayCost,
        int GeodeRobotOreCost,
        int GeodeRobotObsidianCost) blueprint)
    {
        var ore = 0;
        var clay = 0;
        var obsidian = 0;
        var geodes = 0;

        var oreRobots = 1;
        var clayRobots = 0;
        var obsidianRobots = 0;
        var geodeRobots = 0;

        var queue = new Queue<string>();

        for (var time = 1; time <= 24; time++)
        {
            if (ore >= blueprint.GeodeRobotOreCost && obsidian >= blueprint.GeodeRobotObsidianCost)
            {
                ore -= blueprint.GeodeRobotOreCost;
                obsidian -= blueprint.GeodeRobotObsidianCost;

                queue.Enqueue("geode");
            }

            if (ore >= blueprint.ObsidianRobotOreCost && clay >= blueprint.ObsidianRobotClayCost)
            {
                if (!(ore + (oreRobots * 2) >= blueprint.GeodeRobotOreCost &&
                      obsidian + (obsidianRobots * 2) >= blueprint.GeodeRobotObsidianCost))
                {
                    ore -= blueprint.ObsidianRobotOreCost;
                    clay -= blueprint.ObsidianRobotClayCost;

                    queue.Enqueue("obsidian");
                }
            }

            if (ore >= blueprint.ClayRobotOreCost)
            {
                if (!(ore + (oreRobots * 2) >= blueprint.ObsidianRobotOreCost &&
                      clay + (clayRobots * 2) >= blueprint.ObsidianRobotClayCost))
                {
                    ore -= blueprint.ClayRobotOreCost;
                    queue.Enqueue("clay");
                }
            }

            if (ore >= blueprint.OreRobotOreCost)
            {
                if (!(ore + (oreRobots * 2) >= blueprint.ClayRobotOreCost))
                {
                    ore -= blueprint.OreRobotOreCost;
                    queue.Enqueue("ore");
                }
            }

            ore += oreRobots;
            clay += clayRobots;
            obsidian += obsidianRobots; 
            geodes += geodeRobots;

            while (queue.Count > 0)
            {
                switch (queue.Dequeue())
                {
                    case "ore":
                        oreRobots++;
                        break;
                    case "clay":
                        clayRobots++;
                        break;
                    case "obsidian":
                        obsidianRobots++;
                        break;
                    case "geode":
                        geodeRobots++;
                        break;
                }
            }

            Console.WriteLine();
        }

        return geodes;
    }

    private static IEnumerable<(
        int OreRobotOreCost, 
        int ClayRobotOreCost, 
        int ObsidianRobotOreCost, 
        int ObsidianRobotClayCost, 
        int GeodeRobotOreCost, 
        int GeodeRobotObsidianCost)> 
        GetBlueprints()
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

            var _1_oreRobot_oreCost = int.Parse(t1[4]);
            var _2_clayRobot_oreCost = int.Parse(t2[4]);
            var _3_obsidianRobot_oreCost = int.Parse(t3[4]);
            var _3_obsidianRobot_clayCost = int.Parse(t3[7]);
            var _4_geodeRobot_oreCost = int.Parse(t4[4]);
            var _4_geodeRobot_obsidianCost = int.Parse(t4[7]);

            var blueprint = (
                _1_oreRobot_oreCost,
                _2_clayRobot_oreCost,
                _3_obsidianRobot_oreCost,
                _3_obsidianRobot_clayCost,
                _4_geodeRobot_oreCost,
                _4_geodeRobot_obsidianCost);

            yield return blueprint;
        }
    }
}
