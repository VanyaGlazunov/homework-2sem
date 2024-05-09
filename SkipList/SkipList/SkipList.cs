using System.Collections;

namespace SkipList;

public class SkipList<T> : IList<T>
{
    private readonly Comparer<T> comparer;
    private readonly Random random = new ();
    private int version;
    private SkipListNode bottomHead;
    private SkipListNode topHead;

    public bool IsEmpty => this.Count == 0;

    public int Count { get; private set; }

    public bool IsReadOnly => false;

    public SkipList()
    {
        bottomHead = new ();
        topHead = this.bottomHead;
        comparer = Comparer<T>.Default;
    }

    public SkipList(Comparer<T> comparer)
    : this()
    {
        this.comparer = comparer;
    }

    public SkipList(IEnumerable<T> values)
    : this()
    {
        var currentNode = topHead;
        foreach (var value in values)
        {
            currentNode.Next = new () { Key = value };
            currentNode = currentNode.Next;
            ++Count;
        }

        for (int level = 2; level <= Count; level <<= 1)
        {
            var nextLevelHead = new SkipListNode() { Down = topHead };
            var nextLevelCurrentNode = nextLevelHead;
            for (currentNode = topHead.Next; currentNode != null && currentNode.Next != null; currentNode = currentNode.Next.Next)
            {
                nextLevelCurrentNode.Next = new () { Key = currentNode.Key, Down = currentNode };
                nextLevelCurrentNode = nextLevelCurrentNode.Next;
            }
            topHead = nextLevelHead;
        }
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    public T this[int index]
    {
        get { throw new NotSupportedException(); }
        set { throw new NotSupportedException(); }
    }

    public void RemoveAt(int position)
    {
        throw new NotSupportedException();
    }

    public void Insert(int position, T item)
    {
        throw new NotSupportedException();
    }

    public void CopyTo(T?[] array, int arrayIndex)
    {
        if (array != null && array.Rank != 1)
        {
            throw new ArgumentException();
        }

        if (array == null)
        {
            throw new ArgumentNullException(nameof(array));
        }

        if (bottomHead == null)
        {
            return;
        }

        if (arrayIndex < 0 || arrayIndex >= array.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        }

        if (arrayIndex + Count >= array.Length)
        {
            throw new ArgumentException();
        }

        var currentNode = bottomHead;
        while (currentNode.Next != null && arrayIndex < array.Length)
        {
            array[arrayIndex] = currentNode.Next.Key;
            currentNode = currentNode.Next;
        }
    }

    public int IndexOf(T key)
    {
        if (bottomHead == null)
        {
            throw new InvalidOperationException();
        }

        var currentNode = bottomHead;
        int index = 0;
        while (currentNode.Next != null && comparer.Compare(currentNode.Next.Key, key) < 0)
        {
            currentNode = currentNode.Next;
            index++;
        }

        if (currentNode.Next != null && comparer.Compare(currentNode.Next.Key, key) == 0)
        {
            return index - 1;
        }
        else
        {
            throw new InvalidOperationException();
        }

    }

    public void Clear()
    {
        bottomHead = new ();
        topHead = bottomHead;
    }

    public bool Contains(T key)
    {
        return Find(topHead, key) != null;
    }

    public void Add(T key)
    {
        var node = Add(topHead, key);
        if (node != null)
        {
            var newTopHead = new SkipListNode() { Key = key, Down = topHead };
            topHead = newTopHead;
        }

        ++Count;
    }

    public bool Remove(T key)
    {
        if (Remove(topHead, key))
        {
            --Count;
            return true;
        }

        return false;
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

    public struct Enumerator : IEnumerator<T>
    {
        private SkipList<T> skipList;
        private int version;
        private SkipListNode? current;
        private bool isHead;

        public Enumerator(SkipList<T> skiplist)
        {
            this.skipList = skiplist;
            version = skiplist.version;
            current = skipList.bottomHead;
            isHead = true;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (version != skipList.version)
            {
                throw new InvalidOperationException("Invalidated iterator");
            }

            if (current != null) {
                current = current.Next;
                isHead = false;
                return true;
            }

            return false;
        }

        public T Current => current.Key;

        object IEnumerator.Current => Current;

        public void Reset()
        {
            if (version != skipList.version)
            {
                throw new InvalidOperationException();
            }

            current = skipList.bottomHead;
        }
    }

    private class SkipListNode
    {
        public SkipListNode? Next { get; set; }

        public SkipListNode? Down { get; set; }

        public T? Key { get; set; }
    }
}
