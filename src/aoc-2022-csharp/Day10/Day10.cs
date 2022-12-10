using System.Text;

namespace aoc_2022_csharp.Day10;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10/day10.txt");

    public static int Part1()
    {
        var register = 1;
        var cycle = 0;
        var cycleValues = new Dictionary<int, int>();
        var stack = new Stack<string>();

        for (var i = 0; i < Input.Length; i++)
        {
            cycle++;

            var line = Input[i];

            // check if this is one of the cycles we care about
            if (new[] { 20, 60, 100, 140, 180, 220 }.Contains(cycle))
            {
                cycleValues[cycle] = register;
            }

            var values = line.Split(' ');

            if (values[0] == "noop")
            {
                continue;
            }

            if (stack.Count == 0)
            {
                stack.Push(line);
                i--;
                continue;
            }

            var temp = stack.Pop();

            if (temp != line)
            {
                throw new InvalidOperationException();
            }

            register += int.Parse(values[1]);
        }

        return cycleValues.Sum(x => x.Key * x.Value);
    }

    public static string Part2()
    {
        var register = 1;
        var cycle = 0;
        var stack = new Stack<string>();
        var crt = new char[240];

        for (var i = 0; i < Input.Length; i++)
        {
            var line = Input[i];

            var foo = new[] { register - 1, register, register + 1 };

            if (foo.Contains(cycle % 40))
            {
                crt[cycle] = '#';
            }
            else
            {
                crt[cycle] = ' ';
            }

            cycle++;

            var values = line.Split(' ');

            if (values[0] == "noop")
            {
                continue;
            }

            if (stack.Count == 0)
            {
                stack.Push(line);
                i--;
                continue;
            }

            stack.Pop();

            register += int.Parse(values[1]);
        }

        var sb = new StringBuilder();

        for (var j = 0; j < crt.Length; j++)
        {
            if (j % 40 == 0)
            {
                sb.Append(Environment.NewLine);
            }

            sb.Append(crt[j]);
        }

        return sb.ToString();
    }
}
