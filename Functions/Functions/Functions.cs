namespace Functions;

public static class Functions
{
    public static List<TNewValue> Map<TOldValue, TNewValue>(List<TOldValue> list, Func<TOldValue, TNewValue> func)
    {
        List<TNewValue> newList = new ();
        foreach (var item in list)
        {
            newList.Add(func(item));
        }

        return newList;
    }

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

    public static T Fold<T>(List<T> list, T initialValue, Func<T, T, T> func)
    {
        foreach (var item in list)
        {
            initialValue = func(initialValue, item);
        }

        return initialValue;
    }
}
