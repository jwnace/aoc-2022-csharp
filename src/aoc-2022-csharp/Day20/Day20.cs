namespace aoc_2022_csharp.Day20;

public static class Day20
{
    private static readonly (long Number, int OriginalIndex)[] Input = File.ReadAllLines("Day20/day20.txt")
        .Select(long.Parse)
        .Select((x, i) => (x, i))
        .ToArray();

    public static long Part1()
    {
        var nodes = BuildLinkedList(1);

        Mix(nodes, 1);

        return CalculateResult(nodes);
    }

    public static long Part2()
    {
        var nodes = BuildLinkedList(811_589_153);

        Mix(nodes, 10);

        return CalculateResult(nodes);
    }

    private static List<Node> BuildLinkedList(long multiplier)
    {
        var nodes = new List<Node>();

        foreach (var item in Input)
        {
            var node = new Node
            {
                Number = item.Number * multiplier,
                OriginalIndex = item.OriginalIndex,
            };

            var left = nodes.LastOrDefault();

            if (left is not null)
            {
                node.Left = left;
                left.Right = node;
            }

            nodes.Add(node);
        }

        nodes.First().Left = nodes.Last();
        nodes.Last().Right = nodes.First();

        return nodes;
    }

    private static void Mix(IReadOnlyCollection<Node> nodes, int timesToMix)
    {
        for (var t = 0; t < timesToMix; t++)
        {
            Mix(nodes);
        }
    }

    private static void Mix(IReadOnlyCollection<Node> nodes)
    {
        var max = Input.Max(x => x.OriginalIndex);

        for (var i = 0; i <= max; i++)
        {
            var node = nodes.First(x => x.OriginalIndex == i);
            var number = node.Number % max;

            switch (number)
            {
                case > 0:
                    MoveRight(node, number);
                    break;
                case < 0:
                    MoveLeft(node, number);
                    break;
            }
        }
    }

    private static void MoveLeft(Node node, long number)
    {
        for (var j = 0; j < -number; j++)
        {
            Swap(node.Left, node);
        }
    }

    private static void MoveRight(Node node, long number)
    {
        for (var j = 0; j < number; j++)
        {
            Swap(node, node.Right);
        }
    }

    private static void Swap(Node left, Node right)
    {
        var (a, b, c, d) = (left.Left, left, right, right.Right);

        a.Right = right;
        b.Left = right;
        b.Right = d;
        c.Left = a;
        c.Right = left;
        d.Left = left;
    }

    private static long CalculateResult(IEnumerable<Node> nodes)
    {
        var node = nodes.Single(n => n.Number == 0);
        var numbers = new List<long>();

        for (var k = 1; k <= 3000; k++)
        {
            node = node.Right;

            if (k % 1000 == 0)
            {
                numbers.Add(node.Number);
            }
        }

        return numbers.Sum();
    }

    private class Node
    {
        public long Number { get; init; }
        public int OriginalIndex { get; init; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}
