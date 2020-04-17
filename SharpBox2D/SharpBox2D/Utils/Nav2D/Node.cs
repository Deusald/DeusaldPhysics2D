namespace SharpBox2D.Nav2D
{
    using System.Collections.Generic;

    internal class Node
    {
        #region Public Variables

        public          Vector2           Position;
        public readonly HashSet<Triangle> AdjacentTriangles;

        #endregion Public Variables

        #region Public Methods

        public Node(float x, float y)
        {
            Position          = new Vector2(x, y);
            AdjacentTriangles = new HashSet<Triangle>();
        }

        public Node(Vector2 position)
        {
            Position          = position;
            AdjacentTriangles = new HashSet<Triangle>();
        }

        #endregion Public Methods
    }
}