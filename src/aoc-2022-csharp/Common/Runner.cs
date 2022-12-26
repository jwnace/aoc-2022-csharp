using System.Diagnostics;

namespace aoc_2022_csharp.Common;

internal static class Runner
{
    public static async Task RunPart<TResult>(int day, int part, Func<TResult> callback)
    {
        var stopwatch = Stopwatch.StartNew();
        var spinner = StartSpinner(day, part, stopwatch);
        var result = await Task.Run(callback);

        stopwatch.Stop();
        spinner.Stop();

        Console.WriteLine($"Day {day.ToString(),2}, Part {part.ToString(),2} {stopwatch.Elapsed} => {result}");
    }

    private static ConsoleSpinner StartSpinner(int day, int part, Stopwatch? stopwatch = null)
    {
        var spinner = new ConsoleSpinner(message: $"Day {day.ToString(),2}, Part {part.ToString(),2} ", stopwatch);
        spinner.Start();
        return spinner;
    }
}
