using StackCalculator;

Console.WriteLine("Enter expression in reverse polish notation");

var expression = Console.ReadLine();

if (expression is null)
{
    Console.WriteLine("Incorrect input");
    return;
}

var (resultWithListStack, isCorrectListStack) = new StackCalculator.StackCalculator(new ListStack()).Calculate(expression);
var (resultWithLinkedListStack, isCorrectLinkedListStack) = new StackCalculator.StackCalculator(new ArrayListStack()).Calculate(expression);

if (!isCorrectListStack || !isCorrectLinkedListStack)
{
    Console.WriteLine("Incorrect input");
    return;
}

Console.WriteLine($"Result with ListStack: {resultWithListStack}");
Console.WriteLine($"Result with LinkedListStack: {resultWithLinkedListStack}");
