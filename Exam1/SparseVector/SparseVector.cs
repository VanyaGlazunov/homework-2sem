namespace SparseVector;

public class SparseVector
{
    private List<(int index, int value)> coordinates;

    public int Size { get; private set; }

    public bool IsNullVector { get => this.coordinates.Count == 0; }

    public SparseVector(int size)
    {
        this.Size = size;
        coordinates = new ();
    }

    public SparseVector(List<(int index, int value)> coordinates)
    {
        int max = -1;
        foreach (var (index, value) in coordinates)
        {
            max = Math.Max(max, index);
        }
        this.Size = max + 1;
        this.coordinates = new (Size);
        foreach (var (index, value) in coordinates)
        {
            this[index] = value;
        }
    }

    public int this[int index]
    {
        get
        {
            if (index < 0 || index > this.Size)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            foreach (var (position, value) in this.coordinates)
            {
                if (position == index) return value;
            }

            return 0; 
        }
        set
        {
            if (index < 0 || index > this.Size)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            for (int i = 0; i < coordinates.Count; ++i)
            {
                if (this.coordinates[i].index == index)
                {
                    if (value == 0)
                    {
                        this.coordinates.RemoveAt(i);
                    }
                    else
                    {
                        this.coordinates[i] = (index, value);
                    }
                }
            }

            if (value == 0)
            {
                return;
            }

            this.coordinates.Add((index, value));
        }
    }

    public SparseVector Add(SparseVector other)
    {
        if (this.Size != other.Size)
        {
            throw new InvalidOperationException();
        }

        for (int i = 0; i < this.Size; ++i)
        {
            this[i] = this[i] + other[i];
        }
        
        return this;
    }

    public SparseVector Subtract(SparseVector other)
    {
        if (this.Size != other.Size)
        {
            throw new InvalidOperationException();
        }

        for (int i = 0; i < this.Size; ++i)
        {
            this[i] -= other[i];
        }

        return this;
    }

    public int ScalarProduct(SparseVector other)
    {
        if (this.Size != other.Size)
        {
            throw new InvalidOperationException();
        }
        
        int result = 0;
        for (int i = 0; i < this.Size; ++i)
        {
            result += this[i] * other[i];
        }

        return result;
    }
}
