using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace aoc_2022_csharp.Day13;

public static class Day13
{
    private static readonly (string Left, string Right)[] Pairs = File.ReadAllText("Day13/day13.txt")
        .Split($"{Environment.NewLine}{Environment.NewLine}")
        .Select(p => p.Split(Environment.NewLine))
        .Select(x => (x[0], x[1]))
        .ToArray();

    public static int Part1()
    {
        foreach (var pair in Pairs)
        {
            var left = JsonConvert.DeserializeObject(pair.Left);
            var right = JsonConvert.DeserializeObject(pair.Right);

            var areInCorrectOrder = AreInCorrectOrder(left, right);
        }

        return 1;
    }

    public static bool AreInCorrectOrder(object left, object right)
    {
        if (left is JArray l && right is JArray r)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i] is JValue leftValue && r[i] is JValue rightValue)
                {
                    var foo = (int)leftValue;
                    var bar = (int)rightValue;

                    if (foo < bar)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static int Part2() => 2;
}
