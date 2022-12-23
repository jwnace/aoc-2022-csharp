using aoc_2022_csharp.Day22;

namespace aoc_2022_csharp_tests;

public class Day22Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 50412;

        // act
        var actual = Day22.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 130068;

        // act
        var actual = Day22.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
