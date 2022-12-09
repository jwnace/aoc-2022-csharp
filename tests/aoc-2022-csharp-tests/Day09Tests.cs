using aoc_2022_csharp.Day09;

namespace aoc_2022_csharp_tests;

public class Day09Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 5513;

        // act
        var actual = Day09.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 2427;

        // act
        var actual = Day09.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
