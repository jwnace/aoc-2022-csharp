namespace aoc_2022_csharp.Day11;

public static class Day11
{
    private static readonly string[] Input = File.ReadAllText("Day11/day11.txt")
        .Split($"{Environment.NewLine}{Environment.NewLine}");

    private class Monkey
    {
        public string Name { get; set; }
        public long Number { get; set; }
        public List<long> Items { get; set; }
        public string Operation { get; set; }
        public string Test { get; set; }
        public string IfTrue { get; set; }
        public string IfFalse { get; set; }
    }

    public static long Part1()
    {
        var monkeys = GetMonkeys();

        var dict = new Dictionary<long, long>
            { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 }, { 5, 0 }, { 6, 0 }, { 7, 0 }, };

        for (var i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Count > 0)
                {
                    dict[monkey.Number]++;

                    var modifier = long.TryParse(monkey.Operation.Split(' ')[^1], out var value)
                        ? value
                        : monkey.Items[0];

                    if (monkey.Operation.Contains("+"))
                    {
                        monkey.Items[0] += modifier;
                    }
                    else if (monkey.Operation.Contains("*"))
                    {
                        monkey.Items[0] *= modifier;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }

                    monkey.Items[0] /= 3;

                    var divisor = long.Parse(monkey.Test.Split(' ')[^1]);

                    if (monkey.Items[0] % divisor == 0)
                    {
                        var number = long.Parse(monkey.IfTrue.Split(' ')[^1]);
                        monkeys.Single(m => m.Number == number).Items.Add(monkey.Items[0]);
                        monkey.Items.RemoveAt(0);
                    }
                    else
                    {
                        var number = long.Parse(monkey.IfFalse.Split(' ')[^1]);
                        monkeys.Single(m => m.Number == number).Items.Add(monkey.Items[0]);
                        monkey.Items.RemoveAt(0);
                    }
                }
            }
        }

        var topTwo = dict.OrderByDescending(x => x.Value).Select(x => x.Value).Take(2).ToArray();

        return topTwo[0] * topTwo[1];
    }

    private static List<Monkey> GetMonkeys()
    {
        var monkeys = new List<Monkey>();

        foreach (var line in Input)
        {
            var values = line.Split(Environment.NewLine);
            var name = values[0][..^1];
            var number = long.Parse(values[0][^2].ToString());
            var startingItems = values[1].Split(": ")[1].Split(", ").Select(long.Parse).ToList();
            var operation = values[2].Split("= ")[1];
            var test = values[3].Split(": ")[1];
            var ifTrue = values[4].Split(": ")[1];
            var ifFalse = values[5].Split(": ")[1];

            monkeys.Add(new Monkey
            {
                Name = name,
                Number = number,
                Items = startingItems,
                Operation = operation,
                Test = test,
                IfTrue = ifTrue,
                IfFalse = ifFalse
            });
        }

        return monkeys;
    }

    public static long Part2() => 2;
}
