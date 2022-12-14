using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace aoc_2022_csharp.Day13;

public static class Day13
{
    public static int Part1() => File.ReadAllText("Day13/day13.txt")
        .Split($"{Environment.NewLine}{Environment.NewLine}")
        .Select(group => group.Split(Environment.NewLine).Select(JsonConvert.DeserializeObject).ToArray())
        .Select((pair, index) => new { Pair = (Left: pair[0], Right: pair[1]), Index = index })
        .Where(x => AreInCorrectOrder(x.Pair.Left, x.Pair.Right))
        .Sum(x => x.Index + 1);

    public static int Part2()
    {
        var strings = File.ReadAllText("Day13/day13.txt")
            .Replace($"{Environment.NewLine}{Environment.NewLine}", $"{Environment.NewLine}")
            .Split(Environment.NewLine)
            .ToList();

        strings.Add("[[2]]");
        strings.Add("[[6]]");

        var packets = strings.Select(JsonConvert.DeserializeObject).ToList();
        
        packets.Sort(Compare);

        var index1 = packets.Select((x, i) => new { Item = x, Index = i })
            .Single(x => JsonConvert.SerializeObject(x.Item) == "[[2]]")
            .Index + 1;

        var index2 = packets.Select((x, i) => new { Item = x, Index = i })
            .Single(x => JsonConvert.SerializeObject(x.Item) == "[[6]]")
            .Index + 1;

        return index1 * index2;
    }
    
    public static bool AreInCorrectOrder(object left, object right) => Compare(left, right) <= 0;

    public static int Compare(object left, object right)
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
            }
            else if (l[i] is JArray leftArray && r[i] is JArray rightArray)
            {
                var temp = Compare(leftArray, rightArray);
               
                if (temp > 0)
                {
                    return 1;
                }

                if (temp < 0)
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

                if (temp < 0)
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

                if (temp < 0)
                {
                    return -1;
                }
            }

            if (i == l.Count - 1 && r.Count == l.Count)
            {
                // If the lists are the same length and no comparison makes a decision about the order,
                // continue checking the next part of the input.
                return 0;
            }

            if (i == l.Count - 1 && r.Count > l.Count)
            {
                // If the left list runs out of items first, the inputs are in the right order.
                return -1;
            }
        }

        return 0;
    }
}
