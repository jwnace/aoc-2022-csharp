using aoc_2022_csharp.Day02;

namespace aoc_2022_csharp_tests;

public class Day02Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 13009;

        // act
        var actual = Day02.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 10398;

        // act
        var actual = Day02.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
