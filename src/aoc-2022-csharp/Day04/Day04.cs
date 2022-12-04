namespace aoc_2022_csharp.Day04;

public static class Day04
{
    private static readonly string[] Input = File.ReadAllLines("Day04/day04.txt");

    public static int Part1() => Input
        .Select(line => new { Left = line.Split(',')[0].Split('-'), Right = line.Split(',')[1].Split('-') })
        .Select(ranges => new
        {
            A = int.Parse(ranges.Left[0]),
            B = int.Parse(ranges.Left[1]),
            C = int.Parse(ranges.Right[0]),
            D = int.Parse(ranges.Right[1])
        })
        .Count(x => (x.A <= x.C && x.B >= x.D) || (x.C <= x.A && x.D >= x.B));

    public static int Part2() => Input
        .Select(line => new { Left = line.Split(',')[0].Split('-'), Right = line.Split(',')[1].Split('-') })
        .Select(ranges => new
        {
            A = int.Parse(ranges.Left[0]),
            B = int.Parse(ranges.Left[1]),
            C = int.Parse(ranges.Right[0]),
            D = int.Parse(ranges.Right[1])
        })
        .Count(x => (x.A <= x.C && x.B >= x.C) || (x.C <= x.A && x.D >= x.A));
}
