using aoc_2022_csharp.Day16;

namespace aoc_2022_csharp_tests;

public class Day16Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 1460;

        // act
        var actual = Day16.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 2117;

        // act
        var actual = Day16.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
