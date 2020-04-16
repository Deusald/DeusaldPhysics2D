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
    using Box2D;

    public class OverlapShapeInput
    {
        #region Public Variables

        internal          b2Shape     Shape;
        internal readonly b2Transform Transform;
        internal readonly b2AABB      Aabb;

        #endregion Public Variables

        #region Public Methods

        public OverlapShapeInput()
        {
            Transform = Physics2D.GetNewTransform(Vector2.zero, 0f);
            Aabb      = new b2AABB();
        }

        public void SetAsCircle(float radius)
        {
            Shape = Physics2D.GetCircleShape(radius, Vector2.zero);
        }

        public void SetAsBox(float width, float height)
        {
            Shape = Physics2D.GetBoxShape(width, height, Vector2.zero, 0f);
        }

        public void SetAsPolygon(Vector2[] vertices)
        {
            Shape = Physics2D.GetPolygonShape(vertices);
        }

        public void SetTransform(Vector2 position, float rotation)
        {
            Transform.p = Vector2.ConvertToB2Vec(position);
            Transform.q.Set(rotation);
            FillAabb();
        }

        public void SetPosition(Vector2 position)
        {
            Transform.p = Vector2.ConvertToB2Vec(position);
            FillAabb();
        }

        public void SetRotation(float rotation)
        {
            Transform.q.Set(rotation);
            FillAabb();
        }

        #endregion Public Methods

        #region Private Methods

        private void FillAabb()
        {
            Shape.ComputeAABB(Aabb, Transform, 0);
        }

        #endregion Private Methods
    }
}