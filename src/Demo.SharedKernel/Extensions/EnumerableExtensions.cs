namespace Demo.SharedKernel.Extensions;

/// <summary>
/// Provides extension methods for IEnumerable collections.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Performs the specified action on each element of the IEnumerable.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">The IEnumerable to iterate through.</param>
    /// <param name="action">The Action delegate to perform on each element of the IEnumerable.</param>
    /// <exception cref="ArgumentNullException">Thrown if source or action is null.</exception>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (action == null) throw new ArgumentNullException(nameof(action));

        foreach (var item in source)
        {
            action(item);
        }
    }

    /// <summary>
    /// Determines whether the number of elements in the sequence is greater than the specified count.
    /// This method can be more efficient than Count() for large or infinite sequences as it stops counting once the threshold is exceeded.
    /// </summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">The IEnumerable to check.</param>
    /// <param name="count">The count to compare against.</param>
    /// <returns>true if the number of elements is greater than count; otherwise, false.</returns>
    public static bool HasMoreThan<T>(this IEnumerable<T> source, int count)
    {
        if (source == null) return false;
        if (count < 0) return true; // Any non-null collection has more than -1 elements

        var collection = source as ICollection<T> ?? source.ToList();
        return collection.Count > count;
    }
}