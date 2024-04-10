namespace MapFilterFold;

/// <summary>
/// Class contains three helper functions: Map, Filter and Fold.
/// </summary>
public static class Functions
{
    /// <summary>
    /// Maps each element of the given list using specified map function.
    /// </summary>
    /// <typeparam name="TOldValue">Type of elements in the given list.</typeparam>
    /// <typeparam name="TNewValue">Type of element that map function returns.</typeparam>
    /// <param name="list">List of elements to map.</param>
    /// <param name="mapFunc">Function to map elements.</param>
    /// <returns>List with mapped elemnts.</returns>
    public static List<TNewValue> Map<TOldValue, TNewValue>(List<TOldValue> list, Func<TOldValue, TNewValue> mapFunc)
    {
        List<TNewValue> newList = new ();
        foreach (var item in list)
        {
            newList.Add(mapFunc(item));
        }

        return newList;
    }

    /// <summary>
    /// Filters elements of the given list using given predicate function.
    /// </summary>
    /// <typeparam name="T">Type of elements in the given list.</typeparam>
    /// <param name="list">List to filter elemnts from.</param>
    /// <param name="predicate">Function to filter with.</param>
    /// <returns>List that contains elements that satisfies predicate.</returns>
    public static List<T> Filter<T>(List<T> list, Func<T, bool> predicate)
    {
        List<T> newList = new ();
        foreach (var item in list)
        {
            if (predicate(item))
            {
                newList.Add(item);
            }
        }

        return newList;
    }

    /// <summary>
    /// Changes the initial value by using given fold function step by step applying it to the initial value and next element in the list.
    /// </summary>
    /// <typeparam name="TValue">Type of elements in the given list.</typeparam>
    /// <typeparam name="TAccumulatedValue">Type that fold function returns.</typeparam>
    /// <param name="list">List to fold.</param>
    /// <param name="initialValue">Initial value.</param>
    /// <param name="foldFunc">Function to fold with.</param>
    /// <returns>Value accumulated by applying fold function.</returns>
    public static TAccumulatedValue Fold<TValue, TAccumulatedValue>(List<TValue> list, TAccumulatedValue initialValue, Func<TAccumulatedValue, TValue, TAccumulatedValue> foldFunc)
    {
        foreach (var item in list)
        {
            initialValue = foldFunc(initialValue, item);
        }

        return initialValue;
    }
}
