namespace LZW.Tests;

public class Tests
{

    [TestCase("../../../ShakespeareBig.txt")]
    [TestCase("../../../cmatrix")]
    [TestCase("../../../git-cheat-sheet-education.pdf")]
    [TestCase("../../../cat.png")]
    [Test]
    public void CompressDecompress_DifferentFiles_DataIsNotCorrupted(string filePath)
    {
        var expectedFile = File.ReadAllBytes(filePath);
        var actualFile = LZW.Decompress(LZW.Compress(expectedFile));

        CollectionAssert.AreEqual(expectedFile, actualFile);
    }

    [TestCase("../../../ShakespeareBig.txt")]
    [TestCase("../../../cmatrix")]
    [TestCase("../../../git-cheat-sheet-education.pdf")]
    [TestCase("../../../cat.png")]
    [Test]
    public void CompressDecompressWithBWT_DifferentFiles_DataIsNotCorrupted(string filePath)
    {
        var expectedFile = File.ReadAllBytes(filePath);
        var actualFile = LZW.DecompressWithBWT(LZW.CompressWithBwt(expectedFile));

        CollectionAssert.AreEqual(expectedFile, actualFile);
    }


}