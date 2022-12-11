namespace aoc_2022_csharp.Day11;

public static class Day11
{
    private static readonly string[] Input = File.ReadAllText("Day11/day11.txt")
        .Split($"{Environment.NewLine}{Environment.NewLine}");

    private class Monkey
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public List<Item> Items { get; set; }
        public string Operation { get; set; }
        public string Test { get; set; }
        public string IfTrue { get; set; }
        public string IfFalse { get; set; }
    }

    private class Item
    {
        public int Worry { get; set; }
    }

    public static int Part1()
    {
        var monkeys = new List<Monkey>();

        foreach (var line in Input)
        {
            var values = line.Split(Environment.NewLine);
            var name = values[0][..^1];
            var number = int.Parse(values[0][^2].ToString());
            var startingItems = values[1].Split(": ")[1].Split(", ").Select(int.Parse);
            var operation = values[2].Split("= ")[1];
            var test = values[3].Split(": ")[1];
            var ifTrue = values[4].Split(": ")[1];
            var ifFalse = values[5].Split(": ")[1];

            monkeys.Add(new Monkey
            {
                Name = name,
                Number = number,
                Items = startingItems.Select(x => new Item { Worry = x }).ToList(),
                Operation = operation,
                Test = test,
                IfTrue = ifTrue,
                IfFalse = ifFalse
            });
        }

        var dict = new Dictionary<int, int>
        {
            { 0, 0 },
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
            { 6, 0 },
            { 7, 0 },
        };

        for (int round = 0; round < 20; round++)
        {
            foreach (var monkey in monkeys)
            {
                // var thrownItems = new List<Item>();

                // for (var i = 0; i < monkey.Items.Count; i++)
                while (monkey.Items.Count > 0)
                {
                    dict[monkey.Number]++;

                    var modifier = int.TryParse(monkey.Operation.Split(' ')[^1], out var value)
                        ? value
                        : monkey.Items[0].Worry;

                    if (monkey.Operation.Contains("+"))
                    {
                        monkey.Items[0].Worry += modifier;
                    }
                    else if (monkey.Operation.Contains("*"))
                    {
                        monkey.Items[0].Worry *= modifier;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }

                    monkey.Items[0].Worry /= 3;

                    var divisor = int.Parse(monkey.Test.Split(' ')[^1]);

                    if (monkey.Items[0].Worry % divisor == 0)
                    {
                        var foo = int.Parse(monkey.IfTrue.Split(' ')[^1]);

                        monkeys.Single(m => m.Number == foo).Items.Add(monkey.Items[0]);
                        monkey.Items.RemoveAt(0);
                    }
                    else
                    {
                        var foo = int.Parse(monkey.IfFalse.Split(' ')[^1]);

                        monkeys.Single(m => m.Number == foo).Items.Add(monkey.Items[0]);
                        monkey.Items.RemoveAt(0);
                    }
                }
            }
        }

        var topTwo = dict.OrderByDescending(x => x.Value).Select(x => x.Value).Take(2).ToArray();

        return topTwo[0] * topTwo[1];
    }

    public static int Part2() => 2;
}
