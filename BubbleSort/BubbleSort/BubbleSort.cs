namespace BubbleSort;

/// <summary>
/// Static class realizes buble sort for generic collections.
/// </summary>
public static class BubbleSort
{
    /// <summary>
    /// Sorts elements of the given collection using bubble sort.
    /// </summary>
    /// <typeparam name="T">Type of elements in the collection.</typeparam>
    /// <param name="values">Collection to sort.</param>
    /// <param name="comparer">Comparer to compare elements of the collection.</param>
    /// <returns>Array that contains elements of the collection in sorted order.</returns>
    public static T[] Sort<T>(ICollection<T> values, IComparer<T> comparer)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(comparer);

        var size = values.Count;
        if (size == 0)
        {
            return Array.Empty<T>();
        }

        T[] array = new T[size];
        values.CopyTo(array, 0);

        for (int i = 0; i < size; ++i) {
            for (int j = 0; j < size - 1; ++j)
            {
                if (comparer.Compare(array[j], array[j + 1]) > 0)
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }

        return array;
    }
}