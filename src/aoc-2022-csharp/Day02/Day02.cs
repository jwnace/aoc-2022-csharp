namespace aoc_2022_csharp.Day02;

public static class Day02
{
    private static readonly string[] Input = File.ReadAllLines("Day02/day02.txt");

    public static int Part1()
    {
        var score = 0;

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var left = values[0];
            var right = values[1];

            if (right == "X")
            {
                if (left == "A")
                {
                    score += 1 + 3;
                }
                else if (left == "B")
                {
                    score += 1 + 0;
                }
                else if (left == "C")
                {
                    score += 1 + 6;
                }
            }
            else if (right == "Y")
            {
                if (left == "A")
                {
                    score += 2 + 6;
                }
                else if (left == "B")
                {
                    score += 2 + 3;
                }
                else if (left == "C")
                {
                    score += 2 + 0;
                }
            }
            else if (right == "Z")
            {
                if (left == "A")
                {
                    score += 3 + 0;
                }
                else if (left == "B")
                {
                    score += 3 + 6;
                }
                else if (left == "C")
                {
                    score += 3 + 3;
                }
            }
        }

        return score;
    }

    public static int Part2()
    {
        var score = 0;

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var left = values[0];
            var right = values[1];

            if (left == "A")
            {
                if (right == "X")
                {
                    score += 3 + 0;
                }
                else if (right == "Y")
                {
                    score += 1 + 3;
                }
                else if (right == "Z")
                {
                    score += 2 + 6;
                }
            }
            else if (left == "B")
            {
                if (right == "X")
                {
                    score += 1 + 0;
                }
                else if (right == "Y")
                {
                    score += 2 + 3;
                }
                else if (right == "Z")
                {
                    score += 3 + 6;
                }
            }
            else if (left == "C")
            {
                if (right == "X")
                {
                    score += 2 + 0;
                }
                else if (right == "Y")
                {
                    score += 3 + 3;
                }
                else if (right == "Z")
                {
                    score += 1 + 6;
                }
            }
        }

        return score;
    }
}
