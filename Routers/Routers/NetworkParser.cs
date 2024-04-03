namespace Routers;

using Routers.Exceptions;

public static class NetworkParser
{
    public static Graph ParseFromFile(string filePath)
    {
        using StreamReader streamReader = new (File.OpenRead(filePath));
        Graph graph = new ();
        int currentLine = 1;
        while (!streamReader.EndOfStream)
        {
            var input = streamReader.ReadLine() !;
            if (!input.Contains(':'))
            {
                throw new IncorrectNetworkFormatExceptionException($"No colon on line: {currentLine}");
            }

            if (!int.TryParse(input[.. input.IndexOf(':')], out int firstVertex))
            {
                throw new IncorrectNetworkFormatExceptionException($"Cannot read first vertex on line: {currentLine}");
            }

            if (input.Length == input.IndexOf(':') + 1)
            {
                throw new IncorrectNetworkFormatExceptionException($"No new edges on line {currentLine}");
            }

            var splitted = input[(input.IndexOf(':') + 1) ..].Split(' ', ',', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitted.Length; i += 2)
            {
                if (!int.TryParse(splitted[i], out int secondVertex))
                {
                    throw new IncorrectNetworkFormatExceptionException($"Cannot read vertex on line {currentLine}");
                }

                if (i + 1 >= splitted.Length)
                {
                    throw new IncorrectNetworkFormatExceptionException($"No weight on the ({firstVertex} {secondVertex}) edge");
                }

                if (!splitted[i + 1].Contains('(') || !splitted[i + 1].Contains(')'))
                {
                    throw new IncorrectNetworkFormatExceptionException($"Cannot find weight for the ({firstVertex}, {secondVertex}) edge");
                }

                var indexOfLeftBrace = splitted[i + 1].IndexOf('(');
                var indexOfRightBrace = splitted[i + 1].IndexOf(')');

                if (!int.TryParse(splitted[i + 1][(indexOfLeftBrace + 1) .. indexOfRightBrace], out int weight))
                {
                    throw new IncorrectNetworkFormatExceptionException($"Cannot read weight on the ({firstVertex} {secondVertex}) edge");
                }

                graph.AddEdge(firstVertex, secondVertex, weight);
            }
        }

        return graph;
    }

    public static void WriteToFile(string filePath, Graph network)
    {
        using StreamWriter streamWriter = new (File.OpenWrite(filePath));
        foreach (var (vertex, neighbours) in network.AdjacencyLists)
        {
            string currentLine = $"{vertex}: ";
            foreach (var (neighbour, weight) in neighbours)
            {
                if (vertex < neighbour)
                {
                    currentLine += $"{neighbour} ({weight}), ";
                }
            }

            if (currentLine[^2] == ':')
            {
                continue;
            }

            streamWriter.WriteLine(currentLine[..^2]);
        }
    }
}