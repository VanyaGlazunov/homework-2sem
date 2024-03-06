bool quit = false;
Trie.Trie trie = new ();
while (!quit)
{
    Console.WriteLine("""
                        Choose comand:
                        1. Add element.
                        2. Remove element
                        3. Look up element.
                        4. Count how many elements with given prefix.
                        5. Get size.
                        6. Quit.
                        """);

    var comand = Console.ReadLine();
    string? element = " ";
    if (comand is "1" or "2" or "3" or "4")
    {
        Console.WriteLine("Enter string");
        element = Console.ReadLine();
        if (element is null)
        {
            Console.WriteLine("Incorrect input");
            return;
        }
    }

    switch (comand)
    {
        case "1":
            Console.WriteLine("Enter string");
            Console.WriteLine(trie.Add(element) ? $"Element: \"{element}\" has been added" : $"Element: \"{element}\" already exists");
            break;
        case "2":
            Console.WriteLine("Enter string");
            Console.WriteLine(trie.Remove(element) ? $"Removed element: \"{element}\"" : $"Element: \"{element}\" not found");
            break;
        case "3":
            Console.WriteLine("Enter string");
            Console.WriteLine(trie.Contains(element) ? $"Found element: \"{element}\"" : $"Element: \"{element}\" not found");
            break;
        case "4":
            Console.WriteLine("Enter prefix");
            Console.WriteLine($"{trie.HowManyStringsStartsWithPrefix(element)} word(s) starting with prefix \"{element}\"");
            break;
        case "5":
            Console.WriteLine($"The size is {trie.Size} words");
            break;
        case "6":
            quit = true;
            break;
        default:
            Console.WriteLine("incorrect comand");
            break;
    }
}