using aoc_2022_csharp.Day06;

namespace aoc_2022_csharp_tests;

public class Day06Tests
{
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 4, 7)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 4, 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 4, 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 4, 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 4, 11)]
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14, 19)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 14, 23)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 14, 23)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 14, 29)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 14, 26)]
    public void GetStartOfPacketMarkerTest(string message, int sliceSize, int expected)
    {
        // act
        var actual = Day06.GetStartOfPacketMarker(message, sliceSize);

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 1892;

        // act
        var actual = Day06.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 2313;

        // act
        var actual = Day06.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
