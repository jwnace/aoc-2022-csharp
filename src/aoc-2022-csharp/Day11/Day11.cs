namespace aoc_2022_csharp.Day11;

public static class Day11
{
    private static readonly string[] Input = File.ReadAllText("Day11/day11.txt")
        .Split($"{Environment.NewLine}{Environment.NewLine}");

    public static long Part1() => Solve(1);

    public static long Part2() => Solve(2);

    private static long Solve(int part)
    {
        var rounds = part == 1 ? 20 : 10_000;
        var monkeys = GetMonkeys();
        var dict = monkeys.ToDictionary(monkey => monkey.Number, _ => 0L);

        for (var i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    dict[monkey.Number]++;

                    var item = DoOperation(monkey, monkey.Items[0]);

                    if (part == 1)
                    {
                        item /= 3;
                    }
                    else
                    {
                        item %= monkeys.GetLeastCommonMultiple();
                    }

                    var target = item % monkey.Divisor == 0 ? monkey.TrueTarget : monkey.FalseTarget;
                    monkeys.Single(m => m.Number == target).Items.Add(item);
                    monkey.Items.RemoveAt(0);
                }
            }
        }

        var topTwo = dict.OrderByDescending(x => x.Value).Select(x => x.Value).Take(2).ToArray();

        return topTwo[0] * topTwo[1];
    }

    private static long DoOperation(Monkey monkey, long item)
    {
        var temp = long.TryParse(monkey.Operation.Split(' ')[^1], out var value) ? value : item;

        if (monkey.Operation.Contains("+"))
        {
            item += temp;
        }
        else if (monkey.Operation.Contains("*"))
        {
            item *= temp;
        }

        return item;
    }

    private static List<Monkey> GetMonkeys()
    {
        var monkeys = new List<Monkey>();

        foreach (var group in Input)
        {
            var values = group.Split(Environment.NewLine);
            var number = int.Parse(values[0][^2].ToString());
            var startingItems = values[1].Split(": ")[1].Split(", ").Select(long.Parse).ToList();
            var operation = values[2].Split("= ")[1];
            var divisor = int.Parse(values[3].Split(' ')[^1]);
            var trueTarget = int.Parse(values[4].Split(' ')[^1]);
            var falseTarget = int.Parse(values[5].Split(' ')[^1]);

            monkeys.Add(new Monkey
            {
                Number = number,
                Items = startingItems,
                Operation = operation,
                Divisor = divisor,
                TrueTarget = trueTarget,
                FalseTarget = falseTarget,
            });
        }

        return monkeys;
    }

    private static long GetLeastCommonMultiple(this IEnumerable<Monkey> monkeys) =>
        monkeys.Aggregate(1, (accum, monkey) => accum * monkey.Divisor);

    private class Monkey
    {
        public int Number { get; init; }
        public List<long> Items { get; init; } = new();
        public string Operation { get; init; } = "";
        public int Divisor { get; init; }
        public int TrueTarget { get; init; }
        public int FalseTarget { get; init; }
    }
}
