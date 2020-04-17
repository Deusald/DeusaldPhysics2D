namespace SharpBox2D.Nav2D
{
    internal class Triangle
    {
        #region Public Variables

        public TypeVector3<Node> Points;
        public Vector2           CircumCircleCenter { get; private set; }
        public float             RadiusSquared;

        #endregion Public Variables

        #region Public Methods

        public Triangle(Node node1, Node node2, Node node3)
        {
            Points = new TypeVector3<Node>();

            if (!IsCounterClockwise())
            {
                Points.x = node1;
                Points.y = node3;
                Points.z = node2;
            }
            else
            {
                Points.x = node1;
                Points.y = node2;
                Points.z = node3;
            }

            node1.AdjacentTriangles.Add(this);
            node2.AdjacentTriangles.Add(this);
            node3.AdjacentTriangles.Add(this);
            UpdateCircumCircle();
        }

        public bool IsNodeInsideCircumCircle(Node node)
        {
            var squared = (node.Position.x - CircumCircleCenter.x) * (node.Position.x - CircumCircleCenter.x) +
                          (node.Position.y - CircumCircleCenter.y) * (node.Position.y - CircumCircleCenter.y);
            return squared < RadiusSquared;
        }

        #endregion Public Methods

        #region Private Methods

        private bool IsCounterClockwise()
        {
            float result = (Points.y.Position.x - Points.x.Position.x) * (Points.z.Position.y - Points.x.Position.y) -
                           (Points.z.Position.x - Points.x.Position.x) * (Points.y.Position.y - Points.x.Position.y);
            return result > 0;
        }

        private void UpdateCircumCircle()
        {
            // https://codefound.wordpress.com/2013/02/21/how-to-compute-a-circumcircle/#more-58
            // https://en.wikipedia.org/wiki/Circumscribed_circle
            Node  p0 = Points[0];
            Node  p1 = Points[1];
            Node  p2 = Points[2];
            float dA = p0.Position.x * p0.Position.x + p0.Position.y * p0.Position.y;
            float dB = p1.Position.x * p1.Position.x + p1.Position.y * p1.Position.y;
            float dC = p2.Position.x * p2.Position.x + p2.Position.y * p2.Position.y;

            float aux1 = (dA * (p2.Position.y - p1.Position.y) + dB * (p0.Position.y - p2.Position.y) + dC * (p1.Position.y - p0.Position.y));
            float aux2 = -(dA * (p2.Position.x - p1.Position.x) + dB * (p0.Position.x - p2.Position.x) + dC * (p1.Position.x - p0.Position.x));
            float div = (2f * (p0.Position.x * (p2.Position.y - p1.Position.y) +
                               p1.Position.x * (p0.Position.y - p2.Position.y) + p2.Position.x * (p1.Position.y - p0.Position.y)));

            if (MathUtils.IsFloatZero(div))
                throw new System.Exception();

            CircumCircleCenter = new Vector2(aux1 / div, aux2 / div);
            RadiusSquared = (CircumCircleCenter.x - p0.Position.x) * (CircumCircleCenter.x - p0.Position.x)
                            + (CircumCircleCenter.y - p0.Position.y) * (CircumCircleCenter.y - p0.Position.y);
        }

        #endregion Private Methods
    }
}