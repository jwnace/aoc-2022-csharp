using aoc_2022_csharp.Day07;

namespace aoc_2022_csharp_tests;

public class Day07Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 1_513_699;
        var actual = Day07.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 7_991_939;
        var actual = Day07.Part2();
        actual.Should().Be(expected);
    }
}
