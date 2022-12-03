namespace aoc_2022_csharp.Day03;

public static class Day03
{
    private static readonly string[] Input = File.ReadAllLines("Day03/day03.txt");

    public static int Part1()
    {
        var total = 0;

        foreach (var line in Input)
        {
            var length = line.Length / 2;
            var left = line[..length];
            var right = line[length..];

            var commonLetter = left.Intersect(right).Single();

            if (commonLetter is >= 'a' and <= 'z')
            {
                total += commonLetter - 'a' + 1;
            }
            else
            {
                total += commonLetter - 'A' + 27;
            }
        }

        return total;
    }

    public static int Part2()
    {
        var total = 0;
        var groups = Input.Chunk(3);

        foreach (var group in groups)
        {
            var (a, b, c) = (group[0], group[1], group[2]);

            var commonLetter = a.Intersect(b).Intersect(c).Single();

            if (commonLetter is >= 'a' and <= 'z')
            {
                total += commonLetter - 'a' + 1;
            }
            else
            {
                total += commonLetter - 'A' + 27;
            }
        }

        return total;
    }
}
