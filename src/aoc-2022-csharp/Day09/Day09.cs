namespace aoc_2022_csharp.Day09;

public static class Day09
{
    private static readonly string[] Input = File.ReadAllLines("Day09/day09.txt");

    public static int Part1() => Solve(2);

    public static int Part2() => Solve(10);

    private static int Solve(int ropeLength)
    {
        var rope = new (int X, int Y)[ropeLength];
        Array.Fill(rope, (0, 0));
        var tail = ropeLength - 1;
        var visited = new HashSet<(int X, int Y)> { rope[tail] };

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var direction = values[0];
            var steps = int.Parse(values[1]);

            for (var i = 0; i < steps; i++)
            {
                if (direction == "R")
                {
                    rope[0].X++;
                }
                else if (direction == "L")
                {
                    rope[0].X--;
                }
                else if (direction == "U")
                {
                    rope[0].Y++;
                }
                else if (direction == "D")
                {
                    rope[0].Y--;
                }
                else
                {
                    throw new InvalidOperationException();
                }

                for(int j = 1; j < rope.Length; j++)
                {
                    if (!AreAdjacent(rope[j - 1], rope[j]))
                    {
                        if (rope[j - 1].Y == rope[j].Y)
                        {
                            rope[j].X += rope[j - 1].X > rope[j].X ? 1 : -1;
                        }
                        else if (rope[j - 1].X == rope[j].X)
                        {
                            rope[j].Y += rope[j - 1].Y > rope[j].Y ? 1 : -1;
                        }
                        else
                        {
                            var dX = rope[j - 1].X - rope[j].X;
                            var dY = rope[j - 1].Y - rope[j].Y;

                            rope[j].X += dX > 0 ? 1 : -1;
                            rope[j].Y += dY > 0 ? 1 : -1;
                        }

                        if (j == tail)
                        {
                            visited.Add(rope[j]);
                        }
                    }
                }
            }
        }

        return visited.Count;
    }

    private static bool AreAdjacent((int X, int Y) first, (int X, int Y) second)
    {
        var adjacentCells = new List<(int X, int Y)>
        {
            (first.X - 1, first.Y - 1),
            (first.X    , first.Y - 1),
            (first.X + 1, first.Y - 1),
            (first.X - 1, first.Y    ),
            (first.X    , first.Y    ),
            (first.X + 1, first.Y    ),
            (first.X - 1, first.Y + 1),
            (first.X    , first.Y + 1),
            (first.X + 1, first.Y + 1),
        };

        return adjacentCells.Contains(second);
    }
}
