using aoc_2022_csharp.Day04;

namespace aoc_2022_csharp_tests;

public class Day04Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 466;
        var actual = Day04.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 865;
        var actual = Day04.Part2();
        actual.Should().Be(expected);
    }
}
