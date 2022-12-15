using aoc_2022_csharp.Day15;

namespace aoc_2022_csharp_tests;

public class Day15Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 5_040_643;

        // act
        var actual = Day15.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 11_016_575_214_126;

        // act
        var actual = Day15.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
