using NUnit.Framework;
using System.IO;
using GraphIO;

namespace GraphIO.Tests
{
    public class DfsTests
    {
        [Test]
        public void DepthFirstSearch_Traverse_ReturnsExpectedOrder()
        {
            var g = new Graph();
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(0, 3);

            var order = DepthFirstSearch.Traverse(g, 0);

            // Возможны {0,1,2,3} или {0,3,1,2} – проверим лишь корректность
            CollectionAssert.AreEquivalent(new[] { 0, 1, 2, 3 }, order);
            Assert.AreEqual(0, order[0], "первой должна быть стартовая вершина");
        }
    }
}
