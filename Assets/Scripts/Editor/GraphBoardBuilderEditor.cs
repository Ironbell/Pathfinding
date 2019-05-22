using Pathfinding.Graph;
using UnityEditor;
using UnityEngine;

namespace Pathfinding.Editor
{
    [CustomEditor(typeof(GraphBoardBuilder))]
    public class GraphBoardBuilderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Rebuild"))
            {
                var builder = (GraphBoardBuilder)target;
                builder.RebuildGraph();
            }
        }
    }
}
