using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Pathfinding.Graph
{
    public class GraphBoard : MonoBehaviour
    {
        [SerializeField] private int _width = 35;
        [SerializeField] private int _height = 20;
        [SerializeField] [NotNull] private Node _nodePrefab;
        [SerializeField] private Node _startNode;
        [SerializeField] private Node _goalNode;

        public List<Node> Nodes { get; } = new List<Node>();

        public int Width => _width;

        public int Height => _height;

        public Node NodePrefab => _nodePrefab;

        public Node StartNode => _startNode;

        public Node GoalNode => _goalNode;

        public static float ObstacleProbability = 0;

        private void Awake()
        {
            if (_startNode == null || _goalNode == null ||
                _startNode.gameObject.GetInstanceID() == _goalNode.gameObject.GetInstanceID())
            {
                throw new InvalidOperationException("Start and End node must be set and not be the same!");
            }

            Nodes.Clear();
            Nodes.AddRange(GetComponentsInChildren<Node>());

            foreach (var node in Nodes)
            {
                if (node.Type == NodeType.Default && UnityEngine.Random.Range(0f, 1f) < ObstacleProbability)
                {
                    node.Type = NodeType.Obstacle;
                }
            }
        }
    }
}
