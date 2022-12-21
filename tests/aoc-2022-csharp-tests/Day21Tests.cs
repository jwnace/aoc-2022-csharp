using aoc_2022_csharp.Day21;

namespace aoc_2022_csharp_tests;

public class Day21Tests
{
    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 38_914_458_159_166;

        // act
        var actual = Day21.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 3_665_520_865_940;

        // act
        var actual = Day21.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
