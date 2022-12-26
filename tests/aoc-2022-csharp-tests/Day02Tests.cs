using aoc_2022_csharp.Day02;

namespace aoc_2022_csharp_tests;

public class Day02Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 13_009;
        var actual = Day02.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 10_398;
        var actual = Day02.Part2();
        actual.Should().Be(expected);
    }
}
