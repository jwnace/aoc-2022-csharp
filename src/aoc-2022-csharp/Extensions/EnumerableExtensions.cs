namespace aoc_2022_csharp.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Gets all combinations of a given length for a collection.
    /// </summary>
    /// <param name="enumerable">The collection to get combinations for.</param>
    /// <param name="length">The desired length of the individual combinations.</param>
    /// <typeparam name="T">The type of the values in the collection.</typeparam>
    /// <returns>All combinations for the collection of the requested length.</returns>
    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> enumerable, int length)
        where T : IComparable
    {
        if (length == 1)
        {
            return enumerable.Select(x => new List<T> { x });
        }

        return GetCombinations(enumerable, length - 1)
            .SelectMany(x => enumerable.Where(y => y.CompareTo(x.Last()) > 0),
                (a, b) => a.Concat(new[] { b }).ToList());
    }

    /// <summary>
    /// Gets all permutations of a given length for a collection.
    /// </summary>
    /// <param name="enumerable">The collection to get permutations for.</param>
    /// <param name="length">The desired length of the individual permutations.</param>
    /// <typeparam name="T">The type of the values in the collection.</typeparam>
    /// <returns>All permutations for the collection of the requested length.</returns>
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable, int length)
    {
        if (length == 1)
        {
            return enumerable.Select(x => new List<T> { x });
        }

        return GetPermutations(enumerable, length - 1)
            .SelectMany(x => enumerable.Where(y => !x.Contains(y)),
                (a, b) => a.Concat(new[] { b }).ToList());
    }

    /// <summary>
    /// Breaks up a string into overlapping slices of a given length.
    /// </summary>
    /// <param name="input">The input string to be sliced.</param>
    /// <param name="length">The desired length of the individual slices.</param>
    /// <returns>Overlapping slices of the input string of the requested length.</returns>
    public static IEnumerable<IEnumerable<T>> Slice<T>(this IEnumerable<T> input, int length)
    {
        for (var i = 0; i < input.Count() - length + 1; i++)
        {
            yield return input.Skip(i).Take(length);
        }
    }
}
