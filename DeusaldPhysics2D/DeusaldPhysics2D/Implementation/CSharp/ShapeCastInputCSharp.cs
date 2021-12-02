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

using Box2DSharp.Collision;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    internal class ShapeCastInputCSharp : IShapeCastInput
    {
        #region Variables

        internal Shape     shape;
        internal Vector2   translation;
        internal Transform transform;
        internal AABB      aabb;

        private Transform _EndTransform;
        private AABB      _End;

        #endregion Variables

        #region Init Methods

        public ShapeCastInputCSharp()
        {
            transform     = Physics2DCSharp.GetNewTransform(Vector2.Zero, 0f);
            _EndTransform = Physics2DCSharp.GetNewTransform(Vector2.Zero, 0f);
            aabb          = new AABB();
            _End          = new AABB();
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

        public void SetTranslation(Vector2 origin, float rotation, Vector2 end)
        {
            transform.Position = origin;
            transform.Rotation.Set(rotation);
            _EndTransform.Position = end;
            _EndTransform.Rotation.Set(rotation);
            translation = end - origin;
            FillAabb();
        }

        public void SetTranslation(Vector2 origin, float rotation, Vector2 direction, float distance)
        {
            transform.Position = origin;
            transform.Rotation.Set(rotation);
            Vector2 trans = direction * distance;
            _EndTransform.Position = origin + trans;
            _EndTransform.Rotation.Set(rotation);
            translation = trans;
            FillAabb();
        }

        public void SetTranslation(Vector2 origin, Vector2 end)
        {
            transform.Position = origin;
            transform.Rotation.Set(0f);
            _EndTransform.Position = end;
            _EndTransform.Rotation.Set(0f);
            translation = end - origin;
            FillAabb();
        }

        public void SetTranslation(Vector2 origin, Vector2 direction, float distance)
        {
            transform.Position = origin;
            transform.Rotation.Set(0f);
            Vector2 trans = direction * distance;
            _EndTransform.Position = origin + trans;
            _EndTransform.Rotation.Set(0f);
            translation = trans;
            FillAabb();
        }

        #endregion Public Methods

        #region Private Methods

        private void FillAabb()
        {
            shape.ComputeAABB(out aabb, transform, 0);
            shape.ComputeAABB(out _End, _EndTransform, 0);
            aabb.Combine(_End);
        }

        #endregion Private Methods
    }
}