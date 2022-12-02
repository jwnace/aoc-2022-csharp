namespace aoc_2022_csharp.Day02;

public static class Day02
{
    private static readonly IEnumerable<(string, string)> Input = File.ReadAllLines("Day02/day02.txt")
        .Select(x => x.Split(' '))
        .Select(x => (x[0], x[1]));

    public static int Part1() =>
        Input.Sum(round => round switch
        {
            ("A", "X") => 1 + 3,
            ("B", "X") => 1 + 0,
            ("C", "X") => 1 + 6,
            ("A", "Y") => 2 + 6,
            ("B", "Y") => 2 + 3,
            ("C", "Y") => 2 + 0,
            ("A", "Z") => 3 + 0,
            ("B", "Z") => 3 + 6,
            ("C", "Z") => 3 + 3,
            _ => throw new ArgumentOutOfRangeException(),
        });

    public static int Part2() =>
        Input.Sum(line => line switch
        {
            ("A", "X") => 3 + 0,
            ("B", "X") => 1 + 0,
            ("C", "X") => 2 + 0,
            ("A", "Y") => 1 + 3,
            ("B", "Y") => 2 + 3,
            ("C", "Y") => 3 + 3,
            ("A", "Z") => 2 + 6,
            ("B", "Z") => 3 + 6,
            ("C", "Z") => 1 + 6,
            _ => throw new ArgumentOutOfRangeException(),
        });
}
