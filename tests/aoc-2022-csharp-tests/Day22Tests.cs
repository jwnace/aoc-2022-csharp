using aoc_2022_csharp.Day22;

namespace aoc_2022_csharp_tests;

public class Day22Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 50_412;
        var actual = Day22.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 130_068;
        var actual = Day22.Part2();
        actual.Should().Be(expected);
    }
}
