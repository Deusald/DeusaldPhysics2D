namespace SharpBox2D.Nav2D
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Triangulation
    {
        #region Public Methods

        public Triangulation(Vector2 lowerBound, Vector2 upperBound, ref HashSet<Node> nodes)
        {
            _Nodes      = nodes;
            _DownBorder = new Triangle(new Node(lowerBound), new Node(lowerBound.x, upperBound.y), new Node(upperBound));
            _TopBorder  = new Triangle(new Node(lowerBound), new Node(upperBound), new Node(upperBound.x, lowerBound.y));
        }

        public void Triangulate()
        {
            HashSet<Triangle> triangles = new HashSet<Triangle> {_DownBorder, _TopBorder};

            foreach (var node in _Nodes)
            {
                HashSet<Triangle> badTriangles = FindBadTriangles(node, triangles);
            }
        }

        #endregion Public Methods

        #region Private Variables

        private readonly Triangle      _DownBorder;
        private readonly Triangle      _TopBorder;
        private readonly HashSet<Node> _Nodes;

        #endregion Private Variables

        #region Private Methods

        private HashSet<Triangle> FindBadTriangles(Node node, HashSet<Triangle> triangles)
        {
            HashSet<Triangle> result = new HashSet<Triangle>();

            foreach (var triangle in triangles)
            {
                if (!triangle.IsNodeInsideCircumCircle(node)) continue;
                result.Add(triangle);
            }

            return result;
        }

        private List<Edge> FindHoleBoundaries(HashSet<Triangle> badTriangles)
        {
            List<Edge> edges = new List<Edge>();

            foreach (var triangle in badTriangles)
            {
                edges.Add(new Edge(triangle.Points[0], triangle.Points[1]));
                edges.Add(new Edge(triangle.Points[1], triangle.Points[2]));
                edges.Add(new Edge(triangle.Points[2], triangle.Points[0]));
            }
            
            var boundaryEdges = edges.GroupBy(o => o).Where(o => o.Count() == 1).Select(o => o.First());
            return boundaryEdges.ToList();
        }

        #endregion Private Methods
    }
}