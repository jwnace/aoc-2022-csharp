using aoc_2022_csharp.Day23;

namespace aoc_2022_csharp_tests;

public class Day23Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 3966;

        // act
        var actual = Day23.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 933;

        // act
        var actual = Day23.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
