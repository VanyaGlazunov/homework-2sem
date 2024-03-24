namespace Trie;

/// <summary>
/// Class realizes Trie data structure for storing strings.
/// </summary>
public class Trie
{
    private TrieElement root;

    private TrieElement pointer;

    /// <summary>
    /// Initializes a new instance of the <see cref="Trie"/> class.
    /// </summary>
    public Trie()
    {
        this.root = new ();
        this.pointer = this.root;
    }

    /// <summary>
    /// Gets the number of words in trie.
    /// </summary>
    public int Size => this.root.WordsInSubtree;

    /// <summary>
    /// Gets the code of the element which is currently pointed on.
    /// </summary>
    public int PointerCode => this.pointer.Code;

    /// <summary>
    /// Moves pointer to the root.
    /// </summary>
    public void MovePointerToRoot() => this.pointer = this.root;

    /// <summary>
    /// Moves pointer to the next specified character, returns true on success.
    /// </summary>
    /// <param name="nextChar">Symbol to move to.</param>
    /// <returns>True if pointer was moved otherwise false.</returns>
    public bool MovePointerNext(char nextChar)
    {
        if (this.pointer.Next.ContainsKey(nextChar))
        {
            this.pointer = this.pointer.Next[nextChar];
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds next specified symbol to the pointed element.
    /// </summary>
    /// <param name="nextChar">Symbol to add.</param>
    /// <param name="code">Code of the symbol.</param>
    public void AddNextToPointer(char nextChar, int code)
    {
        this.pointer.WordsInSubtree++;
        this.pointer.Next[nextChar] = new TrieElement() { Code = code, IsTerminal = true };
    }

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
            vertex = vertex.Next[symbol];
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

        public int Code { get; set; }

        public int WordsInSubtree { get; set; }
    }
}
