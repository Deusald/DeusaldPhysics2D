namespace SharpBox2D.Nav2D
{
    internal class Edge
    {
        #region Public Variables

        public Node NodeOne;
        public Node NodeTwo;

        #endregion Public Variables

        #region Public Methods

        public Edge(Node nodeOne, Node nodeTwo)
        {
            NodeOne = nodeOne;
            NodeTwo = nodeTwo;
        }

        #endregion Public Methods
    }
}