using System.IO;

namespace GraphIO
{
    /// <summary>Пишет граф как список связности («v: u1 u2 …»).</summary>
    public class AdjacencyListWriter : IGraphWriter
    {
        public void Write(Graph graph, TextWriter writer)
        {
            foreach (var line in graph.ToAdjacencyListLines())
                writer.WriteLine(line);
        }
    }
}
