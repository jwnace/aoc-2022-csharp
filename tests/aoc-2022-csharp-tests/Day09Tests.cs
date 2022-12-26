using aoc_2022_csharp.Day09;

namespace aoc_2022_csharp_tests;

public class Day09Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 5_513;
        var actual = Day09.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 2_427;
        var actual = Day09.Part2();
        actual.Should().Be(expected);
    }
}
