using System.Security.Cryptography;
using System.Text;

namespace aoc_2022_csharp.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Gets all combinations of a given length for a string.
    /// </summary>
    /// <param name="input">The string to get combinations for.</param>
    /// <param name="length">The desired length of the individual combinations.</param>
    /// <returns>All combinations of the requested length for the input string.</returns>
    public static IEnumerable<string> GetCombinations(this string input, int length) =>
        ((IEnumerable<char>)input).GetCombinations(length).Select(x => new string(x.ToArray()));

    /// <summary>
    /// Gets all permutations of a given length for a string.
    /// </summary>
    /// <param name="input">The string to get permutations for.</param>
    /// <param name="length">The desired length of the individual permutations.</param>
    /// <returns>All permutations of the requested length for the input string.</returns>
    public static IEnumerable<string> GetPermutations(this string input, int length) =>
        ((IEnumerable<char>)input).GetPermutations(length).Select(x => new string(x.ToArray()));

    /// <summary>
    /// Breaks up a string into overlapping slices of a given length.
    /// </summary>
    /// <param name="input">The input string to be sliced.</param>
    /// <param name="length">The desired length of the individual slices.</param>
    /// <returns>Overlapping slices of the input string of the requested length.</returns>
    public static IEnumerable<string> Slice(this string input, int length)
    {
        for (var i = 0; i < input.Length - length + 1; i++)
        {
            yield return input.Substring(i, length);
        }
    }

    /// <summary>
    /// Generates an MD5 hash for a given input string.
    /// </summary>
    /// <param name="input">The input string to be hashed.</param>
    /// <returns>A new string representing the MD5 hash of the input string.</returns>
    public static string ToMd5String(this string input)
    {
        using var md5 = MD5.Create();
        return Convert.ToHexString(md5.ComputeHash(Encoding.ASCII.GetBytes(input))).ToLower();
    }
}
