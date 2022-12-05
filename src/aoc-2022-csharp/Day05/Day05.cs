namespace aoc_2022_csharp.Day05;

public static class Day05
{
    private record Instruction(int Source, int Destination, int Count)
    {
        public static Instruction Parse(string input)
        {
            var values = input.Split(' ');
            var count = int.Parse(values[1]);
            var a = int.Parse(values[3]) - 1;
            var b = int.Parse(values[5]) - 1;

            return new Instruction(a, b, count);
        }
    }

    private static readonly string[] Input =
        File.ReadAllText("Day05/day05.txt")
            .Split($"{Environment.NewLine}{Environment.NewLine}");

    private static readonly string[] StackStrings =
        Input[0].Split(Environment.NewLine)[..^1]
            .Reverse()
            .ToArray();

    private static readonly Instruction[] Instructions =
        Input[1].Split(Environment.NewLine)
            .Select(Instruction.Parse)
            .ToArray();

    private static List<Stack<char>> BuildStacks()
    {
        var stacks = new List<Stack<char>> { new(), new(), new(), new(), new(), new(), new(), new(), new() };

        foreach (var line in StackStrings)
        {
            var (a, b, c, d, e, f, g, h, i) =
                (line[1], line[5], line[9], line[13], line[17], line[21], line[25], line[29], line[33]);

            if (a != ' ') stacks[0].Push(a);
            if (b != ' ') stacks[1].Push(b);
            if (c != ' ') stacks[2].Push(c);
            if (d != ' ') stacks[3].Push(d);
            if (e != ' ') stacks[4].Push(e);
            if (f != ' ') stacks[5].Push(f);
            if (g != ' ') stacks[6].Push(g);
            if (h != ' ') stacks[7].Push(h);
            if (i != ' ') stacks[8].Push(i);
        }

        return stacks;
    }

    public static string Part1()
    {
        var stacks = BuildStacks();

        foreach (var instruction in Instructions)
        {
            var (a, b, count) = instruction;

            for (var i = 0; i < count; i++)
            {
                var item = stacks[a].Pop();
                stacks[b].Push(item);
            }
        }

        return stacks.Aggregate("", (result, stack) => result + (stack.Count > 0 ? stack.Peek() : ' '));
    }

    public static string Part2()
    {
        var stacks = BuildStacks();

        foreach (var instruction in Instructions)
        {
            var (a, b, count) = instruction;
            var items = stacks[a].Take(count).Reverse().ToArray();

            for (var i = 0; i < count; i++)
            {
                stacks[a].Pop();
                stacks[b].Push(items[i]);
            }
        }

        return stacks.Aggregate("", (result, stack) => result + (stack.Count > 0 ? stack.Peek() : ' '));
    }
}
