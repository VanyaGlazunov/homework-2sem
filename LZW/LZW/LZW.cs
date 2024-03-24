namespace LZW;

using BWT;

/// <summary>
/// Class realizes LZW data compression algorithm.
/// </summary>
public static class LZW
{
    private const int EndOfDataSignal = 256;
    private const int StartCodeSize = 9;
    private const int StartCode = 257;

    /// <summary>
    /// Method compress data from <paramref name="bytesToCompress"/> using BWT Transfrom.
    /// </summary>
    /// <param name="bytesToCompress">Byte array to compress.</param>
    /// <returns>Byte array with compressed data.</returns>
    public static byte[] CompressWithBwt(byte[] bytesToCompress)
    {
        var (transformedBytesToCompress, endPosition) = BWT.Transform(bytesToCompress);

        var endPositionBytes = BitConverter.GetBytes(endPosition);
        var transformedBytes = transformedBytesToCompress.ToList();
        transformedBytes.AddRange(endPositionBytes);
        return Compress([..transformedBytes]);
    }

    /// <summary>
    /// Method decompresses data from <paramref name="bytesToDecompress"/> using BWT Transform.
    /// </summary>
    /// <param name="bytesToDecompress">Byte array to compress.</param>
    /// <returns>Byte array with decompressed data.</returns>
    public static byte[] DecompressWithBWT(byte[] bytesToDecompress)
    {
        var decompressed = Decompress(bytesToDecompress);
        var endPositionBytes = new[] { decompressed[^4], decompressed[^3], decompressed[^2], decompressed[^1] };
        var endPosition = BitConverter.ToInt32(endPositionBytes);

        return BWT.InverseTransform(decompressed[..^4], endPosition);
    }

    /// <summary>
    /// Method compresses data from <paramref name="bytesToCompress"/>.
    /// </summary>
    /// <param name="bytesToCompress">Byte array to compress.</param>
    /// <returns>Byte array with compressed data.</returns>
    public static byte[] Compress(byte[] bytesToCompress)
    {
        Trie.Trie trie = new ();
        for (var b = 0; b <= byte.MaxValue; ++b)
        {
            trie.AddNextToPointer((char)b, b);
        }

        CompressedSequence compressedSequence = new ();
        trie.MovePointerNext((char)bytesToCompress[0]);
        var newCode = StartCode;
        var currentCodeSize = StartCodeSize;
        for (var i = 1; i < bytesToCompress.Length; ++i)
        {
            var nextByte = bytesToCompress[i];
            if (!trie.MovePointerNext((char)nextByte))
            {
                compressedSequence.Add(trie.PointerCode, currentCodeSize);
                trie.AddNextToPointer((char)nextByte, newCode);
                trie.MovePointerToRoot();
                trie.MovePointerNext((char)nextByte);
                currentCodeSize += newCode >= (1 << currentCodeSize) ? 1 : 0;
                newCode++;
            }
        }

        compressedSequence.Add(trie.PointerCode, currentCodeSize);
        compressedSequence.Add(EndOfDataSignal, currentCodeSize);

        return compressedSequence.Sequence;
    }

    /// <summary>
    /// Method decompresses data from <paramref name="bytesToDecompress"/>.
    /// </summary>
    /// <param name="bytesToDecompress">Byte array from compressed file.</param>
    /// <returns>Byte array with decompressed data.</returns>
    public static byte[] Decompress(byte[] bytesToDecompress)
    {
        Dictionary<int, List<byte>> dictionary = new ();
        List<byte> decompressed = new ();
        for (var i = 0; i <= byte.MaxValue; ++i)
        {
            dictionary[i] = new () { (byte)i };
        }

        CompressedSequence compressedSequence = new () { Sequence = bytesToDecompress };
        var currentCodeSize = StartCodeSize;
        var newCode = StartCode;
        var (currentCode, isEndOfData) = compressedSequence.GetNextCode(currentCodeSize);
        decompressed.Add((byte)currentCode);
        (var nextCode, isEndOfData) = compressedSequence.GetNextCode(currentCodeSize);
        while (!isEndOfData)
        {
            if (nextCode == newCode)
            {
                dictionary[nextCode] = [.. dictionary[currentCode], dictionary[currentCode][0]];
                decompressed.AddRange(dictionary[nextCode]);
            }
            else
            {
                decompressed.AddRange(dictionary[nextCode]);
                dictionary[newCode] = [..dictionary[currentCode], dictionary[nextCode][0]];
            }

            newCode++;
            currentCodeSize += newCode >= (1 << currentCodeSize) ? 1 : 0;

            currentCode = nextCode;
            (nextCode, isEndOfData) = compressedSequence.GetNextCode(currentCodeSize);
        }

        return [..decompressed];
    }

    private class CompressedSequence
    {
        private List<byte> codes = new ();

        private int firstFreeBit = 0;
        private int lastReadBit = 0;

        public byte[] Sequence
        {
            get => [.. this.codes];
            set
            {
                this.codes = [.. value];
                this.firstFreeBit = (value.Length * 8) + 1;
            }
        }

        public void Clear()
        {
            this.firstFreeBit = 0;
            this.lastReadBit = 0;
        }

        public void Add(int code, int codeSize)
        {
            var indexOfByte = this.firstFreeBit / 8;
            var indexOfFreeBit = this.firstFreeBit % 8;
            for (var i = 0; i < codeSize; ++i)
            {
                if (this.codes.Count <= indexOfByte)
                {
                    this.codes.Add(0);
                }

                if ((1 & (code >> i)) == 1)
                {
                    this.codes[indexOfByte] |= (byte)(1 << indexOfFreeBit);
                }

                this.firstFreeBit++;
                indexOfByte += indexOfFreeBit + 1 > 7 ? 1 : 0;
                indexOfFreeBit = (indexOfFreeBit + 1) % 8;
            }
        }

        public (int Code, bool IsEndOfData) GetNextCode(int codeSize)
        {
            var currentBitIndex = this.lastReadBit % 8;
            var currentByteIndex = this.lastReadBit / 8;
            var code = 0;
            for (var i = 0; i < codeSize; ++i)
            {
                if ((1 & (this.codes[currentByteIndex] >> currentBitIndex)) == 1)
                {
                    code |= 1 << i;
                }

                this.lastReadBit++;
                currentByteIndex += currentBitIndex + 1 > 7 ? 1 : 0;
                currentBitIndex = (currentBitIndex + 1) % 8;
            }

            return (code, code == EndOfDataSignal);
        }
    }
}
