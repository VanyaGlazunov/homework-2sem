using StackCalculator;

Console.WriteLine("Enter expression in reverse polish notation");

var expression = Console.ReadLine();

if (expression is null)
{
    Console.WriteLine("Incorrect input");
    return;
}

var (resultWithListStack, isCorrectListStack) = new StackCalculator.StackCalculator(new ListStack()).Calculate(expression);
var (resultWithLinkedListStack, isCorrectLinkedListStack) = new StackCalculator.StackCalculator(new LinkedListStack()).Calculate(expression);

if (!isCorrectListStack || !isCorrectLinkedListStack)
{
    Console.WriteLine("Incorrect input");
    return;
}

Console.WriteLine($"result with ListStack: {resultWithListStack}");
Console.WriteLine($"result with LinkedListStack: {resultWithLinkedListStack}");
