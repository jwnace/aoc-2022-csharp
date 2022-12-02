namespace aoc_2022_csharp.Day02;

public static class Day02
{
    private static readonly IEnumerable<(string Left, string Right)> Input = File.ReadAllLines("Day02/day02.txt")
        .Select(x => x.Split(' '))
        .Select(x => (x[0], x[1]));

    public static int Part1() =>
        Input.Sum(round => round.Left switch
        {
            "A" when round.Right == "X" => 1 + 3,
            "B" when round.Right == "X" => 1 + 0,
            "C" when round.Right == "X" => 1 + 6,
            "A" when round.Right == "Y" => 2 + 6,
            "B" when round.Right == "Y" => 2 + 3,
            "C" when round.Right == "Y" => 2 + 0,
            "A" when round.Right == "Z" => 3 + 0,
            "B" when round.Right == "Z" => 3 + 6,
            "C" when round.Right == "Z" => 3 + 3,
            _ => throw new ArgumentOutOfRangeException(),
        });

    public static int Part2() =>
        Input.Sum(line => line.Left switch
        {
            "A" when line.Right == "X" => 3 + 0,
            "B" when line.Right == "X" => 1 + 0,
            "C" when line.Right == "X" => 2 + 0,
            "A" when line.Right == "Y" => 1 + 3,
            "B" when line.Right == "Y" => 2 + 3,
            "C" when line.Right == "Y" => 3 + 3,
            "A" when line.Right == "Z" => 2 + 6,
            "B" when line.Right == "Z" => 3 + 6,
            "C" when line.Right == "Z" => 1 + 6,
            _ => throw new ArgumentOutOfRangeException(),
        });
}
