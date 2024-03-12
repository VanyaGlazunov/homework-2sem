using System.Linq.Expressions;

namespace Trie.Tests;

public class TrieTests
{
    Trie trie;
    
    [SetUp]
    public void Setup()
    {
        trie = new();
    }

    [Test]
    public void Add_ThreeWords_ContainsAddedWords()
    {
        var words = new [] {"abacaba", "@#$ ^&*(--)", "-120849218"};
        foreach (var word in words)
        {
            trie.Add(word);
        }
        
        foreach (var word in words)
        {
            Assert.That(trie.Contains(word) is true);
        }
    }

    [Test]
    public void Size_AddThreeWords_SizeReturnsThree()
    {
        var words = new [] {"abacaba", "@#$ ^&*(--)", "-120849218"};
        foreach (var word in words)
        {
            trie.Add(word);
        }
        
        Assert.That(trie.Size is 3);
    }

    [Test]
    public void Add_SameWord_AddReturnsFalseAndSizeIsOne()
    {
        trie.Add("abacaba");
        Assert.That(trie.Add("abacaba") is false);
        Assert.That(trie.Size is 1);
    }

    [Test]
    public void AddRemove_ThreeWords_RemoveReturnsTrueAndTrieIsEmpty()
    {
        var words = new [] {"abacaba", "@#$ ^&*(--)", "-120849218"};
        foreach (var word in words)
        {
            trie.Add(word);
        }

        foreach (var word in words)
        {
            Assert.That(trie.Remove(word) is true);
        }

        Assert.That(trie.Size is 0);
    }

    [Test]
    public void Remove_EmptyTrie_RemoveReturnsFalse()
    {
        var words = new [] {"abacaba", "@#$ ^&*(--)", "-120849218"};

        foreach (var word in words)
        {
            Assert.That(trie.Remove(word) is false);
        }
    }

    [Test]
    public void Add_EmptyString_ContainsEmptyString()
    {
        trie.Add("");
        Assert.That(trie.Contains("") is true);
    }

    [Test]
    public void HowManyStartsWithPrefix_NoWordsShareSamePrefix_ReturnsOne()
    {
        var words = new [] {"abacaba", "@#$ ^&*(--)", "-120849218"};
        foreach (var word in words)
        {
            trie.Add(word);
        }

        Assert.That(trie.HowManyStringsStartsWithPrefix("ab") is 1);
        Assert.That(trie.HowManyStringsStartsWithPrefix("@") is 1);
        Assert.That(trie.HowManyStringsStartsWithPrefix("-1") is 1);
    }

    [Test]
    public void HowManyStartsWithPrefix_SomeWordsShareSamePrefix_ReturnsCorrectResult()
    {
        var words = new [] {"abacaba", "aba", "bbb", "abcde", "lkj"};
        foreach (var word in words)
        {
            trie.Add(word);
        }

        Assert.That(trie.HowManyStringsStartsWithPrefix("ab") is 3);
        Assert.That(trie.HowManyStringsStartsWithPrefix("bb") is 1);
    }

    [Test]
    public void Contains_DeletePrefix_StringContaningThePrefixStillInTrie()
    {
        var words = new [] {"abacaba", "bbb",  "-12393"};
        var prefixes = new [] {"aba", "b", "-1239"};
        foreach (var prefix in prefixes)
        {
            trie.Add(prefix);
        }
        foreach (var word in words)
        {
            trie.Add(word);
        }
        
        for (int i = 0; i < 3; ++i)
        {
            trie.Remove(prefixes[i]);
            Assert.That(trie.Contains(words[i]) is true);
        }
    }
}