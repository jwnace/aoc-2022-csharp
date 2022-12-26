using aoc_2022_csharp.Day16;

namespace aoc_2022_csharp_tests;

public class Day16Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 1_460;
        var actual = Day16.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 2_117;
        var actual = Day16.Part2();
        actual.Should().Be(expected);
    }
}
