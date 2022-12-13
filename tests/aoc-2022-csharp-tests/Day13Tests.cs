using aoc_2022_csharp.Day13;
using Newtonsoft.Json;

namespace aoc_2022_csharp_tests;

public class Day13Tests
{
    [TestCase("[1,1,3,1,1]", "[1,1,5,1,1]", true)]
    [TestCase("[[1],[2,3,4]]", "[[1],4]", true)]
    [TestCase("[9]", "[[8,7,6]]", false)]
    [TestCase("[[4,4],4,4]", "[[4,4],4,4,4]", true)]
    [TestCase("[7,7,7,7]", "[7,7,7]", false)]
    [TestCase("[]", "[3]", true)]
    [TestCase("[[[]]]", "[[]]", false)]
    [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", false)]
    [TestCase("[[],[],[9,7]]", "[[],[]]", false)]
    public void AreInCorrectOrderTest(string left, string right, bool expected)
    {
        // act
        var actual = Day13.AreInCorrectOrder(JsonConvert.DeserializeObject(left), JsonConvert.DeserializeObject(right));

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part1Test()
    {
        // arrange
        var expected = 5684;

        // act
        var actual = Day13.Part1();

        // assert
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2Test()
    {
        // arrange
        var expected = 22932;

        // act
        var actual = Day13.Part2();

        // assert
        actual.Should().Be(expected);
    }
}
