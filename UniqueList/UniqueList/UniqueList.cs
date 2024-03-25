namespace List.UniqueList;

using Exceptions;

/// <summary>
/// Class realizes List that contains only unique elements.
/// </summary>
/// <typeparam name="T">Type of elements in the <see cref="UniqueList"/>.</typeparam>
public class UniqueList<T> : List<T>
{
    /// <summary>
    /// Gets or sets element at the specified index.
    /// </summary>
    /// <param name="index">Index of the element.</param>
    /// <returns>Element at the <paramref name="index"/>.</returns>
    /// <exception cref="ElementAlreadyExistsException">Thrown when trying to change element such that the list would contain duplicates.</exception>
    public override T this[int index]
    {
        get => base[index];
        set
        {
            if (!this.Contains(value) || (base[index]?.Equals(value) ?? (base[index] is null && value is null)))
            {
                base[index] = value;
            }
            else
            {
                throw new ElementAlreadyExistsException($"Cannot insert duplicates in {nameof(UniqueList)}");
            }
        }
    }

    /// <summary>
    /// Inserts an element into <see cref="UniqueList"/> at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="element"/> should be inserted.</param>
    /// <param name="element">The object to insert.</param>
    /// <exception cref="ElementAlreadyExistsException">Thrown when trying to insert element that is already in the <see cref="UniqueList"/>.</exception>
    public override void Insert(int index, T element)
    {
        if (this.Contains(element))
        {
            throw new ElementAlreadyExistsException($"Cannot insert duplicates in {nameof(UniqueList)}");
        }

        base.Insert(index, element);
    }
}