using aoc_2022_csharp.Day11;

namespace aoc_2022_csharp_tests;

public class Day11Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 99852;

        // act
        var actual = Day11.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 25935263541;

        // act
        var actual = Day11.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
