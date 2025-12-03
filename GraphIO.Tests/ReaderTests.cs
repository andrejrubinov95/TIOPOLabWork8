using NUnit.Framework;
using System.IO;
using GraphIO;

namespace GraphIO.Tests
{
    public class ReaderTests
    {
        // Та же матрица инцидентности, что и раньше.
        private const string MatrixSample =
@"4 3
1 0 1
1 1 0
0 1 1
0 0 0";

        [Test]
        public void IncidenceMatrixReader_ReadsCorrectGraph()
        {
            var reader = new IncidenceMatrixReader();
            using var sr = new StringReader(MatrixSample);
            var g = reader.Read(sr);

            Assert.AreEqual(4, g.VertexCount, "Количество вершин");

            // Рёбра: (0-1), (1-2), (0-2).
            CollectionAssert.AreEquivalent(new[] { 1, 2 }, g.Neighbors(0), "Соседи вершины 0");
            CollectionAssert.AreEquivalent(new[] { 0, 2 }, g.Neighbors(1), "Соседи вершины 1");
            CollectionAssert.AreEquivalent(new[] { 0, 1 }, g.Neighbors(2), "Соседи вершины 2");
            CollectionAssert.IsEmpty(g.Neighbors(3), "Вершина 3 должна быть изолирована");
        }

        [Test]
        public void AdjacencyListWriter_WritesExpectedText()
        {
            var gReader = new IncidenceMatrixReader();
            using var sr = new StringReader(MatrixSample);
            var g = gReader.Read(sr);

            var writer = new AdjacencyListWriter();
            using var sw = new StringWriter();
            writer.Write(g, sw);

            string output = sw.ToString().TrimEnd();
            output = output.Replace("\r\n", "\n");
            var lines = output.Split('\n');

            Assert.AreEqual(4, lines.Length);

            Assert.AreEqual("0: 1 2", lines[0].TrimEnd());
            Assert.AreEqual("1: 0 2", lines[1].TrimEnd());
            Assert.AreEqual("2: 0 1", lines[2].TrimEnd());
            Assert.AreEqual("3:", lines[3].TrimEnd()); // изолированная вершина
        }
    }
}
