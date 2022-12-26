using aoc_2022_csharp.Day14;

namespace aoc_2022_csharp_tests;

public class Day14Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 692;
        var actual = Day14.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 31_706;
        var actual = Day14.Part2();
        actual.Should().Be(expected);
    }
}
