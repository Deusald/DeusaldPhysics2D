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
    internal class ShapeCastInput : IShapeCastInput
    {
        #region Variables

        internal          b2Shape     shape;
        internal          b2Vec2      translation;
        internal readonly b2Transform transform;
        internal readonly b2AABB      aabb;

        private readonly b2Transform _EndTransform;
        private readonly b2AABB      _End;

        #endregion Variables

        #region Init Methods

        public ShapeCastInput()
        {
            transform     = Physics2D.GetNewTransform(Vector2.Zero, 0f);
            _EndTransform = Physics2D.GetNewTransform(Vector2.Zero, 0f);
            aabb          = new b2AABB();
            _End          = new b2AABB();
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

        public void SetTranslation(Vector2 origin, float rotation, Vector2 end)
        {
            transform.p = origin.ToB2Vec2();
            transform.q.Set(rotation);
            _EndTransform.p = end.ToB2Vec2();
            _EndTransform.q.Set(rotation);
            translation = (end - origin).ToB2Vec2();
            FillAabb();
        }

        public void SetTranslation(Vector2 origin, float rotation, Vector2 direction, float distance)
        {
            transform.p = origin.ToB2Vec2();
            transform.q.Set(rotation);
            Vector2 trans = direction * distance;
            _EndTransform.p = (origin + trans).ToB2Vec2();
            _EndTransform.q.Set(rotation);
            translation = trans.ToB2Vec2();
            FillAabb();
        }

        public void SetTranslation(Vector2 origin, Vector2 end)
        {
            transform.p = origin.ToB2Vec2();
            transform.q.Set(0f);
            _EndTransform.p = end.ToB2Vec2();
            _EndTransform.q.Set(0f);
            translation = (end - origin).ToB2Vec2();
            FillAabb();
        }

        public void SetTranslation(Vector2 origin, Vector2 direction, float distance)
        {
            transform.p = origin.ToB2Vec2();
            transform.q.Set(0f);
            Vector2 trans = direction * distance;
            _EndTransform.p = (origin + trans).ToB2Vec2();
            _EndTransform.q.Set(0f);
            translation = trans.ToB2Vec2();
            FillAabb();
        }

        #endregion Public Methods

        #region Private Methods

        private void FillAabb()
        {
            shape.ComputeAABB(aabb, transform,     0);
            shape.ComputeAABB(_End, _EndTransform, 0);
            aabb.Combine(_End);
        }

        #endregion Private Methods
    }
}