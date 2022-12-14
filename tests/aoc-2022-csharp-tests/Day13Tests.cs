using aoc_2022_csharp.Day13;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    [TestCase("[[],[]]", "[[],[],[9,7]]", true)]
    public void AreInCorrectOrderTest(string left, string right, bool expected)
    {
        // act
        var actual = Day13.AreInCorrectOrder((JArray)JsonConvert.DeserializeObject(left), (JArray)JsonConvert.DeserializeObject(right));

        // assert
        actual.Should().Be(expected);
    }
    
    [TestCase("[1,1,3,1,1]", "[1,1,5,1,1]", -1)]
    [TestCase("[[1],[2,3,4]]", "[[1],4]", -1)]
    [TestCase("[9]", "[[8,7,6]]", 1)]
    [TestCase("[[4,4],4,4]", "[[4,4],4,4,4]", -1)]
    [TestCase("[7,7,7,7]", "[7,7,7]", 1)]
    [TestCase("[]", "[3]", -1)]
    [TestCase("[[[]]]", "[[]]", 1)]
    [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]", "[1,[2,[3,[4,[5,6,0]]]],8,9]", 1)]
    [TestCase("[[],[],[9,7]]", "[[],[]]", 1)]
    [TestCase("[[],[]]", "[[],[],[9,7]]", -1)]
    [TestCase("[69,420]", "[69,420]", 0)]
    [TestCase("[420,69]", "[420,69]", 0)]
    public void CompareTest(string left, string right, int expected)
    {
        // act
        var actual = Day13.Compare((JArray)JsonConvert.DeserializeObject(left), (JArray)JsonConvert.DeserializeObject(right));

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
