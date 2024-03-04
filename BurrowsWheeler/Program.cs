using BurrowsWheeler;

bool TestBWT(string input, (string, int) output)
{
    return BWT.Transform(input) == output;
}

bool TestInverseBWT((string, int) input, string output)
{
    return BWT.InverseTransform(input.Item1, input.Item2) == output;
}

var originalStrings = new[] { "BANANA", "A", "AA BB$" };
var transform = new[] { ("NNBAAA", 3), ("A", 0), ("ABA$B ", 3) };
var testsPassed = true;
for (var test = 0; test < 3; ++test)
{
    if (!TestBWT(originalStrings[test], transform[test]))
    {
        Console.WriteLine($"Burrows-Wheeler Transform failed with string: {originalStrings[test]}");
        testsPassed = false;
    }

    if (!TestInverseBWT(transform[test], originalStrings[test]))
    {
        Console.WriteLine($"Inverse Burrows-Wheeler Transform failed with: {transform[test].Item1}, {transform[test].Item2}");
        testsPassed = false;
    }
}

if (!testsPassed)
{
    return;
}

Console.WriteLine("All tests passed");

var quit = false;

while (!quit)
{
    Console.WriteLine("choose comand:\n1. Burrows-Wheeler Transform\n2. Inverse Burrows-Wheeler Transform\n3. Quit");
    var comand = Console.ReadLine();
    switch (comand)
    {
        case "1":
            Console.WriteLine("Enter string");
            var inputString = Console.ReadLine();
            var (tranformedString, endOfStringPosition) = BWT.Transform(inputString);
            Console.WriteLine($"Transformed string: {tranformedString}, the end of the string position(zero-based): {endOfStringPosition}");
            break;
        case "2":
            Console.WriteLine("Enter string and the end of the string position(zero-based)");
            inputString = Console.ReadLine();
            if (inputString is null)
            {
                Console.WriteLine("Incorrect input");
                break;
            }

            var arguments = inputString.Split();

            if (arguments.Length != 2 || !int.TryParse(arguments[1], out int endOfString))
            {
                Console.WriteLine("Incorrect input");
                break;
            }

            Console.WriteLine($"Original string: {BWT.InverseTransform(arguments[0], endOfString)}");
            break;
        case "3":
            quit = true;
            break;
        default:
            Console.WriteLine("Incorrect comand");
            break;
    }
}
