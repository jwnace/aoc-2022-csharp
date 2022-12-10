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
        var callStack = new Stack<string>();

        for (var i = 0; i < Input.Length; i++)
        {
            var line = Input[i];
            var values = line.Split(' ');
            cycle++;

            if (new[] { 20, 60, 100, 140, 180, 220 }.Contains(cycle))
            {
                cycleValues[cycle] = register;
            }

            if (values[0] == "noop")
            {
                continue;
            }

            if (callStack.Count == 0)
            {
                callStack.Push(line);
                i--;
                continue;
            }

            callStack.Pop();
            register += int.Parse(values[1]);
        }

        return cycleValues.Sum(x => x.Key * x.Value);
    }

    public static string Part2()
    {
        var register = 1;
        var cycle = 0;
        var callStack = new Stack<string>();
        var crt = new char[240];

        for (var i = 0; i < Input.Length; i++)
        {
            var line = Input[i];
            var values = line.Split(' ');
            var sprite = new[] { register - 1, register, register + 1 };
            crt[cycle] = sprite.Contains(cycle % 40) ? '#' : ' ';
            cycle++;

            if (values[0] == "noop")
            {
                continue;
            }

            if (callStack.Count == 0)
            {
                callStack.Push(line);
                i--;
                continue;
            }

            callStack.Pop();
            register += int.Parse(values[1]);
        }

        return GenerateOutput(crt);
    }

    private static string GenerateOutput(char[] crt)
    {
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
