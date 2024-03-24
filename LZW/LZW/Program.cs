if (args.Length != 2)
{
    Console.WriteLine($"Wrong number of arguments. Please try to specify [path] and [-mode].");
}

var bytesFromFile = File.ReadAllBytes(args[0]);

if (bytesFromFile.Length == 0)
{
    Console.WriteLine("Cannot work with empty file");
    return;
}

switch (args[1])
{
    case "-c":
        var compressedFilePath = $"./{Path.GetFileName(args[0])}.zipped";
        var compressedWithoutBWT = LZW.LZW.Compress(bytesFromFile);
        var compressedWithBWT = LZW.LZW.CompressWithBwt(bytesFromFile);
        Console.WriteLine($"Compression ratio without BWT: {1.0 * bytesFromFile.Length / compressedWithoutBWT.Length}");
        Console.WriteLine($"Compression ratio with BWT: {1.0 * bytesFromFile.Length / compressedWithBWT.Length}");
        File.WriteAllBytes(compressedFilePath, compressedWithBWT);
        break;
    case "-u":
        var decompressedFilePath = $"./{Path.GetFileName(args[0])}";
        if (!decompressedFilePath.EndsWith(".zipped"))
        {
            Console.WriteLine("Input file has wrong extension");
            break;
        }

        decompressedFilePath = decompressedFilePath.Remove(decompressedFilePath.LastIndexOf('.'));
        File.WriteAllBytes(decompressedFilePath, LZW.LZW.DecompressWithBWT(bytesFromFile));
        break;
    default:
        Console.WriteLine("Incorrect mode.");
        break;
}
