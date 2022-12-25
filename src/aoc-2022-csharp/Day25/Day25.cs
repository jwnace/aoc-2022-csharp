using System.Text;

namespace aoc_2022_csharp.Day25;

public static class Day25
{
    private static readonly string[] Input = File.ReadAllLines("Day25/day25.txt");

    public static string Part1()
    {
        return ConvertDecimalToSnafu(Input.Sum(ConvertSnafuToDecimal));
    }

    public static long Part2() => 2;

    public static long ConvertSnafuToDecimal(string input)
    {
        var result = 0L;
        var reversed = input.Reverse().ToArray();

        for (var i = 0; i < input.Length; i++)
        {
            var placeValue = Convert.ToInt64(Math.Pow(5, i));

            var value = reversed[i] switch
            {
                '2' => placeValue * 2,
                '1' => placeValue * 1,
                '0' => 0,
                '-' => placeValue * -1,
                '=' => placeValue * -2,
                _ => throw new ArgumentOutOfRangeException()
            };

            result += value;
        }

        return result;
    }

    public static string ConvertDecimalToSnafu(long input)
    {
        var builder = new StringBuilder();
        var number = input;

        var maxPlaceValue = 1L * 2L;
        var placeIndex = 0;

        while (maxPlaceValue < number)
        {
            maxPlaceValue *= 5;
            placeIndex++;
        }

        // Console.WriteLine($"number: {number} => placeIndex: {placeIndex} => placeValue: {Math.Pow(5, placeIndex)} => maxPlaceValue: {maxPlaceValue}");

        var temp = 0L;

        for (var i = placeIndex; i >= 0; i--)
        {
            var placeValue = Convert.ToInt64(Math.Pow(5, i));

            var possibleValues = new[]
            {
                (Value: placeValue * 2, Symbol: '2'),
                (Value: placeValue * 1, Symbol: '1'),
                (Value: placeValue * 0, Symbol: '0'),
                (Value: placeValue * -1, Symbol: '-'),
                (Value: placeValue * -2, Symbol: '='),
            };

            // pick the smallest value that is bigger than our number
            // var value = possibleValues
            //     .Where(v => temp + v.Value >= number)
            //     .MinBy(v => v.Value);

            var query = possibleValues
                .Select(v => new { Value = v, Difference = Math.Abs((temp + v.Value) - number) })
                .ToList();

            // pick the value that is CLOSEST to our number
            var value = possibleValues
                .Select(v => new { Value = v, Difference = Math.Abs((temp + v.Value) - number) })
                .MinBy(x => x.Difference);

            builder.Append(value.Value.Symbol);
            temp += value.Value.Value;
        }

        return builder.ToString();
    }
}
