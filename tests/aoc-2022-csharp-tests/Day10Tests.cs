using aoc_2022_csharp.Day10;

namespace aoc_2022_csharp_tests;

public class Day10Tests
{
    [Test]
    public void Part1_ReturnsCorrectResult()
    {
        var expected = 11_820;
        var actual = Day10.Part1();
        actual.Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectResult()
    {
        var expected =
            Environment.NewLine + "#### ###    ## ###  ###  #  #  ##  #  # " +
            Environment.NewLine + "#    #  #    # #  # #  # # #  #  # #  # " +
            Environment.NewLine + "###  #  #    # ###  #  # ##   #  # #### " +
            Environment.NewLine + "#    ###     # #  # ###  # #  #### #  # " +
            Environment.NewLine + "#    #    #  # #  # # #  # #  #  # #  # " +
            Environment.NewLine + "#### #     ##  ###  #  # #  # #  # #  # ";

        var actual = Day10.Part2();
        actual.Should().Be(expected);
    }
}
