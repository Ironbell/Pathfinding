using NUnit.Framework;
using Pathfinding.Algorithm;
using Pathfinding.Graph;

namespace Pathfinding.Tests.EditMode
{
    public class NodeComparerTests
    {
        [Test]
        public void GivenTwoNodes_WhenComparingUsingDijkstra_MinNodeReturned()
        {
            var node1 = new Node { Distance = 0.4f };
            var node2 = new Node { Distance = 0.2f };

            var comparer = new Dijkstra.DijkstraNodeComparer();
            var result = comparer.Compare(node1, node2);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GivenTwoNodes_WhenComparingUsingAStar_MinNodeReturned()
        {
            var node1 = new Node { X = 0, Y = 0 };
            var node2 = new Node { X = 2, Y = 0};
            var goalNode = new Node {X = 3, Y = 2};

            var comparer = new AStar.AStarNodeComparer(goalNode);
            var result = comparer.Compare(node1, node2);

            Assert.AreEqual(1, result);
        }
    }
}
