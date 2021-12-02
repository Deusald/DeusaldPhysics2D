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
using Box2DSharp.Collision.Collider;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    internal class ColliderCSharp : ICollider
    {
        #region Properties

        public ShapeType      ShapeType     { get; }
        public IPhysicsObject PhysicsObject { get; }
        public int            ColliderId    { get; }
        public int            ChildCount    { get; }

        public bool IsSensor
        {
            get => Fixture.IsSensor;
            set => Fixture.IsSensor = value;
        }

        public ushort CategoryBits
        {
            get => Fixture.Filter.CategoryBits;
            set
            {
                Filter filter = Fixture.Filter;
                filter.CategoryBits = value;
                Fixture.Filter      = filter;
            }
        }

        public ushort MaskBits
        {
            get => Fixture.Filter.MaskBits;
            set
            {
                Filter filter = Fixture.Filter;
                filter.MaskBits = value;
                Fixture.Filter  = filter;
            }
        }

        public short GroupIndex
        {
            get => Fixture.Filter.GroupIndex;
            set
            {
                Filter filter = Fixture.Filter;
                filter.GroupIndex = value;
                Fixture.Filter    = filter;
            }
        }

        public float Density
        {
            get => Fixture.Density;
            set => Fixture.Density = value;
        }

        public float Friction
        {
            get => Fixture.Friction;
            set => Fixture.Friction = value;
        }

        public float Restitution
        {
            get => Fixture.Restitution;
            set => Fixture.Restitution = value;
        }

        public object UserData { get; set; }

        internal Fixture Fixture { get; }

        public event Delegates.OnCollisionEvent OnCollisionEnter;

        public event Delegates.OnCollisionEvent OnCollisionExit;

        #endregion Properties

        #region Variables

        private readonly Shape           _PointShape;
        private readonly Physics2DCSharp _Physics2D;

        #endregion Variables

        #region Init Methods

        internal ColliderCSharp(Fixture fixture, IPhysicsObject physicsObject, int colliderId, Physics2DCSharp physics2D)
        {
            ShapeType     = (ShapeType)fixture.ShapeType;
            PhysicsObject = physicsObject;
            ColliderId    = colliderId;
            ChildCount    = fixture.Shape.GetChildCount();
            Fixture       = fixture;
            _Physics2D    = physics2D;

            _PointShape = new CircleShape
            {
                Position = new Vector2(0f, 0f),
                Radius   = float.Epsilon
            };
        }

        #endregion Init Methods

        #region Public Methods

        public void Destroy()
        {
            PhysicsObject.DestroyCollider(ColliderId);
        }

        internal void ExecuteOnCollisionEnter(ICollisionData collisionData)
        {
            OnCollisionEnter?.Invoke(collisionData);
        }

        internal void ExecuteOnCollisionExit(ICollisionData collisionData)
        {
            OnCollisionExit?.Invoke(collisionData);
        }

        public void RayCast(Delegates.SingleRayCastCallback callback, Vector2 origin, Vector2 end, int childIndex = 0)
        {
            RayCastInput input = new RayCastInput
            {
                P1          = origin,
                P2          = end,
                MaxFraction = 1f
            };

            bool result = Fixture.RayCast(out var output, input, childIndex);

            if (result)
            {
                Vector2 point = Vector2.Lerp(origin, end, output.Fraction);
                callback.Invoke(true, point, output.Normal, output.Fraction);
            }
            else
                callback.Invoke(false, Vector2.Zero, Vector2.Zero, 0f);
        }

        public void RayCast(Delegates.SingleRayCastCallback callback, Vector2 origin, Vector2 direction, float distance, int childIndex = 0)
        {
            Vector2 endPoint = origin + direction * distance;
            RayCast(callback, origin, endPoint, childIndex);
        }

        public bool OverlapArea(Vector2 lowerBound, Vector2 upperBound, int childIndex = 0)
        {
            Fixture.Shape.ComputeAABB(out var aabb, Fixture.Body.GetTransform(), childIndex);
            AABB testAabb = new AABB
            {
                LowerBound = lowerBound,
                UpperBound = upperBound
            };

            return CollisionUtils.TestOverlap(aabb, testAabb);
        }

        public void OverlapPoint(Delegates.OverlapPointCallback callback, Vector2 point, int childIndex = 0)
        {
            Transform pointTransform = new Transform(point, new Rotation(0f));
            OverlapPoint(callback, point, pointTransform, childIndex);
        }

        internal void OverlapPoint(Delegates.OverlapPointCallback callback, Vector2 point, Transform transform, int childIndex = 0)
        {
            bool hit = Fixture.TestPoint(point);
            callback.Invoke(hit, () => _Physics2D.GetDistance(_PointShape, 0, transform,
                Fixture.Shape, childIndex, Fixture.Body.GetTransform()));
        }

        public void OverlapShape(Delegates.SingleOverlapShapeCallback callback, IOverlapShapeInput input, int childIndex = 0)
        {
            OverlapShapeInputCSharp inputUnpacked = (OverlapShapeInputCSharp)input;
            DistanceOutput distanceOutput = _Physics2D.GetDistance(inputUnpacked.shape, 0, inputUnpacked.transform,
                Fixture.Shape, childIndex, Fixture.Body.GetTransform());

            callback.Invoke(distanceOutput.distance <= 0, distanceOutput);
        }

        public void ShapeCast(Delegates.SingleShapeCastCallback callback, IShapeCastInput input, int childIndex = 0)
        {
            ShapeCastInputCSharp inputUnpacked = (ShapeCastInputCSharp)input;

            DistanceProxy proxyFixed = new DistanceProxy();
            proxyFixed.Set(Fixture.Shape, childIndex);
            DistanceProxy proxyMoving = new DistanceProxy();
            proxyMoving.Set(inputUnpacked.shape, 0);

            ShapeCastInput castInput = new ShapeCastInput
            {
                ProxyA       = proxyFixed,
                ProxyB       = proxyMoving,
                TransformA   = Fixture.Body.GetTransform(),
                TransformB   = inputUnpacked.transform,
                TranslationB = inputUnpacked.translation
            };

            bool success = DistanceAlgorithm.ShapeCast(out var output, castInput);
            callback.Invoke(success, output.Point, output.Normal, output.Lambda);
        }

        #endregion Public Methods
    }
}