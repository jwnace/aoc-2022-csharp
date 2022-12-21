using System.Diagnostics;

namespace aoc_2022_csharp.Day20;

public static class Day20
{
    private static readonly (long Number, int OriginalIndex)[] Input = File.ReadAllLines("Day20/day20.txt")
        .Select(long.Parse)
        .Select((x, i) => (x, i))
        .ToArray();

    public static long Part1()
    {
        var nodes = new List<Node>();

        foreach (var item in Input)
        {
            var node = new Node
            {
                OriginalIndex = item.OriginalIndex,
                Number = item.Number
            };

            var left = nodes.LastOrDefault();

            if (left is not null)
            {
                node.Left = left;
                left.Right = node;
            }

            nodes.Add(node);
        }

        var first = nodes.First();
        var last = nodes.Last();

        first.Left = last;
        last.Right = first;

        for (var i = Input.Min(x => x.OriginalIndex); i <= Input.Max(x => x.OriginalIndex); i++)
        {
            var item = nodes.First(x => x.OriginalIndex == i);
            var number = item.Number;

            if (number > 0)
            {
                // move right
                for (var j = 0; j < number; j++)
                {
                    var a = item.Left;
                    var b = item;
                    var c = item.Right;
                    var d = item.Right.Right;

                    a.Right = c;

                    b.Left = c;
                    b.Right = d;

                    c.Left = a;
                    c.Right = b;

                    d.Left = b;
                }
            }
            else if (number < 0)
            {
                // move left
                for (var j = 0; j < -number; j++)
                {
                    var a = item.Left.Left;
                    var b = item.Left;
                    var c = item;
                    var d = item.Right;

                    a.Right = c;

                    b.Left = c;
                    b.Right = d;

                    c.Left = a;
                    c.Right = b;

                    d.Left = b;
                }
            }
        }

        var start = nodes.Single(n => n.Number == 0);
        var temp = start;

        for (var k = 0; k < 1000; k++)
        {
            temp = temp.Right;
        }

        Console.WriteLine($"1000th number: {temp.Number}");
        var n1 = temp.Number;

        for (var k = 0; k < 1000; k++)
        {
            temp = temp.Right;
        }

        Console.WriteLine($"2000th number: {temp.Number}");
        var n2 = temp.Number;

        for (var k = 0; k < 1000; k++)
        {
            temp = temp.Right;
        }

        Console.WriteLine($"3000th number: {temp.Number}");
        var n3 = temp.Number;

        return n1 + n2 + n3;
    }

    public static long Part2()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // build the list
        var nodes = BuildLinkedList();

        stopwatch.Stop();
        Console.WriteLine($"building the linked list took {stopwatch.Elapsed}");
        stopwatch.Reset();

        stopwatch.Start();

        // mix the list
        Mix(nodes, 10);

        stopwatch.Stop();
        Console.WriteLine($"mixing the list took {stopwatch.Elapsed}");
        stopwatch.Reset();

        var start = nodes.Single(n => n.Number == 0);
        var temp = start;

        for (var k = 0; k < 1000; k++)
        {
            temp = temp.Right;
        }

        Console.WriteLine($"1000th number: {temp.Number}");
        var n1 = temp.Number;

        for (var k = 0; k < 1000; k++)
        {
            temp = temp.Right;
        }

        Console.WriteLine($"2000th number: {temp.Number}");
        var n2 = temp.Number;

        for (var k = 0; k < 1000; k++)
        {
            temp = temp.Right;
        }

        Console.WriteLine($"3000th number: {temp.Number}");
        var n3 = temp.Number;

        return n1 + n2 + n3;
    }

    private static List<Node> BuildLinkedList()
    {
        var nodes = new List<Node>();

        foreach (var item in Input)
        {
            var node = new Node
            {
                OriginalIndex = item.OriginalIndex,
                Number = item.Number * 811_589_153
            };

            var left = nodes.LastOrDefault();

            if (left is not null)
            {
                node.Left = left;
                left.Right = node;
            }

            nodes.Add(node);
        }

        var first = nodes.First();
        var last = nodes.Last();

        first.Left = last;
        last.Right = first;
        return nodes;
    }

    private static void Mix(List<Node> nodes, int t)
    {
        for (var times = 0; times < t; times++)
        {
            Console.WriteLine($"mixing the list, {times + 1} / {t} times");

            var min = Input.Min(x => x.OriginalIndex);
            var max = Input.Max(x => x.OriginalIndex);

            for (var i = min; i <= max; i++)
            {
                Console.WriteLine($"    {i} / {max} => {Math.Round((double)i / max * 100.0, 2)}%");

                var item = nodes.First(x => x.OriginalIndex == i);
                var number = item.Number % max;

                if (number > 0)
                {
                    // move right
                    for (var j = 0; j < number; j++)
                    {
                        var a = item.Left;
                        var b = item;
                        var c = item.Right;
                        var d = item.Right.Right;

                        a.Right = c;

                        b.Left = c;
                        b.Right = d;

                        c.Left = a;
                        c.Right = b;

                        d.Left = b;
                    }
                }
                else if (number < 0)
                {
                    // move left
                    for (var j = 0; j < -number; j++)
                    {
                        var a = item.Left.Left;
                        var b = item.Left;
                        var c = item;
                        var d = item.Right;

                        a.Right = c;

                        b.Left = c;
                        b.Right = d;

                        c.Left = a;
                        c.Right = b;

                        d.Left = b;
                    }
                }
            }
        }
    }

    private class Node
    {
        public long Number { get; set; }
        public int OriginalIndex { get; set; }

        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}
