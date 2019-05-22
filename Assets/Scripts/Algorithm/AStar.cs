using System.Collections.Generic;
using System.Linq;
using Pathfinding.Graph;
using UnityEngine;

namespace Pathfinding.Algorithm
{
    public class AStar : PathfindingAlgorithm
    {
        // TODO: use priority queue implementation
        private readonly List<Node> _sortedList;

        public AStar(GraphBoard board) : base(board)
        {
            board.StartNode.Distance = 0;

            _sortedList = new List<Node>();
            _sortedList.AddRange(board.Nodes.Where(x => x.Type != NodeType.Obstacle));
            _sortedList.Sort(new AStarNodeComparer(board.GoalNode));
        }

        public override void DoStep()
        {
            if (IsComplete)
            {
                return;
            }

            var current = ExtractMin();
            if (current == null)
            {
                IsComplete = true;
                Debug.LogError("Could not find path");
                return;
            }

            current.SetCompleted();

            if (current.gameObject.GetInstanceID() == Board.GoalNode.gameObject.GetInstanceID())
            {
                IsComplete = true;
                return;
            }

            var neighbors = GetNeighbors(current).Where(x => !x.IsCompleted);
            foreach (var neighbor in neighbors)
            {
                var alternativeDistance = current.Distance + DistanceToNeighbor(current, neighbor);
                if (alternativeDistance < neighbor.Distance)
                {
                    neighbor.SetVisited();
                    neighbor.Distance = alternativeDistance;
                    neighbor.PreviousNode = current;
                }
            }

            _sortedList.Sort(new AStarNodeComparer(Board.GoalNode));
        }

        private static float DistanceToNeighbor(Node current, Node neighbor)
        {
            if (current.X != neighbor.X && current.Y != neighbor.Y)
            {
                return Mathf.Sqrt(2);
            }

            return 1;
        }

        public Node ExtractMin()
        {
            if (_sortedList.Count == 0)
            {
                return null;
            }

            var toReturn = _sortedList[0];
            _sortedList.RemoveAt(0);

            return toReturn;
        }

        public class AStarNodeComparer : IComparer<Node>
        {
            private readonly Node _goalNode;

            public AStarNodeComparer(Node goalNode)
            {
                _goalNode = goalNode;
            }

            public int Compare(Node x, Node y)
            {
                var scoreX = x.Distance + HeuristicDistance(x);
                var scoreY = y.Distance + HeuristicDistance(y);

                var compare = scoreX - scoreY;
                return compare < 0 ? -1 : compare > 0 ? 1 : 0;
            }

            private float HeuristicDistance(Node node)
            {
                return Mathf.Abs(node.X - _goalNode.X) + Mathf.Abs(node.Y - _goalNode.Y);
            }
        }

    }
}
