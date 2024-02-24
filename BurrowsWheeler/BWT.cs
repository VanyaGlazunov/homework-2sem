using System.Text;

namespace BurrowsWheeler;

public static class BWT
{
    public static (string, int) Transform(string? inputString)
    {
        if (inputString is null) throw new ArgumentException("inputString cannot be null");

        int[] suffixArray = CreateSuffixArray(inputString);
        int endOfString = 0;
        StringBuilder transformedString = new(inputString.Length);
        for (int i = 0; i < suffixArray.Length; ++i)
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

    public static string InverseTransform(string? transformedString, int endOfString)
    {
        if (transformedString is null) throw new ArgumentException("transformedString cannot be null");
        if (endOfString >= transformedString.Length || endOfString < 0) throw new ArgumentException("endOfString index out of range");

        int[] positions = new int[transformedString.Length];
        for (int i = 0; i < positions.Length; ++i) positions[i] = i;
        char[] transformedStringChars = transformedString.ToCharArray();
        Array.Sort(transformedStringChars, positions);

        StringBuilder orginalString = new(transformedString.Length);
        int position = endOfString;
        for (int i = 0; i < transformedString.Length; ++i)
        {
            orginalString.Append(transformedStringChars[position]);
            position = positions[position];
        }

        return orginalString.ToString();
    }

    private static int[] CreateSuffixArray(string inputString)
    {
        int[] suffixArray = new int[inputString.Length];
        int[] equivalenceClasses = new int[inputString.Length];

        for (int i = 0; i < suffixArray.Length; ++i) suffixArray[i] = i;

        Array.Sort(inputString.ToCharArray(), suffixArray);

        for (int i = 1; i < suffixArray.Length; ++i)
        {
            equivalenceClasses[suffixArray[i]] = inputString[suffixArray[i]] == inputString[suffixArray[i - 1]] ?
            equivalenceClasses[suffixArray[i - 1]] : equivalenceClasses[suffixArray[i - 1]] + 1;
        }

        for (int i = 0; (1 << i) < inputString.Length; ++i)
        {
            for (int j = 0; j < suffixArray.Length; ++j)
            {
                suffixArray[j] = (suffixArray[j] - (1 << i) + suffixArray.Length) % suffixArray.Length;
            }

            suffixArray = CountSort(suffixArray, equivalenceClasses);

            int[] newEquivalenceClasses = new int[equivalenceClasses.Length];

            for (int j = 1; j < suffixArray.Length; ++j)
            {
                (int, int) previousBlock = new(equivalenceClasses[suffixArray[j - 1]], equivalenceClasses[(suffixArray[j - 1] + (1 << i)) % suffixArray.Length]);
                (int, int) currentBlock = new(equivalenceClasses[suffixArray[j]], equivalenceClasses[(suffixArray[j] + (1 << i)) % suffixArray.Length]);
                newEquivalenceClasses[suffixArray[j]] = previousBlock == currentBlock ? newEquivalenceClasses[suffixArray[j - 1]] : newEquivalenceClasses[suffixArray[j - 1]] + 1;
            }

            equivalenceClasses = newEquivalenceClasses;
        }

        return suffixArray;
    }

    private static int[] CountSort(int[] suffixArray, int[] equivalenceClasses)
    {
        int[] count = new int[suffixArray.Length];
        int[] positions = new int[suffixArray.Length];
        foreach (int element in equivalenceClasses) count[element]++;
        for (int i = 1; i < suffixArray.Length; ++i) positions[i] = positions[i - 1] + count[i - 1];
        int[] sortedArray = new int[suffixArray.Length];
        foreach (int element in suffixArray)
        {
            int elementClass = equivalenceClasses[element];
            sortedArray[positions[elementClass]] = element;
            positions[elementClass]++;
        }

        return sortedArray;
    }
}
