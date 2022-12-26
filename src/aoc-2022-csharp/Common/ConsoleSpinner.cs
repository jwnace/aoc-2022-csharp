using System.Diagnostics;

namespace aoc_2022_csharp.Common;

internal class ConsoleSpinner
{
    private static readonly string[] Sequence = { "   ", ".  ", ".. ", "..." };
    private readonly string _message;
    private readonly Stopwatch? _stopwatch;
    private readonly int _left;
    private readonly int _top;
    private readonly int _delay;
    private readonly Timer _timer;
    private int _counter;

    public ConsoleSpinner(
        string message = "",
        Stopwatch? stopwatch = null,
        int? left = null,
        int? top = null,
        int delay = 200)
    {
        _message = message;
        _stopwatch = stopwatch;
        _left = left ?? Console.CursorLeft;
        _top = top ?? Console.CursorTop;
        _delay = delay;
        _timer = new Timer(_ => Spin());
    }

    public void Start() => _timer.Change(dueTime: TimeSpan.Zero, period: TimeSpan.FromMilliseconds(_delay));

    public void Stop()
    {
        _timer.Change(dueTime: -1, period: -1);
        Console.SetCursorPosition(_left, _top);
    }

    private void Spin()
    {
        var elapsed = _stopwatch?.Elapsed.ToString() ?? "";
        var sequence = _stopwatch is null ? Sequence[++_counter % Sequence.Length] : "...";
        Console.SetCursorPosition(_left, _top);
        Console.WriteLine($"{_message}{elapsed}{sequence}");
    }
}
