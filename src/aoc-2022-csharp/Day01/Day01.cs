namespace aoc_2022_csharp.Day01;

public static class Day01
{
    private static readonly string Input = File.ReadAllText("Day01/day01.txt");

    public static int Part1() => Input.Split($"{Environment.NewLine}{Environment.NewLine}")
        .Select(elf => elf.Split(Environment.NewLine).Select(int.Parse))
        .Max(calories => calories.Sum());

    public static int Part2() => Input.Split($"{Environment.NewLine}{Environment.NewLine}")
        .Select(elf => elf.Split(Environment.NewLine).Select(int.Parse))
        .Select(calories => calories.Sum())
        .OrderByDescending(x => x)
        .Take(3)
        .Sum();
}
