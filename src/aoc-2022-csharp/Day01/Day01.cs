namespace aoc_2022_csharp.Day01;

public static class Day01
{
    private static readonly string Input = File.ReadAllText("Day01/day01.txt");

    public static int Part1()
    {
        var query = Input.Split("\r\n\r\n");

        var sums = new List<int>();

        foreach (var group in query)
        {
            var foo = group.Split("\r\n").Select(int.Parse);

            sums.Add(foo.Sum());
        }

        return sums.Max();
    }

    public static int Part2()
    {
        var query = Input.Split("\r\n\r\n");

        var sums = new List<int>();

        foreach (var group in query)
        {
            var foo = group.Split("\r\n").Select(int.Parse);

            sums.Add(foo.Sum());
        }

        return sums.OrderByDescending(x => x).Take(3).Sum();
    }
}
