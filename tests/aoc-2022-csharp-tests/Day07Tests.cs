using aoc_2022_csharp.Day07;

namespace aoc_2022_csharp_tests;

public class Day07Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 1513699;

        // act
        var actual = Day07.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 7991939;

        // act
        var actual = Day07.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
