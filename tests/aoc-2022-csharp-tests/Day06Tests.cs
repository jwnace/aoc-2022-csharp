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
    public void GetStartOfPacketMarker_ReturnsCorrectResult(string message, int sliceSize, int expected)
    {
        var actual = Day06.GetStartOfPacketMarker(message, sliceSize);
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 1_892;
        var actual = Day06.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected = 2_313;
        var actual = Day06.Part2();
        actual.Should().Be(expected);
    }
}
