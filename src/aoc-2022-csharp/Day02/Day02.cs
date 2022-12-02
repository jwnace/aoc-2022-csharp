namespace aoc_2022_csharp.Day02;

public static class Day02
{
    private static readonly string[] Input = File.ReadAllLines("Day02/day02.txt");

    public static int Part1()
    {
        var totalScore = 0;

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var left = values[0];
            var right = values[1];

            var score = left switch
            {
                "A" when right == "X" => 1 + 3,
                "B" when right == "X" => 1 + 0,
                "C" when right == "X" => 1 + 6,
                "A" when right == "Y" => 2 + 6,
                "B" when right == "Y" => 2 + 3,
                "C" when right == "Y" => 2 + 0,
                "A" when right == "Z" => 3 + 0,
                "B" when right == "Z" => 3 + 6,
                "C" when right == "Z" => 3 + 3,
                _ => throw new ArgumentOutOfRangeException(),
            };

            totalScore += score;
        }

        return totalScore;
    }

    public static int Part2()
    {
        var totalScore = 0;

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var left = values[0];
            var right = values[1];

            var score = left switch
            {
                "A" when right == "X" => 3 + 0,
                "B" when right == "X" => 1 + 0,
                "C" when right == "X" => 2 + 0,
                "A" when right == "Y" => 1 + 3,
                "B" when right == "Y" => 2 + 3,
                "C" when right == "Y" => 3 + 3,
                "A" when right == "Z" => 2 + 6,
                "B" when right == "Z" => 3 + 6,
                "C" when right == "Z" => 1 + 6,
                _ => throw new ArgumentOutOfRangeException(),
            };

            totalScore += score;
        }

        return totalScore;
    }
}
