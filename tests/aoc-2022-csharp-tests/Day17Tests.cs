using aoc_2022_csharp.Day17;

namespace aoc_2022_csharp_tests;

public class Day17Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 3_127;
        var actual = Day17.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 1_542_941_176_480;
        var actual = Day17.Part2();
        actual.Should().Be(expected);
    }
}
