// MIT License

// SharpBox2D:
// Copyright (c) 2020 Adam "Deusald" Orliński

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace SharpBox2D
{
    using System;
    using System.Collections.Generic;

    public class Nav2D
    {
        #region Public Methods

        /// <summary>
        /// Create new nav2d world
        /// </summary>
        /// <param name="lowerWorldBound">Bottom left boundary of the world</param>
        /// <param name="upperWorldBound">Top right boundary of the world</param>
        /// <param name="polygonAccuracy">How many sub-pieces should polygons approximation have. It must be an odd number</param>
        /// <param name="agentsRadius"></param>
        public Nav2D(Vector2 lowerWorldBound, Vector2 upperWorldBound, int polygonAccuracy, float[] agentsRadius)
        {
            if (polygonAccuracy % 2 != 1)
                throw new Exception("Polygon accuracy must be an odd number!");

            _World  = new Aabb(lowerWorldBound, upperWorldBound);
            _Points = new List<Point>(100);

            _Points.Add(new Point(_World.LowerBound, _World));

            Aabb testBox = new Aabb(new Vector2(-1f, -1f), new Vector2(1f, 1f));
            testBox.FillPoints(ref _Points);
            _Points.Sort(PointsComparison);
        }

        #endregion Public Methods

        #region Private Variables

        private readonly Aabb        _World;
        private readonly List<Point> _Points;

        #endregion Private Variables

        #region Private Methods

        private int PointsComparison(Point x, Point y)
        {
            if (x.Y == y.Y)
            {
                if (x.X == y.X) return 0;

                if (x.X < y.X) return -1;

                return 1;
            }

            if (x.Y < y.Y) return -1;

            return 1;
        }

        private void CreateNodes()
        {
            if (_Points.Count == 0) return;

            LinkedList<Point> tmpList              = new LinkedList<Point>(_Points);
            LinkedList<Point> pointsToAddAtNextLvl = new LinkedList<Point>();
            List<Node>        nodes                = new List<Node>(tmpList.Count);

            if (tmpList.First == null || tmpList.Last == null) return;

            var currentPoint = tmpList.First;
            int currentLvl   = currentPoint.Value.Y;

            while (currentPoint != null)
            {
                Node node = new Node {DownLeft = currentPoint.Value};

                if (currentPoint.Next.Value.Y != currentLvl)
                {
                    Point bottomRight = new Point(_World.UpperBound.x, currentLvl);
                    
                }
                else
                {
                    
                }
            }
        }

        #endregion Private Methods
    }
}