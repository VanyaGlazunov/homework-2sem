using StackCalculator;

Console.WriteLine("Enter expression in reverse polish notation");

string? expression = Console.ReadLine();

var resultWithListStack = new StackCalculator.StackCalculator(new ListStack()).Calculate(expression);
var resultWithLinkedListStack = new StackCalculator.StackCalculator(new LinkedListStack()).Calculate(expression);

Console.WriteLine($"result with ListStack: {resultWithListStack}");
Console.WriteLine($"result with LinkedListStack: {resultWithLinkedListStack}");
