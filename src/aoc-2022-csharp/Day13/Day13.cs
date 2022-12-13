using System.Diagnostics.SymbolStore;
using System.Reflection.Metadata.Ecma335;
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
        var dict = new Dictionary<int, bool>();

        for (var i = 0; i < Pairs.Length; i++)
        {
            var pair = Pairs[i];

            var left = JsonConvert.DeserializeObject(pair.Left);
            var right = JsonConvert.DeserializeObject(pair.Right);

            var areInCorrectOrder = AreInCorrectOrder(left, right);

            dict[i + 1] = areInCorrectOrder;
        }

        return dict.Where(x => x.Value).Sum(x => x.Key);
    }

    public static bool AreInCorrectOrder(object left, object right)
    {
        return Compare(left, right) < 0;
    }

    private static int Compare(object left, object right)
    {
        if (left is not JArray l || right is not JArray r)
        {
            throw new InvalidOperationException();
        }

        if (l.Count == 0 && r.Count > 0)
        {
            return -1;
        }

        for (var i = 0; i < l.Count; i++)
        {
            if (i >= r.Count)
            {
                // If the right list runs out of items first, the inputs are not in the right order.
                return 1;
            }

            if (l[i] is JValue leftValue && r[i] is JValue rightValue)
            {
                var foo = (int)leftValue;
                var bar = (int)rightValue;

                if (foo < bar)
                {
                    // If the left integer is lower than the right integer, the inputs are in the right order.
                    return -1;
                }

                if (foo > bar)
                {
                    // If the left integer is higher than the right integer, the inputs are not in the right order.
                    return 1;
                }

                if (i == l.Count - 1 && r.Count == l.Count)
                {
                    // If the lists are the same length and no comparison makes a decision about the order,
                    // continue checking the next part of the input.
                    return 0;
                }

                // TODO: determine if this should REALLY be `>=` or just `>`
                if (i == l.Count - 1 && r.Count > l.Count)
                {
                    // If the left list runs out of items first, the inputs are in the right order.
                    return -1;
                }
            }
            else if (l[i] is JArray leftArray && r[i] is JArray rightArray)
            {
                var temp = Compare(leftArray, rightArray);
                if (temp > 0)
                {
                    return 1;
                }
                else if (temp < 0)
                {
                    return -1;
                }
            }
            else if (l[i] is JArray leftArray1 && r[i] is JValue rightValue1)
            {
                // If exactly one value is an integer, convert the integer to a list which contains that integer as its
                // only value, then retry the comparison.
                var temp = Compare(leftArray1, new JArray(rightValue1));
                if (temp > 0)
                {
                    return 1;
                }
                else if (temp < 0)
                {
                    return -1;
                }
            }
            else if (l[i] is JValue leftValue2 && r[i] is JArray rightArray2)
            {
                // If exactly one value is an integer, convert the integer to a list which contains that integer as its
                // only value, then retry the comparison.
                var temp = Compare(new JArray(leftValue2), rightArray2);
                if (temp > 0)
                {
                    return 1;
                }
                else if (temp < 0)
                {
                    return -1;
                }
            }
        }

        return 0;
    }

    public static int Part2()
    {
        var strings = File.ReadAllText("Day13/day13.txt")
            .Replace($"{Environment.NewLine}{Environment.NewLine}", $"{Environment.NewLine}")
            .Split(Environment.NewLine)
            .ToList();

        strings.Add("[[2]]");
        strings.Add("[[6]]");

        var foo = JsonConvert.DeserializeObject("[[2]]");
        var bar = JsonConvert.DeserializeObject("[[6]]");

        var packets = strings.Select(JsonConvert.DeserializeObject).ToList();
        packets.Sort(Compare);

        var serializedFoo = JsonConvert.SerializeObject(foo);
        var serializedBar = JsonConvert.SerializeObject(bar);

        var indexOfFoo = packets.Select((x, i) => new { Item = x, Index = i })
            .Single(x => JsonConvert.SerializeObject(x.Item) == "[[2]]")
            .Index + 1;

        var indexOfBar = packets.Select((x, i) => new { Item = x, Index = i })
            .Single(x => JsonConvert.SerializeObject(x.Item) == "[[6]]")
            .Index;

        return indexOfFoo * indexOfBar;
    }
}
