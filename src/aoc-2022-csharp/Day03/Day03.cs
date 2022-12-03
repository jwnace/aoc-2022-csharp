namespace aoc_2022_csharp.Day03;

public static class Day03
{
    private static readonly string[] Input = File.ReadAllLines("Day03/day03.txt");

    public static int Part1() => Input
        .Select(line => (Left: line[..(line.Length / 2)], Right: line[(line.Length / 2)..]))
        .Select(group => group.Left.Intersect(group.Right).Single())
        .Select(GetPriority)
        .Sum();

    public static int Part2() => Input
        .Chunk(3)
        .Select(group => group[0].Intersect(group[1]).Intersect(group[2]).Single())
        .Select(GetPriority)
        .Sum();

    private static int GetPriority(char letter) => IsLowerCase(letter) ? letter - 'a' + 1 : letter - 'A' + 27;

    private static bool IsLowerCase(char letter) => letter is >= 'a' and <= 'z';
}
