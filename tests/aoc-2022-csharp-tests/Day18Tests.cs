using aoc_2022_csharp.Day18;

namespace aoc_2022_csharp_tests;

public class Day18Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 4_400;
        var actual = Day18.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 2_522;
        var actual = Day18.Part2();
        actual.Should().Be(expected);
    }
}
