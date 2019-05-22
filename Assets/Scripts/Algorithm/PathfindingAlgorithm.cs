using System.Collections.Generic;
using Pathfinding.Graph;

namespace Pathfinding.Algorithm
{
    public abstract class PathfindingAlgorithm
    {
        protected GraphBoard Board { get; }

        public bool IsComplete { get; protected set; }

        protected PathfindingAlgorithm(GraphBoard board)
        {
            Board = board;
        }

        public abstract void DoStep();

        protected IReadOnlyList<Node> GetNeighbors(Node node)
        {
            var list = new List<Node>();
            if (node.Type == NodeType.Obstacle)
            {
                return list;
            }

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    var newX = node.X + i;
                    var newY = node.Y + j;

                    if (newX < 0 || newX >= Board.Width)
                    {
                        continue;
                    }

                    if (newY < 0 || newY >= Board.Height)
                    {
                        continue;
                    }

                    var index = newX * Board.Height + newY;
                   
                    list.Add(Board.Nodes[index]);
                }
            }

            return list;
        }
    }
}
