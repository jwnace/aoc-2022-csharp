using aoc_2022_csharp.Day24;

namespace aoc_2022_csharp_tests;

public class Day24Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 260;

        // act
        var actual = Day24.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 0;

        // act
        var actual = Day24.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
