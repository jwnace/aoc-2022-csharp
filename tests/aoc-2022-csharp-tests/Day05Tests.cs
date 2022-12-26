using aoc_2022_csharp.Day05;

namespace aoc_2022_csharp_tests;

public class Day05Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = "FRDSQRRCD";
        var actual = Day05.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = "HRFTQVWNN";
        var actual = Day05.Part2();
        actual.Should().Be(expected);
    }
}
