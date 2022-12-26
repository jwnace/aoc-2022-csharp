using aoc_2022_csharp.Day08;

namespace aoc_2022_csharp_tests;

public class Day08Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 1_840;
        var actual = Day08.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 405_769;
        var actual = Day08.Part2();
        actual.Should().Be(expected);
    }
}
