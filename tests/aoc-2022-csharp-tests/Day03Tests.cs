using aoc_2022_csharp.Day03;

namespace aoc_2022_csharp_tests;

public class Day03Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 7_811;
        var actual = Day03.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 2_639;
        var actual = Day03.Part2();
        actual.Should().Be(expected);
    }
}
