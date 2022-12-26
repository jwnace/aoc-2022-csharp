using aoc_2022_csharp.Day15;

namespace aoc_2022_csharp_tests;

public class Day15Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 5_040_643;
        var actual = Day15.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 11_016_575_214_126;
        var actual = Day15.Part2();
        actual.Should().Be(expected);
    }
}
