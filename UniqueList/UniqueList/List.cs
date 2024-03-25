namespace List;

using List.UniqueList.Exceptions;

/// <summary>
/// Class realizes generic List.
/// </summary>
/// <typeparam name="T">Type of elements in the <see cref="List"/>.</typeparam>
public class List<T>
{
    private ListNode? head;

    /// <summary>
    /// Gets the number of elements contained in the <see cref="List"/>.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Gets or sets element at the specified index.
    /// </summary>
    /// <param name="index">Index of the element.</param>
    /// <returns>The element at the <paramref name="index"/>.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown when index is less than zero or greater or equal to the current <see cref="Length"/>.</exception>
    public virtual T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Length)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            var pointer = this.head;
            for (int i = 0; i < index - 1; ++i)
            {
                pointer = pointer!.Next;
            }

            return pointer!.Value;
        }

        set
        {
            if (index < 0 || index >= this.Length)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }

            var pointer = this.head;
            for (int i = 0; i < index - 1; ++i)
            {
                pointer = pointer!.Next;
            }

            pointer!.Value = value;
        }
    }

    /// <summary>
    /// Returns a value indicating whether the specified element occurs in the <see cref="List"/>.
    /// </summary>
    /// <param name="element">The element to seek.</param>
    /// <returns><see cref="true"/> if <paramref name="element"/> parameter occurs in the <see cref="List"/>; otherwise <see cref="false"/>.</returns>
    public bool Contains(T element)
    {
        var pointer = this.head;
        for (int i = 0; i < this.Length; ++i)
        {
            if (pointer!.Value?.Equals(element) ?? (element is null && pointer!.Value is null))
            {
                return true;
            }

            pointer = pointer.Next;
        }

        return false;
    }

    /// <summary>
    /// Inserts an element into the <see cref="List"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="value"/> should be inserted.</param>
    /// <param name="value">The object to insert.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown when <paramref name="index"/> is less than 0 or greater than <see cref="Length"/>.</exception>
    public virtual void Insert(int index, T value)
    {
        if (index < 0 || index > this.Length)
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        if (index == 0)
        {
            this.head = new (value, this.head);
            return;
        }

        var pointer = this.head;
        for (int i = 0; i < index - 1; ++i)
        {
            pointer = pointer!.Next;
        }

        ListNode newElement = new (value, pointer!.Next);
        pointer.Next = newElement;
        ++this.Length;
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="List"/>.
    /// </summary>
    /// <param name="element">The object to remove from the <see cref="List"/>.</param>
    /// <exception cref="ElementNotFoundException">Thrown when <paramref name="element"/> not found in the <see cref="List"/>.</exception>
    public void Remove(T element)
    {
        if (this.head!.Value?.Equals(element) ?? (element is null && this.head!.Value is null))
        {
            this.head = this.head.Next;
        }

        var pointer = this.head;
        ListNode? previousPointer = null;
        for (int i = 0; i < this.Length; ++i)
        {
            if (pointer!.Value?.Equals(element) ?? (pointer!.Value is null && element is null))
            {
                previousPointer!.Next = pointer.Next;
                return;
            }

            previousPointer = pointer;
            pointer = pointer.Next;
        }

        throw new ElementNotFoundException($"{nameof(element)} not found");
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="List"/>.
    /// </summary>
    /// <param name="index">The zero-based index of the element to remove.</param>
    /// <exception cref="IndexOutOfRangeException">Thrown when index is less than zero or greater or equal to <see cref="Length"/>.</exception>
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= this.Length)
        {
            throw new IndexOutOfRangeException(nameof(index));
        }

        if (index == 0)
        {
            this.head = this.head!.Next;
            return;
        }

        var pointer = this.head;
        for (int i = 0; i < index - 1; ++i)
        {
            pointer = pointer!.Next;
        }

        pointer!.Next = pointer!.Next!.Next;
        --this.Length;
    }

    private class ListNode(T value, ListNode? next)
    {
        public T Value { get; set; } = value;

        public ListNode? Next { get; set; } = next;
    }
}