using aoc_2022_csharp.Day21;

namespace aoc_2022_csharp_tests;

public class Day21Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 38_914_458_159_166;
        var actual = Day21.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 3_665_520_865_940;
        var actual = Day21.Part2();
        actual.Should().Be(expected);
    }
}
