using System.Linq;
using Pathfinding.Graph;
using UnityEditor;
using UnityEngine;

namespace Pathfinding.Editor
{
    [CustomEditor(typeof(Node))]
    [CanEditMultipleObjects]
    public class NodeEditor : UnityEditor.Editor
    {
        private bool _isObstacle;

        private void Awake()
        {
            _isObstacle = ((Node) target).Type == NodeType.Obstacle;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Toggle(_isObstacle, "IsObstacle") != _isObstacle)
            {
                _isObstacle = !_isObstacle;
                targets.ToList().Select(x => (Node)x).ToList().ForEach(x => x.SetObstacle(_isObstacle));
            }
        }
    }
}
