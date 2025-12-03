using System;
using System.IO;
using System.Linq;

namespace GraphIO
{
    /// <summary>
    /// Читает граф из матрицы инцидентности (вершины × рёбра).
    /// Формат:
    ///   n m
    ///   затем n строк, каждая содержит m чисел 0/1/-1/1
    /// Для неориентированного графа в столбце будет ровно две «1».
    /// Для ориентированного — «1» и «-1». Здесь считаем, что граф неориентирован.
    /// </summary>
    public class IncidenceMatrixReader : IGraphReader
    {
        public Graph Read(TextReader reader)
        {
            var header = reader.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (header == null || header.Length != 2)
                throw new InvalidDataException("Matrix header must be \"n m\".");

            int n = int.Parse(header[0]);
            int m = int.Parse(header[1]);

            // matrix[v, e] ∈ {0,1}
            var matrix = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                var tokens = reader.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (tokens == null || tokens.Length != m)
                    throw new InvalidDataException($"Row {i} length mismatch.");
                for (int j = 0; j < m; j++)
                    matrix[i, j] = int.Parse(tokens[j]);
            }

            var g = new Graph();
            for (int v = 0; v < n; v++) g.AddVertex(v);

            for (int e = 0; e < m; e++)
            {
                var incident = Enumerable.Range(0, n).Where(v => matrix[v, e] != 0).ToArray();
                if (incident.Length != 2)
                    throw new InvalidDataException($"Edge column {e} must have exactly 2 ones.");
                g.AddEdge(incident[0], incident[1]);
            }
            return g;
        }
    }
}
