using aoc_2022_csharp.Day11;

namespace aoc_2022_csharp_tests;

public class Day11Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 99_852;
        var actual = Day11.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 25_935_263_541;
        var actual = Day11.Part2();
        actual.Should().Be(expected);
    }
}
