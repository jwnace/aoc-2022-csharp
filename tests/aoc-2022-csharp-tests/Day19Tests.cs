using aoc_2022_csharp.Day19;

namespace aoc_2022_csharp_tests;

public class Day19Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 1_565;
        var actual = Day19.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 10_672;
        var actual = Day19.Part2();
        actual.Should().Be(expected);
    }
}
