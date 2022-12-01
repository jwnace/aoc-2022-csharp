using aoc_2022_csharp.Day01;

namespace aoc_2022_csharp_tests;

public class Day01Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 69528;

        // act
        var actual = Day01.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 206152;

        // act
        var actual = Day01.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
