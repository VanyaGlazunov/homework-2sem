Console.WriteLine("Enter path to the file containing expression");

var filePath = Console.ReadLine();

if (filePath is null)
{
    Console.WriteLine($"filePath cannot be null");
    return;
}

var expression = File.ReadAllText(filePath);

ParseTree.ParseTree parseTree = new ();
parseTree.Build(expression);

Console.WriteLine($"{parseTree.Print()} = {parseTree.Evaluate()}");