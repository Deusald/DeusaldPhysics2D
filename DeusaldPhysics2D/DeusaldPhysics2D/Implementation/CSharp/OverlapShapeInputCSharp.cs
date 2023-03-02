// MIT License

// DeusaldPhysics2D:
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

using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    internal class OverlapShapeInputCSharp : IOverlapShapeInput
    {
        #region Public Variables

        internal Shape                     shape;
        internal Transform                 transform;
        internal Box2DSharp.Collision.AABB aabb;

        #endregion Public Variables

        #region Init Methods

        public OverlapShapeInputCSharp()
        {
            transform = Physics2DCSharp.GetNewTransform(Vector2.Zero, 0f);
            aabb      = new Box2DSharp.Collision.AABB();
        }

        #endregion Init Methods

        #region Public Methods

        public void SetAsCircle(float radius)
        {
            shape = Physics2DCSharp.GetCircleShape(radius, Vector2.Zero);
        }

        public void SetAsBox(float width, float height)
        {
            shape = Physics2DCSharp.GetBoxShape(width, height, Vector2.Zero, 0f);
        }

        public void SetAsPolygon(Vector2[] vertices)
        {
            shape = Physics2DCSharp.GetPolygonShape(vertices);
        }

        public void SetTransform(Vector2 position, float rotation)
        {
            transform.Position = position;
            transform.Rotation.Set(rotation);
            FillAabb();
        }

        public void SetPosition(Vector2 position)
        {
            transform.Position = position;
            FillAabb();
        }

        public void SetRotation(float rotation)
        {
            transform.Rotation.Set(rotation);
            FillAabb();
        }

        #endregion Public Methods

        #region Private Methods

        private void FillAabb()
        {
            shape.ComputeAABB(out aabb, transform, 0);
        }

        #endregion Private Methods
    }
}