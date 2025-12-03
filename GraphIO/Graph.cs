using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphIO
{
    /// <summary>Неориентированный граф, хранится как список связности.</summary>
    public class Graph
    {
        /// <summary>Список смежности: вершина → соседние вершины.</summary>
        private readonly Dictionary<int, List<int>> _adj = new();

        public int VertexCount => _adj.Count;

        /// <summary>Добавить вершину, если её ещё нет.</summary>
        public void AddVertex(int v)
        {
            if (!_adj.ContainsKey(v))
                _adj[v] = new List<int>();
        }

        /// <summary>Добавить неориентированное ребро v-u.</summary>
        public void AddEdge(int v, int u)
        {
            AddVertex(v);
            AddVertex(u);
            if (!_adj[v].Contains(u)) _adj[v].Add(u);
            if (!_adj[u].Contains(v)) _adj[u].Add(v);
        }

        /// <summary>Соседи вершины v.</summary>
        public IReadOnlyList<int> Neighbors(int v) => _adj.TryGetValue(v, out var list) ? list : Array.Empty<int>();

        /// <summary>Экспорт списка связности: каждая строка «v: u1 u2 …».</summary>
        public IEnumerable<string> ToAdjacencyListLines() =>
            _adj.OrderBy(p => p.Key)
                .Select(p => $"{p.Key}: {string.Join(' ', p.Value.OrderBy(x => x))}");
    }
}
