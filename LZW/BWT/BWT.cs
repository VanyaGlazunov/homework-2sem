namespace BWT;

/// <summary>
/// Class contains methods for direct and inverse Burrows-Wheeler transfrom.
/// </summary>
public static class BWT
{
    /// <summary>
    /// Performs direct Burrows-Wheeler transform.
    /// </summary>
    /// <param name="inputString">Byte array to transform. </param>
    /// <returns>Transformed byte array and end position of the original byte array. </returns>
    /// <exception cref="ArgumentException"> Thrown when when <paramref name="inputString"/> is null. </exception>
    public static (byte[] Transformed, int EndPosition) Transform(byte[] inputString)
    {
        if (inputString is null)
        {
            throw new ArgumentException("inputString cannot be null");
        }

        var suffixArray = CreateSuffixArray(inputString);
        var endOfString = 0;
        List<byte> transformed = new (inputString.Length);
        for (var i = 0; i < suffixArray.Length; ++i)
        {
            if (suffixArray[i] != 0)
            {
                transformed.Add(inputString[suffixArray[i] - 1]);
            }
            else
            {
                transformed.Add(inputString[^1]);
                endOfString = i;
            }
        }

        return ([..transformed], endOfString);
    }

    /// <summary>
    /// Performs inverse Burrows-Wheeler transform.
    /// </summary>
    /// <param name="transformed">Byte array to perform inverse transform with. </param>
    /// <param name="endOfString"> Zero-based index of the end of original byte array in transformed array. </param>
    /// <returns> Original byte array. </returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="transformed"/> is null. </exception>
    /// <exception cref="IndexOutOfRangeException">Thrown when <paramref name="endOfString"/> is out of range. </exception>
    public static byte[] InverseTransform(byte[] transformed, int endOfString)
    {
        if (transformed is null)
        {
            throw new ArgumentException("transformed cannot be null");
        }

        if (endOfString >= transformed.Length || endOfString < 0)
        {
            throw new IndexOutOfRangeException("endOfString index out of range");
        }

        var positions = new int[transformed.Length];
        for (var i = 0; i < positions.Length; ++i)
        {
            positions[i] = i;
        }

        positions = [.. positions.OrderBy((int a) => transformed[a])];
        Array.Sort(transformed);

        List<byte> orginalArray = new (transformed.Length);
        var position = endOfString;
        for (var i = 0; i < transformed.Length; ++i)
        {
            orginalArray.Add(transformed[position]);
            position = positions[position];
        }

        return [..orginalArray];
    }

    private static int[] CreateSuffixArray(byte[] inputString)
    {
        var suffixArray = new int[inputString.Length];
        var equivalenceClasses = new int[inputString.Length];

        for (var i = 0; i < suffixArray.Length; ++i)
        {
            suffixArray[i] = i;
        }

        suffixArray = suffixArray.OrderBy((int a) => inputString[a]).ToArray();

        for (var i = 1; i < suffixArray.Length; ++i)
        {
            equivalenceClasses[suffixArray[i]] = inputString[suffixArray[i]] == inputString[suffixArray[i - 1]] ?
            equivalenceClasses[suffixArray[i - 1]] : equivalenceClasses[suffixArray[i - 1]] + 1;
        }

        for (var i = 0; (1 << i) < inputString.Length; ++i)
        {
            for (var j = 0; j < suffixArray.Length; ++j)
            {
                suffixArray[j] = (suffixArray[j] - (1 << i) + suffixArray.Length) % suffixArray.Length;
            }

            suffixArray = CountSort(suffixArray, equivalenceClasses);

            var newEquivalenceClasses = new int[equivalenceClasses.Length];

            for (var j = 1; j < suffixArray.Length; ++j)
            {
                var previousBlock = (equivalenceClasses[suffixArray[j - 1]], equivalenceClasses[(suffixArray[j - 1] + (1 << i)) % suffixArray.Length]);
                var currentBlock = (equivalenceClasses[suffixArray[j]], equivalenceClasses[(suffixArray[j] + (1 << i)) % suffixArray.Length]);
                newEquivalenceClasses[suffixArray[j]] = previousBlock == currentBlock ? newEquivalenceClasses[suffixArray[j - 1]] : newEquivalenceClasses[suffixArray[j - 1]] + 1;
            }

            equivalenceClasses = newEquivalenceClasses;
        }

        return suffixArray;
    }

    private static int[] CountSort(int[] suffixArray, int[] equivalenceClasses)
    {
        var count = new int[suffixArray.Length];
        var positions = new int[suffixArray.Length];
        foreach (var element in equivalenceClasses)
        {
            count[element]++;
        }

        for (var i = 1; i < suffixArray.Length; ++i)
        {
            positions[i] = positions[i - 1] + count[i - 1];
        }

        var sortedArray = new int[suffixArray.Length];
        foreach (var element in suffixArray)
        {
            var elementClass = equivalenceClasses[element];
            sortedArray[positions[elementClass]] = element;
            positions[elementClass]++;
        }

        return sortedArray;
    }
}
