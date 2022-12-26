using aoc_2022_csharp.Day12;

namespace aoc_2022_csharp_tests;

public class Day12Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 423;
        var actual = Day12.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 416;
        var actual = Day12.Part2();
        actual.Should().Be(expected);
    }
}
