using aoc_2022_csharp.Day08;

namespace aoc_2022_csharp_tests;

public class Day08Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 1840;

        // act
        var actual = Day08.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 405769;

        // act
        var actual = Day08.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
