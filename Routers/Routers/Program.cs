using Routers;
using Routers.Exceptions;

if (args.Length != 2)
{
    Console.WriteLine("Wrong number of arguments. Try to specify path to an input file then path to a outputfile.");
    return;
}

Graph graph = NetworkParser.ParseFromFile(args[0]);

Graph maxSpanningTree;
try
{
    maxSpanningTree = graph.GetMaxSpanningTree();
}
catch (InvalidOperationException e)
{
    Console.Error.WriteLine(e.Message);
    throw;
}
catch (GraphIsDisconnectedExpceptionException)
{
    Console.Error.WriteLine("Cannot make optimal configuration in disconnected network");
    throw;
}

NetworkParser.WriteToFile(args[1], graph);