namespace BurrowsWheeler;

using System.Text;

/// <summary>
/// Class contains methods for direct and inverse Burrows-Wheeler transfrom.
/// </summary>
public static class BWT
{
    /// <summary>
    /// Performs direct Burrows-Wheeler transform.
    /// </summary>
    /// <param name="inputString"> String to transform. </param>
    /// <returns> The transformed string and end position of the original string. </returns>
    /// <exception cref="ArgumentException"> Thrown when when <paramref name="inputString"/> is null. </exception>
    public static (string Transformed, int EndPosition) Transform(string? inputString)
    {
        if (inputString is null)
        {
            throw new ArgumentException("inputString cannot be null");
        }

        var suffixArray = CreateSuffixArray(inputString);
        var endOfString = 0;
        StringBuilder transformedString = new (inputString.Length);
        for (var i = 0; i < suffixArray.Length; ++i)
        {
            if (suffixArray[i] != 0)
            {
                transformedString.Append(inputString[suffixArray[i] - 1]);
            }
            else
            {
                transformedString.Append(inputString[^1]);
                endOfString = i;
            }
        }

        return (transformedString.ToString(), endOfString);
    }

    /// <summary>
    /// Performs inverse Burrows-Wheeler transorm.
    /// </summary>
    /// <param name="transformedString"> String to perform inverse transform with. </param>
    /// <param name="endOfString"> Zero-based index of the end of original string in transformed string. </param>
    /// <returns> Returns tranformed string. </returns>
    /// <exception cref="ArgumentException"> Thrown when <paramref name="transformedString"/> is null. </exception>
    /// <exception cref="IndexOutOfRangeException"> Thrown when <paramref name="endOfString"/> is out of range. </exception>
    public static string InverseTransform(string? transformedString, int endOfString)
    {
        if (transformedString is null)
        {
            throw new ArgumentException("transformedString cannot be null");
        }

        if (endOfString >= transformedString.Length || endOfString < 0)
        {
            throw new IndexOutOfRangeException("endOfString index out of range");
        }

        var positions = new int[transformedString.Length];
        for (var i = 0; i < positions.Length; ++i)
        {
            positions[i] = i;
        }

        var transformedStringChars = transformedString.ToCharArray();
        Array.Sort(transformedStringChars, positions);

        StringBuilder orginalString = new (transformedString.Length);
        var position = endOfString;
        for (var i = 0; i < transformedString.Length; ++i)
        {
            orginalString.Append(transformedStringChars[position]);
            position = positions[position];
        }

        return orginalString.ToString();
    }

    private static int[] CreateSuffixArray(string inputString)
    {
        var suffixArray = new int[inputString.Length];
        var equivalenceClasses = new int[inputString.Length];

        for (var i = 0; i < suffixArray.Length; ++i)
        {
            suffixArray[i] = i;
        }

        Array.Sort(inputString.ToCharArray(), suffixArray);

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
