using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Pathfinding.Graph
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(GraphBoard))]
    public class GraphBoardBuilder : MonoBehaviour
    {
       
        private const float NodeSize = 0.5f;

        private Node _previousStartNode;
        private Node _previousGoalNode;
        private GraphBoard _board;

        private void Awake()
        {
            _board = GetComponent<GraphBoard>();
        }

        private void Update()
        {
            if (_previousGoalNode != _board.GoalNode)
            {
                if (_previousGoalNode != null)
                {
                    _previousGoalNode.Type = NodeType.Default;
                }

                _previousGoalNode = _board.GoalNode;
                _previousGoalNode.Type = NodeType.Goal;
            }

            if (_previousStartNode != _board.StartNode)
            {
                if (_previousStartNode != null)
                {
                    _previousStartNode.Type = NodeType.Default;
                }

                _previousStartNode = _board.StartNode;
                _previousStartNode.Type = NodeType.Start;
            }
        }

        public void RebuildGraph()
        {
            _previousStartNode = null;
            _previousGoalNode = null;

            foreach (var child in transform)
            {
                DestroyImmediate(((Transform)child).gameObject);
            }

            var startX = NodeSize * (_board.Width - 1) * -0.5f;
            var startY = NodeSize * (_board.Height - 1) * -0.5f;

            var currentX = startX;
            for (int x = 0; x < _board.Width; x++)
            {
                var currentY = startY;
                for (int y = 0; y < _board.Height; y++)
                {
                    var newNode = Instantiate(_board.NodePrefab, this.transform, true);
                    newNode.transform.position = new Vector3(currentX, currentY);
                    currentY += NodeSize;
                    newNode.X = x;
                    newNode.Y = y;
                }

                currentX += NodeSize;
            }
        }
    }
}
