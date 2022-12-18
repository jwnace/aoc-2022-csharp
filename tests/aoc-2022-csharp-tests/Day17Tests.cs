using aoc_2022_csharp.Day17;

namespace aoc_2022_csharp_tests;

public class Day17Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 3127;

        // act
        var actual = Day17.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 1_542_941_176_480;

        // act
        var actual = Day17.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
