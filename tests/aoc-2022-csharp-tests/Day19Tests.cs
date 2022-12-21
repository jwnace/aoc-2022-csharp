using aoc_2022_csharp.Day19;

namespace aoc_2022_csharp_tests;

public class Day19Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 1565;

        // act
        var actual = Day19.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 10672;

        // act
        var actual = Day19.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
