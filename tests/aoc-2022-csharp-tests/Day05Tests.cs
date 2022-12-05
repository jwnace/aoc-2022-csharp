using aoc_2022_csharp.Day05;

namespace aoc_2022_csharp_tests;

public class Day05Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = "FRDSQRRCD";

        // act
        var actual = Day05.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = "HRFTQVWNN";

        // act
        var actual = Day05.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
