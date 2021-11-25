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

using Box2D;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    public class OverlapShapeInput
    {
        #region Public Variables

        internal          b2Shape     shape;
        internal readonly b2Transform transform;
        internal readonly b2AABB      aabb;

        #endregion Public Variables

        #region Init Methods

        public OverlapShapeInput()
        {
            transform = Physics2D.GetNewTransform(Vector2.Zero, 0f);
            aabb      = new b2AABB();
        }

        #endregion Init Methods

        #region Public Methods

        public void SetAsCircle(float radius)
        {
            shape = Physics2D.GetCircleShape(radius, Vector2.Zero);
        }

        public void SetAsBox(float width, float height)
        {
            shape = Physics2D.GetBoxShape(width, height, Vector2.Zero, 0f);
        }

        public void SetAsPolygon(Vector2[] vertices)
        {
            shape = Physics2D.GetPolygonShape(vertices);
        }

        public void SetTransform(Vector2 position, float rotation)
        {
            transform.p = SharpBox2D.ToB2Vec2(position);
            transform.q.Set(rotation);
            FillAabb();
        }

        public void SetPosition(Vector2 position)
        {
            transform.p = SharpBox2D.ToB2Vec2(position);
            FillAabb();
        }

        public void SetRotation(float rotation)
        {
            transform.q.Set(rotation);
            FillAabb();
        }

        #endregion Public Methods

        #region Private Methods

        private void FillAabb()
        {
            shape.ComputeAABB(aabb, transform, 0);
        }

        #endregion Private Methods
    }
}