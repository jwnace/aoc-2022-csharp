using aoc_2022_csharp.Day18;

namespace aoc_2022_csharp_tests;

public class Day18Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 4400;

        // act
        var actual = Day18.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 2522;

        // act
        var actual = Day18.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
