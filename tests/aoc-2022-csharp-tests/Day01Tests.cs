using aoc_2022_csharp.Day01;

namespace aoc_2022_csharp_tests;

public class Day01Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 69_528;
        var actual = Day01.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 206_152;
        var actual = Day01.Part2();
        actual.Should().Be(expected);
    }
}
