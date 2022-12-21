namespace aoc_2022_csharp.Day21;

public static class Day21
{
    private static readonly string[] Input = File.ReadAllLines("Day21/day21.txt");

    public static long Part1()
    {
        var monkeys = GetMonkeys();

        PopulateMonkeys(monkeys);

        return monkeys.First(m => m.Name == "root").GetValue();
    }

    public static long Part2()
    {
        var monkeys = GetMonkeys();

        PopulateMonkeys(monkeys);

        var root = monkeys.First(m => m.Name == "root");
        var human = monkeys.First(m => m.Name == "humn");

        human.Number = 3_665_520_865_000;

        while (true)
        {
            var left = root.Left.GetValue();
            var right = root.Right.GetValue();

            if (left == right)
            {
                return human.Number!.Value;
            }

            var diff = Math.Abs(left - right);
            // Console.WriteLine($"humn: {human.Number}, diff: {diff}");

            human.Number++;
        }
    }

    private static void PopulateMonkeys(List<Monkey> monkeys)
    {
        foreach (var line in Input)
        {
            var values = line.Split(null);
            var name = values[0][..^1];
            var monkey = monkeys.First(m => m.Name == name);

            if (values.Length == 2)
            {
                var number = long.Parse(values[1]);
                monkey.Number = number;
            }
            else if (values.Length == 4)
            {
                var a = values[1];
                var operation = values[2];
                var b = values[3];

                monkey.Left = monkeys.First(m => m.Name == a);
                monkey.Right = monkeys.First(m => m.Name == b);
                monkey.Operation = operation;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }

    private static List<Monkey> GetMonkeys()
    {
        var monkeys = new List<Monkey>();

        foreach (var line in Input)
        {
            var values = line.Split(null);
            var name = values[0][..^1];

            monkeys.Add(new Monkey { Name = name });
        }

        return monkeys;
    }

    private class Monkey
    {
        public string Name { get; set; }
        public long? Number { get; set; }
        public string Operation { get; set; }
        public Monkey Left { get; set; }
        public Monkey Right { get; set; }

        public long GetValue()
        {
            if (Number is not null)
            {
                return Number.Value;
            }

            return Operation switch
            {
                "+" => Left.GetValue() + Right.GetValue(),
                "-" => Left.GetValue() - Right.GetValue(),
                "*" => Left.GetValue() * Right.GetValue(),
                "/" => Left.GetValue() / Right.GetValue(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
