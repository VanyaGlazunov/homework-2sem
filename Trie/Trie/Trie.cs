namespace Trie;

/// <summary>
/// Class realizes Trie data structure for storing strings.
/// </summary>
public class Trie
{
    private readonly TrieElement root;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class.
    /// </summary>
    public Trie() => this.root = new ();

    /// <summary>
    /// Gets the number of words in trie.
    /// </summary>
    public int Size => this.root.WordsInSubtree;

    /// <summary>
    /// Adds an element and returns true if the element already existed.
    /// </summary>
    /// <param name="element">Element to add.</param>
    /// <returns>Bool flag indicating whether <paramref name="element"/> already existed.</returns>
    public bool Add(string element)
    {
        if (this.Contains(element))
        {
            return false;
        }

        var vertex = this.root;
        int index = 0;
        ++vertex.WordsInSubtree;
        for (; index < element.Length && vertex.Next.ContainsKey(element[index]); ++index)
        {
            vertex = vertex.Next[element[index]];
            ++vertex.WordsInSubtree;
        }

        for (; index < element.Length; ++index)
        {
            vertex.Next[element[index]] = new ();
            vertex = vertex.Next[element[index]];
            ++vertex.WordsInSubtree;
        }

        vertex.IsTerminal = true;
        return true;
    }

    /// <summary>
    /// Looks up if a given element exists.
    /// </summary>
    /// <param name="element">Element to look up.</param>
    /// <returns>Bool flag indicating whether the <paramref name="element"/> exists.</returns>
    public bool Contains(string element)
    {
        var vertex = this.root;
        foreach (var symbol in element)
        {
            if (!vertex.Next.TryGetValue(symbol, out vertex))
            {
                return false;
            }
        }

        return vertex.IsTerminal;
    }

    /// <summary>
    /// Removes given element if it existed.
    /// </summary>
    /// <param name="element">Element to remove.</param>
    /// <returns>Bool indicating whether <paramref name="element"/> existed.</returns>
    public bool Remove(string element)
    {
        if (!this.Contains(element))
        {
            return false;
        }

        var vertex = this.root;
        --vertex.WordsInSubtree;
        foreach (var symbol in element)
        {
            if (!vertex.Next.TryGetValue(symbol, out vertex))
            {
                return false;
            }

            --vertex.WordsInSubtree;
        }

        vertex.IsTerminal = false;
        return true;
    }

    /// <summary>
    /// Counts strings that starts with given prefix.
    /// </summary>
    /// <param name="prefix">Prefix to look up.</param>
    /// <returns>Number of strings that starts with <paramref name="prefix"/>.</returns>
    public int HowManyStringsStartsWithPrefix(string prefix)
    {
        var vertex = this.root;
        foreach (var symbol in prefix)
        {
            if (!vertex.Next.TryGetValue(symbol, out vertex))
            {
                return 0;
            }
        }

        return vertex.WordsInSubtree;
    }

    private class TrieElement
    {
        public TrieElement() => this.Next = new ();

        public Dictionary<char, TrieElement> Next { get; set; }

        public bool IsTerminal { get; set; }

        public int WordsInSubtree { get; set; }
    }
}
