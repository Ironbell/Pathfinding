using UnityEngine;

namespace Pathfinding.Graph
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Node : MonoBehaviour
    {
        [SerializeField] private NodeType _type;
        [SerializeField] private int _x;
        [SerializeField] private int _y;

        private SpriteRenderer _renderer;

        public Node PreviousNode { get; set; }

        public float Distance { get; set; } = 1000000f;

        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public bool IsVisited { get; private set; }

        public bool IsCompleted { get; private set; }

        public void SetVisited()
        {
            IsVisited = true;
            UpdateSprite();
        }

        public void SetCompleted()
        {
            IsCompleted = true;
            UpdateSprite();
        }

        public NodeType Type
        {
            get => _type;
            set
            {
                _type = value;
                UpdateSprite();
            }
        }

        private SpriteRenderer Renderer
        {
            get
            {
                if (_renderer == null)
                {
                    _renderer = GetComponent<SpriteRenderer>();
                }

                return _renderer;
            }
        }

        public void SetObstacle(bool obstacle)
        {
            Type = obstacle ? NodeType.Obstacle : NodeType.Default;
        }

        private void UpdateSprite()
        {
            if (Type == NodeType.Obstacle)
            {
                Renderer.color = Color.black;
                transform.localScale = 2 * Vector3.one;
                return;
            }

            transform.localScale = Vector3.one;

            if (Type == NodeType.Start)
            {
                Renderer.color = Color.cyan;
                return;
            }

            if (Type == NodeType.Goal)
            {
                Renderer.color = Color.green;
                return;
            }

            if (IsCompleted)
            {
                Renderer.color = Color.red;
                return;
            }

            if (IsVisited)
            {
                Renderer.color = Color.magenta;
                return;
            }

            Renderer.color = Color.white;
        }
    }
}
