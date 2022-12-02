namespace aoc_2022_csharp.Day01;

public static class Day01
{
    private static readonly IEnumerable<int> Elves = File.ReadAllText("Day01/day01.txt")
        .Split($"{Environment.NewLine}{Environment.NewLine}")
        .Select(elf => elf.Split(Environment.NewLine).Select(int.Parse))
        .Select(calories => calories.Sum());

    public static int Part1() => Elves.Max();

    public static int Part2() => Elves.OrderByDescending(x => x).Take(3).Sum();
}
