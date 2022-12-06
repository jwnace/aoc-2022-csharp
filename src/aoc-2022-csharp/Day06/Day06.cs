using aoc_2022_csharp.Extensions;

namespace aoc_2022_csharp.Day06;

public static class Day06
{
    private static readonly string Input = File.ReadAllText("Day06/day06.txt");

    public static int Part1() => GetStartOfPacketMarker(Input, 4);

    public static int Part2() => GetStartOfPacketMarker(Input, 14);

    public static int GetStartOfPacketMarker(string message, int sliceSize) =>
        message.Slice(sliceSize)
            .Select((s, i) => (Slice: s, Index: i))
            .First(x => x.Slice.Distinct().Count() == sliceSize)
            .Index + sliceSize;
}
