using Routers;

if (args.Length != 2)
{
    return;
}

Graph graph = new();

using (StreamReader streamReader = new (File.OpenRead(args[0])))
{
    while (!streamReader.EndOfStream)
    {
        var input = streamReader.ReadLine();
        var splitted = input!.Split(':', '(', ')', ' ');
        for (int i = 1; i + 1 < splitted.Length; i += 2)
        {
            graph.AddEdge(int.Parse(splitted[0]), int.Parse(splitted[i]), int.Parse(splitted[i + 1]));
        }
    }
}

Graph MaxSpanningTree;
try
{
    MaxSpanningTree = graph.GetMaxSpanningTree();
}
catch (InvalidOperationException)
{
    Console.Error.WriteLine("Network is disconnected."); 
    throw;
}

using (var outputStream = File.OpenWrite(args[1]))
{
    MaxSpanningTree.Print(outputStream);
}