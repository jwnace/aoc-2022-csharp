using System.Text;

namespace aoc_2022_csharp.Day22;

public static class Day22
{
    private static readonly string Input = File.ReadAllText("Day22/day22.txt");

    public static int Part1()
    {
        var values = Input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var map = values[0].Split(Environment.NewLine);
        var path = values[1];
        var instructions = GetInstructions(path);

        // foreach (var line in map)
        // {
        //     Console.WriteLine(line);
        // }

        // Console.WriteLine(path);
        
        // var temp = string.Join(',', instructions);
        // Console.WriteLine(temp);

        foreach (var item in instructions)
        {
            if (item is int number)
            {
                // Console.WriteLine($"{item} is a number!");
            }
        
            if (item is char direction)
            {
                // Console.WriteLine($"{item} is a direction!");
            }
        }
        
        return 1;
    }

    private static List<object> GetInstructions(string path)
    {
        var buffer = new StringBuilder();
        var instructions = new List<object>();

        foreach (var c in path)
        {
            switch (c)
            {
                case 'U':
                case 'D':
                case 'L':
                case 'R':
                    if (buffer.Length > 0)
                    {
                        instructions.Add(int.Parse(buffer.ToString()));
                        buffer.Clear();
                    }

                    instructions.Add(c);
                    break;
                default:
                    buffer.Append(c);
                    break;
            }
        }

        return instructions;
    }

    public static int Part2()
    {
        return 2;
    }
}
