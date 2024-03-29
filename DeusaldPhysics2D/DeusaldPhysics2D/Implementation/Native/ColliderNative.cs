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
    internal class ColliderNative : ICollider
    {
        #region Properties

        public ShapeType      ShapeType     { get; }
        public IPhysicsObject PhysicsObject { get; }
        public int            ColliderId    { get; }
        public int            ChildCount    { get; }

        public bool IsSensor
        {
            get => Fixture.IsSensor();
            set => Fixture.SetSensor(value);
        }

        public ushort CategoryBits
        {
            get => Fixture.GetFilterData().categoryBits;
            set
            {
                b2Filter filter = Fixture.GetFilterData();
                filter.categoryBits = value;
                Fixture.SetFilterData(filter);
            }
        }

        public ushort MaskBits
        {
            get => Fixture.GetFilterData().maskBits;
            set
            {
                b2Filter filter = Fixture.GetFilterData();
                filter.maskBits = value;
                Fixture.SetFilterData(filter);
            }
        }

        public short GroupIndex
        {
            get => Fixture.GetFilterData().groupIndex;
            set
            {
                b2Filter filter = Fixture.GetFilterData();
                filter.groupIndex = value;
                Fixture.SetFilterData(filter);
            }
        }

        public float Density
        {
            get => Fixture.GetDensity();
            set => Fixture.SetDensity(value);
        }

        public float Friction
        {
            get => Fixture.GetFriction();
            set => Fixture.SetFriction(value);
        }

        public float Restitution
        {
            get => Fixture.GetRestitution();
            set => Fixture.SetRestitution(value);
        }

        public object UserData { get; set; }

        internal b2Fixture Fixture { get; }

        public event Delegates.OnCollisionEvent OnCollisionEnter;

        public event Delegates.OnCollisionEvent OnCollisionExit;

        #endregion Properties
        
        #region Variables

        private readonly b2Shape   _PointShape;
        private readonly Physics2DNative _Physics2D;

        #endregion Variables
        
        #region Init Methods

        internal ColliderNative(b2Fixture fixture, IPhysicsObject physicsObject, int colliderId, Physics2DNative physics2D)
        {
            ShapeType     = (ShapeType) fixture.GetShapeType();
            PhysicsObject = physicsObject;
            ColliderId    = colliderId;
            ChildCount    = fixture.GetShape().GetChildCount();
            Fixture       = fixture;
            _Physics2D    = physics2D;

            _PointShape = new b2CircleShape
            {
                m_p      = new b2Vec2(0f, 0f),
                m_radius = float.Epsilon
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
            b2RayCastInput input = new b2RayCastInput
            {
                p1          = origin.ToB2Vec2(),
                p2          = end.ToB2Vec2(),
                maxFraction = 1f
            };

            b2RayCastOutput output = new b2RayCastOutput();
            bool            result = Fixture.RayCast(output, input, childIndex);

            if (result)
            {
                Vector2 point = Vector2.Lerp(origin, end, output.fraction);
                callback.Invoke(true, point, output.normal.ToVector2(), output.fraction);
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
            b2AABB aabb = new b2AABB();
            Fixture.GetShape().ComputeAABB(aabb, Fixture.GetBody().GetTransform(), childIndex);
            b2AABB testAabb = new b2AABB
            {
                lowerBound = lowerBound.ToB2Vec2(),
                upperBound = upperBound.ToB2Vec2()
            };

            return Box2d.b2TestOverlap(aabb, testAabb);
        }

        public void OverlapPoint(Delegates.OverlapPointCallback callback, Vector2 point, int childIndex = 0)
        {
            b2Vec2      vec2Point      = point.ToB2Vec2();
            b2Transform pointTransform = new b2Transform(vec2Point, new b2Rot(0f));
            OverlapPoint(callback, vec2Point, pointTransform, childIndex);
        }

        internal void OverlapPoint(Delegates.OverlapPointCallback callback, b2Vec2 point, b2Transform transform, int childIndex = 0)
        {
            bool hit = Fixture.TestPoint(point);
            callback.Invoke(hit, () => _Physics2D.GetDistance(_PointShape, 0, transform,
                Fixture.GetShape(), childIndex, Fixture.GetBody().GetTransform()));
        }

        public void OverlapShape(Delegates.SingleOverlapShapeCallback callback, IOverlapShapeInput input, int childIndex = 0)
        {
            OverlapShapeInputNative inputUnpacked = (OverlapShapeInputNative)input;
            DistanceOutput distanceOutput = _Physics2D.GetDistance(inputUnpacked.shape, 0, inputUnpacked.transform,
                Fixture.GetShape(), childIndex, Fixture.GetBody().GetTransform());

            callback.Invoke(distanceOutput.distance <= 0, distanceOutput);
        }

        public void ShapeCast(Delegates.SingleShapeCastCallback callback, IShapeCastInput input, int childIndex = 0)
        {
            ShapeCastInputNative inputUnpacked = (ShapeCastInputNative)input;
            
            b2DistanceProxy proxyFixed = new b2DistanceProxy();
            proxyFixed.Set(Fixture.GetShape(), childIndex);
            b2DistanceProxy proxyMoving = new b2DistanceProxy();
            proxyMoving.Set(inputUnpacked.shape, 0);

            b2ShapeCastInput castInput = new b2ShapeCastInput
            {
                proxyA       = proxyFixed,
                proxyB       = proxyMoving,
                transformA   = Fixture.GetBody().GetTransform(),
                transformB   = inputUnpacked.transform,
                translationB = inputUnpacked.translation
            };

            b2ShapeCastOutput output  = new b2ShapeCastOutput();
            bool              success = Box2d.b2ShapeCast(output, castInput);
            callback.Invoke(success, output.point.ToVector2(), output.normal.ToVector2(), output.lambda);
        }

        public AABB GetAABB(int childIndex = 0)
        {
            b2AABB aabb = new b2AABB();
            Fixture.GetShape().ComputeAABB(aabb, ((PhysicsObjectNative)PhysicsObject).Body.GetTransform(), childIndex);
            return new AABB
            {
                LowerBound = aabb.lowerBound.ToVector2(),
                UpperBound = aabb.upperBound.ToVector2()
            };
        }
        
        #endregion Public Methods
    }
}