using Routers;
using Routers.Exceptions;

if (args.Length != 2)
{
    Console.WriteLine("Wrong number of arguments. Try to specify path to an input file then path to a outputfile.");
    return;
}

Graph maxSpanningTree;
try
{
    Graph graph = NetworkParser.ParseFromFile(args[0]);
    maxSpanningTree = graph.GetMaxSpanningTree();
    NetworkParser.WriteToFile(args[1], graph);
}
catch (InvalidOperationException e)
{
    Console.Error.WriteLine(e.Message);
    Environment.Exit(1);
}
catch (GraphIsDisconnectedException)
{
    Console.Error.WriteLine("Cannot make optimal configuration in disconnected network");
    Environment.Exit(1);
}