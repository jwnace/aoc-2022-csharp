using aoc_2022_csharp.Day20;

namespace aoc_2022_csharp_tests;

public class Day20Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 8_372;
        var actual = Day20.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 7_865_110_481_723;
        var actual = Day20.Part2();
        actual.Should().Be(expected);
    }
}
