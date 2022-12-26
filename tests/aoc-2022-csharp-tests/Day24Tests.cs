using aoc_2022_csharp.Day24;

namespace aoc_2022_csharp_tests;

public class Day24Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 260;
        var actual = Day24.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 747;
        var actual = Day24.Part2();
        actual.Should().Be(expected);
    }
}
