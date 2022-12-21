using aoc_2022_csharp.Day20;

namespace aoc_2022_csharp_tests;

public class Day20Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 8372;

        // act
        var actual = Day20.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 7_865_110_481_723;

        // act
        var actual = Day20.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
