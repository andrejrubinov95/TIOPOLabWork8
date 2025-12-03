using System;
using System.Collections.Generic;

namespace GraphIO
{
    /// <summary>Класс-утилита: обход в глубину от заданной вершины.</summary>
    public static class DepthFirstSearch
    {
        /// <summary>Возвращает порядок посещения вершин.</summary>
        public static IReadOnlyList<int> Traverse(Graph g, int start)
        {
            if (g == null) throw new ArgumentNullException(nameof(g));

            var visited = new HashSet<int>();
            var result = new List<int>();

            void Dfs(int v)
            {
                visited.Add(v);
                result.Add(v);
                foreach (var u in g.Neighbors(v))
                    if (!visited.Contains(u))
                        Dfs(u);
            }

            Dfs(start);
            return result;
        }
    }
}
