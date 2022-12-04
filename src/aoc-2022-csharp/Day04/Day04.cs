namespace aoc_2022_csharp.Day04;

public static class Day04
{
    private static readonly IEnumerable<(Range Left, Range Right)> Pairs = File.ReadAllLines("Day04/day04.txt")
        .Select(line => line.Split(','))
        .Select(ranges => (Range.FromString(ranges[0]), Range.FromString(ranges[1])));

    public static int Part1() => Pairs
        .Count(ranges => ranges.Left.Contains(ranges.Right) || ranges.Right.Contains(ranges.Left));

    public static int Part2() => Pairs.Count(ranges => ranges.Left.Overlaps(ranges.Right));

    private record Range(int Start, int End)
    {
        public static Range FromString(string input) =>
            new(int.Parse(input.Split('-')[0]), int.Parse(input.Split('-')[1]));

        public bool Contains(Range other) => Start <= other.Start && End >= other.End;

        public bool Overlaps(Range other) =>
            (Start <= other.Start && End >= other.Start) || (other.Start <= Start && other.End >= Start);
    }
}
