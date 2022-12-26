using aoc_2022_csharp.Day23;

namespace aoc_2022_csharp_tests;

public class Day23Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 3_966;
        var actual = Day23.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 933;
        var actual = Day23.Part2();
        actual.Should().Be(expected);
    }
}
