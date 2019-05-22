using System.Collections.Generic;
using Pathfinding.Graph;
using UnityEngine;

namespace Pathfinding.Algorithm
{
    [RequireComponent(typeof(LineRenderer))]
    public class Pathfinder : MonoBehaviour
    {
        [NotNull] [SerializeField] private GraphBoard _board;
        [SerializeField] private PathfindingType _type;

        private PathfindingAlgorithm _algorithm;

        public bool IsComplete { get; private set; }

        public static PathfindingType? TypeOverride;

        private void Start()
        {
            var type = TypeOverride ?? _type;

            switch (type)
            {
                case PathfindingType.Dijkstra:
                    _algorithm = new Dijkstra(_board);
                    break;
                case PathfindingType.AStar:
                    _algorithm = new AStar(_board);
                    break;
            }
        }

        private void Update()
        {
            if (IsComplete)
            {
                return;
            }

            if (_algorithm.IsComplete)
            {
                BacktrackPath();
                IsComplete = true;
                return;
            }

            // there is one step done per frame, 
            // so we can see what the algorithm does
            _algorithm.DoStep();
        }

        private void BacktrackPath()
        {
            var lineRenderer = GetComponent<LineRenderer>();
            var currentNode = _board.GoalNode;
            var positions = new List<Vector3> { currentNode.transform.position };

            while (currentNode.PreviousNode != null)
            {
                currentNode = currentNode.PreviousNode;
                positions.Add(currentNode.transform.position);
            }

            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
    }
}
