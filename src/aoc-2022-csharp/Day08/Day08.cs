namespace aoc_2022_csharp.Day08;

public static class Day08
{
    private static readonly int[][] Trees = File.ReadAllLines("Day08/day08.txt")
        .Select(x => x.Select(y => int.Parse(y.ToString())).ToArray()).ToArray();

    public static int Part1()
    {
        var count = 0;

        for (var row = 1; row < Trees.Length - 1; row++)
        for (var col = 1; col < Trees[0].Length - 1; col++)
        {
            var currentTree = Trees[row][col];

            var leftTrees = Trees[row][..col];
            var visibleFromLeft = !leftTrees.Any(x => x >= currentTree);

            var rightTrees = Trees[row][(col + 1)..];
            var visibleFromRight = !rightTrees.Any(x => x >= currentTree);

            var topTrees = Trees[..row].Select(x => x[col]);
            var visibleFromTop = !topTrees.Any(x => x >= currentTree);

            var bottomTrees = Trees[(row + 1)..].Select(x => x[col]);
            var visibleFromBottom = !bottomTrees.Any(x => x >= currentTree);

            if (visibleFromLeft || visibleFromRight || visibleFromTop || visibleFromBottom)
            {
                count++;
            }
        }

        return count + Trees.Length * 2 + Trees[0].Length * 2 - 4;
    }

    public static int Part2()
    {
        var dict = new Dictionary<(int Row, int Col), int>();

        for (var row = 0; row < Trees.Length; row++)
        for (var col = 0; col < Trees[0].Length; col++)
        {
            var currentTree = Trees[row][col];
            var leftCount = 0;
            var rightCount = 0;
            var topCount = 0;
            var bottomCount = 0;

            for (var i = col - 1; i >= 0; i--)
            {
                leftCount++;

                if (Trees[row][i] >= currentTree)
                {
                    break;
                }
            }

            for (var i = col + 1; i < Trees[0].Length; i++)
            {
                rightCount++;

                if (Trees[row][i] >= currentTree)
                {
                    break;
                }
            }

            for (var i = row - 1; i >= 0; i--)
            {
                topCount++;

                if (Trees[i][col] >= currentTree)
                {
                    break;
                }
            }

            for (var i = row + 1; i < Trees.Length; i++)
            {
                bottomCount++;

                if (Trees[i][col] >= currentTree)
                {
                    break;
                }
            }

            dict[(row, col)] = leftCount * rightCount * topCount * bottomCount;
        }

        return dict.Max(x => x.Value);
    }
}
