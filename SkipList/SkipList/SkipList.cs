namespace SkipList;

using System.Collections;

/// <summary>
/// Class realizes skip list data structure.
/// </summary>
/// <typeparam name="T">The type of elements in skip list.</typeparam>
public class SkipList<T> : IList<T>
{
    private readonly Comparer<T> comparer;
    private readonly Random random = new ();
    private int version;
    private SkipListNode bottomHead;
    private SkipListNode topHead;

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class that is empty and has the default comparer for <typeparamref name="T"/>.
    /// </summary>
    public SkipList()
    {
        bottomHead = new ();
        topHead = bottomHead;
        comparer = Comparer<T>.Default;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class that is empty and has specified comparer.
    /// </summary>
    /// <param name="comparer">Comparer to use for comparing elements.</param>
    public SkipList(Comparer<T> comparer)
    : this()
    {
        this.comparer = comparer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class that contains elements copied from the specified collection.
    /// </summary>
    /// <param name="values">The collection whoose elements are copied to the new skiplist.</param>
    public SkipList(IEnumerable<T> values)
    : this()
    {
        foreach (var value in values)
        {
            this.Add(value);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SkipList{T}"/> class that contains elements copied from the specified collection and has specified comparer.
    /// </summary>
    /// <param name="values">The collection whoose elements are copied to the new skiplist.</param>
    /// <param name="comparer">Comparer to use for comparing elements.</param>
    public SkipList(IEnumerable<T> values, Comparer<T> comparer)
    : this(values)
    {
        this.comparer = comparer;
    }

    /// <summary>
    /// Gets the number of elements in the <see cref="SkipList{T}"/>.
    /// </summary>
    public int Count { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the ICollection is read-only.
    /// </summary>
    bool ICollection<T>.IsReadOnly => false;

    /// <inheritdoc/>
    T IList<T>.this[int index]
    {
        get { throw new NotSupportedException(); }
        set { throw new NotSupportedException(); }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="SkipList{T}"/>.
    /// </summary>
    /// <returns>A <see cref="List{T}.Enumerator"/> for the <see cref="List{T}"/>.</returns>
    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>An <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.</returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Adds object to the <see cref="SkipList{T}"/>.
    /// </summary>
    /// <param name="key">The object to be added to the end of the <see cref="SkipList{T}"/>.</param>
    public void Add(T key)
    {
        var node = Add(topHead, key);
        if (node != null)
        {
            var newTopHead = new SkipListNode
            {
                Down = topHead,
                Next = new () { Key = key, Down = node },
            };
            topHead = newTopHead;
        }

        ++Count;
        ++version;
    }

    /// <inheritdoc/>
    void IList<T>.Insert(int position, T item)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Removes the first occurrecne of a specific object.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="SkipList{T}"/>.</param>
    /// <returns>true if item is successfully removed; otherwise false.</returns>
    public bool Remove(T item)
    {
        if (Remove(topHead, item))
        {
            --Count;
            ++version;
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    void IList<T>.RemoveAt(int position)
    {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Removes all elements from the <see cref="SkipList{T}"/>.
    /// </summary>
    public void Clear()
    {
        bottomHead = new ();
        topHead = bottomHead;
        ++version;
        Count = 0;
    }

    /// <summary>
    /// Determines whether an element is in the <see cref="SkipList{T}"/>.
    /// </summary>
    /// <param name="key">The object to locate in the <see cref="SkipList{T}"/>.</param>
    /// <returns>true if key is found in the <see cref="SkipList{T}"/>; false otherwise.</returns>
    public bool Contains(T key)
    {
        return Find(topHead, key) != null;
    }

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="SkipList{T}"/>.
    /// </summary>
    /// <param name="key">The object to locate in the <see cref="SkipList{T}"/>.</param>
    /// <returns>The zero-based index of the first occurrence of item within the entire <see cref="SkipList{T}"/>, if found; otherwise, -1.</returns>
    public int IndexOf(T key)
    {
        var currentNode = bottomHead;
        var index = 0;
        while (currentNode.Next != null && comparer.Compare(currentNode.Next.Key, key) < 0)
        {
            currentNode = currentNode.Next;
            index++;
        }

        return currentNode.Next != null && comparer.Compare(currentNode.Next.Key, key) == 0 ? index : -1;
    }

    /// <summary>
    /// Copies the entire <see cref="SkipList{T}"/> to a compatible one-dimensional array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination of the elements copied from <see cref="SkipList{T}"/>.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    /// <exception cref="ArgumentException">Thrown when the number of elements in the <see cref="SkipList{T}"/> is greater than the available space.</exception>
    /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when arrayIndex is greater than array length or less than zero.</exception>
    public void CopyTo(T?[] array, int arrayIndex)
    {
        ArgumentNullException.ThrowIfNull(array);

        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (arrayIndex + Count > array.Length)
        {
            throw new ArgumentException("Not enough space to copy elements");
        }

        var currentNode = bottomHead;
        while (currentNode.Next != null && arrayIndex < array.Length)
        {
            array[arrayIndex] = currentNode.Next.Key;
            currentNode = currentNode.Next;
            arrayIndex++;
        }
    }

    private bool Remove(SkipListNode node, T key)
    {
        while (node.Next != null && comparer.Compare(node.Next.Key, key) < 0)
        {
            node = node.Next;
        }

        bool result = false;
        if (node.Down != null)
        {
            result |= Remove(node.Down, key);
        }

        if (node.Next != null && comparer.Compare(node.Next.Key, key) == 0)
        {
            node.Next = node.Next.Next;
            return true;
        }

        return result;
    }

    private SkipListNode? Add(SkipListNode node, T key)
    {
        while (node.Next != null && comparer.Compare(node.Next.Key, key) < 0)
        {
            node = node.Next;
        }

        SkipListNode? downNode;
        if (node.Down == null)
        {
            downNode = null;
        }
        else
        {
            downNode = Add(node.Down, key);
        }

        if (downNode != null || node.Down == null)
        {
            node.Next = new () { Next = node.Next, Down = downNode, Key = key };
            if (random.NextDouble() < 0.5)
            {
                return node.Next;
            }
            else
            {
                return null;
            }
        }

        return null;
    }

    private SkipListNode? Find(SkipListNode node, T key)
    {
        while (node.Next != null && comparer.Compare(node.Next.Key, key) < 0)
        {
            node = node.Next;
        }

        if (node.Next != null && comparer.Compare(node.Next.Key, key) == 0)
        {
            return node;
        }
        else
        {
            return node.Down == null ? null : Find(node.Down, key);
        }
    }

    /// <summary>
    /// Enumerates the elements of the <see cref="SkipList{t}"/>.
    /// </summary>
    public struct Enumerator : IEnumerator<T>
    {
        private SkipList<T> skipList;
        private int version;
        private SkipListNode current;
        private bool isHead;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkipList{T}.Enumerator"/> struct.
        /// </summary>
        /// <param name="skiplist">SkipList to enumerate.</param>
        public Enumerator(SkipList<T> skiplist)
        {
            this.skipList = skiplist;
            version = skiplist.version;
            current = skipList.bottomHead;
            isHead = true;
        }

        /// <summary>
        /// Gets the element at the current position of the enumerator.
        /// </summary>
        public T Current
        {
            get
            {
                if (version != skipList.version)
                {
                    throw new InvalidOperationException("Invalid iterator");
                }

                if (isHead)
                {
                    throw new InvalidOperationException("Current position is before first element");
                }

                if (current.Key == null)
                {
                    throw new InvalidOperationException("Current node value is null");
                }

                return current.Key;
            }
        }

        /// <summary>
        /// Gets the element at the current position of the enumerator.
        /// </summary>
        object IEnumerator.Current => Current!;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public readonly void Dispose()
        {
        }

        /// <summary>
        /// Advances enumerator to the next element of the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <returns>true if succsessfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        /// <exception cref="InvalidOperationException">Thrown if collection was modified after creating enumerator.</exception>
        public bool MoveNext()
        {
            if (version != skipList.version)
            {
                throw new InvalidOperationException("Invalid iterator");
            }

            if (current.Next != null)
            {
                current = current.Next;
                isHead = false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the enumerator to its initial position.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if collection was modified after creating enumerator.</exception>
        public void Reset()
        {
            if (version != skipList.version)
            {
                throw new InvalidOperationException("Invalid iterator");
            }

            current = skipList.bottomHead;
            isHead = true;
        }
    }

    private class SkipListNode
    {
        public SkipListNode? Next { get; set; }

        public SkipListNode? Down { get; set; }

        public T? Key { get; set; }
    }
}
