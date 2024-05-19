namespace Routers;

using Routers.Exceptions;

/// <summary>
/// Static class that helps parsing data from file to the graph representaion and writing data represented by a graph.
/// </summary>
public static class NetworkParser
{
    /// <summary>
    /// Creates graph representation of the network that is contained in a file.
    /// </summary>
    /// <param name="filePath">Path to the file containing network.</param>
    /// <returns>Graph that represents network.</returns>
    /// <exception cref="IncorrectNetworkFormatException">Thrown when file contains wrong format of network representation.</exception>
    public static Graph ParseFromFile(string filePath)
    {
        using var streamReader = new StreamReader(File.OpenRead(filePath));
        var graph = new Graph();
        var currentLine = 1;
        while (!streamReader.EndOfStream)
        {
            var input = streamReader.ReadLine() !;
            if (!input.Contains(':'))
            {
                throw new IncorrectNetworkFormatException($"No colon on line: {currentLine}");
            }

            if (!int.TryParse(input[.. input.IndexOf(':')], out int firstVertex))
            {
                throw new IncorrectNetworkFormatException($"Cannot read first vertex on line: {currentLine}");
            }

            if (input.Length == input.IndexOf(':') + 1)
            {
                throw new IncorrectNetworkFormatException($"No new edges on line {currentLine}");
            }

            var splitted = input[(input.IndexOf(':') + 1) ..].Split(' ', ',', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < splitted.Length; i += 2)
            {
                if (!int.TryParse(splitted[i], out int secondVertex))
                {
                    throw new IncorrectNetworkFormatException($"Cannot read vertex on line {currentLine}");
                }

                if (i + 1 >= splitted.Length)
                {
                    throw new IncorrectNetworkFormatException($"No weight on the ({firstVertex} {secondVertex}) edge");
                }

                if (!splitted[i + 1].Contains('(') || !splitted[i + 1].Contains(')'))
                {
                    throw new IncorrectNetworkFormatException($"Cannot find weight for the ({firstVertex}, {secondVertex}) edge");
                }

                var indexOfLeftBrace = splitted[i + 1].IndexOf('(');
                var indexOfRightBrace = splitted[i + 1].IndexOf(')');

                if (!int.TryParse(splitted[i + 1][(indexOfLeftBrace + 1) .. indexOfRightBrace], out int weight))
                {
                    throw new IncorrectNetworkFormatException($"Cannot read weight on the ({firstVertex} {secondVertex}) edge");
                }

                graph.AddEdge(firstVertex, secondVertex, weight);
            }
        }

        return graph;
    }

    /// <summary>
    /// Writes graph representaion of the network to the specified file.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="network">Graph that represents network.</param>
    public static void WriteToFile(string filePath, Graph network)
    {
        using var streamWriter = new StreamWriter(File.OpenWrite(filePath));
        foreach (var (vertex, neighbours) in network.AdjacencyLists)
        {
            var currentLine = $"{vertex}: ";
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