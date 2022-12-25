using aoc_2022_csharp.Day25;

namespace aoc_2022_csharp_tests;

public class Day25Tests
{
    [TestCase("1", 1)]
    [TestCase("2", 2)]
    [TestCase("1=", 3)]
    [TestCase("1-", 4)]
    [TestCase("10", 5)]
    [TestCase("11", 6)]
    [TestCase("12", 7)]
    [TestCase("2=", 8)]
    [TestCase("2-", 9)]
    [TestCase("20", 10)]
    [TestCase("1=0", 15)]
    [TestCase("1-0", 20)]
    [TestCase("2=-01", 976)]
    [TestCase("1=11-2", 2022)]
    [TestCase("1-0---0", 12345)]
    [TestCase("1121-1110-1=0", 314159265)]
    public void ConvertSnafuToDecimal_ReturnsCorrectResult(string input, int expected)
    {
        var actual = Day25.ConvertSnafuToDecimal(input);

        actual.Should().Be(expected);
    }

    [TestCase(1, "1")]
    [TestCase(2, "2")]
    [TestCase(3, "1=")]
    [TestCase(4, "1-")]
    [TestCase(5, "10")]
    [TestCase(6, "11")]
    [TestCase(7, "12")]
    [TestCase(8, "2=")]
    [TestCase(9, "2-")]
    [TestCase(10, "20")]
    [TestCase(15, "1=0")]
    [TestCase(20, "1-0")]
    [TestCase(976, "2=-01")]
    [TestCase(2022, "1=11-2")]
    [TestCase(12345, "1-0---0")]
    [TestCase(314159265, "1121-1110-1=0")]
    public void ConvertDecimalToSnafu_ReturnsCorrectResult(long input, string expected)
    {
        var actual = Day25.ConvertDecimalToSnafu(input);

        actual.Should().Be(expected);
    }

    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = "2==221=-002=0-02-000";

        // act
        var actual = Day25.Part1();

        // assert
        actual.Should().Be(expected);
    }

    // [Test]
    // public void Part2Test()
    // {
    //     // arrange
    //     var expected = 0;
    //
    //     // act
    //     var actual = Day25.Part2();
    //
    //     // assert
    //     actual.Should().Be(expected);
    // }
}
