namespace aoc_2022_csharp.Day04;

public static class Day04
{
    private static readonly string[] Input = File.ReadAllLines("Day04/day04.txt");

    public static int Part1()
    {
        var count = 0;

        foreach (var line in Input)
        {
            var values = line.Split(',');
            var left = values[0];
            var right = values[1];

            var foo = left.Split('-');
            var a = int.Parse(foo[0]);
            var b = int.Parse(foo[1]);

            var bar = right.Split('-');
            var c = int.Parse(bar[0]);
            var d = int.Parse(bar[1]);

            if ((a <= c && b >= d) || (c <= a && d >= b))
            {
                count++;
            }
        }

        return count;
    }

    public static int Part2()
    {
        var count = 0;

        foreach (var line in Input)
        {
            var values = line.Split(',');
            var left = values[0];
            var right = values[1];

            var foo = left.Split('-');
            var a = int.Parse(foo[0]);
            var b = int.Parse(foo[1]);

            var bar = right.Split('-');
            var c = int.Parse(bar[0]);
            var d = int.Parse(bar[1]);

            if ((a <= c && b >= c) || (c <= a && d >= a))
            {
                count++;
            }
        }

        return count;
    }
}
