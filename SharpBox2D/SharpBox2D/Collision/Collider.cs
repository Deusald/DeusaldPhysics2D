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

    public class Collider : ICollider
    {
        #region Public Variables

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

        public event IPhysics2D.OnCollisionEvent OnCollisionEnter;

        public event IPhysics2D.OnCollisionEvent OnCollisionExit;

        #endregion Public Variables

        #region Public Methods

        internal Collider(b2Fixture fixture, IPhysicsObject physicsObject, int colliderId, Physics2D physics2D)
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

        public void RayCast(IPhysics2D.SingleRayCastCallback callback, Vector2 origin, Vector2 end, int childIndex = 0)
        {
            b2RayCastInput input = new b2RayCastInput
            {
                p1          = Vector2.ConvertToB2Vec(origin),
                p2          = Vector2.ConvertToB2Vec(end),
                maxFraction = 1f
            };

            b2RayCastOutput output = new b2RayCastOutput();
            bool            result = Fixture.RayCast(output, input, childIndex);

            if (result)
            {
                Vector2 point = Vector2.Lerp(origin, end, output.fraction);
                callback.Invoke(true, point, Vector2.ConvertFromB2Vec(output.normal), output.fraction);
            }
            else
                callback.Invoke(false, Vector2.zero, Vector2.zero, 0f);
        }

        public void RayCast(IPhysics2D.SingleRayCastCallback callback, Vector2 origin, Vector2 direction, float distance, int childIndex = 0)
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
                lowerBound = Vector2.ConvertToB2Vec(lowerBound),
                upperBound = Vector2.ConvertToB2Vec(upperBound)
            };

            return Box2d.b2TestOverlap(aabb, testAabb);
        }

        public void OverlapPoint(IPhysics2D.OverlapPointCallback callback, Vector2 point, int childIndex = 0)
        {
            b2Vec2      vec2Point      = Vector2.ConvertToB2Vec(point);
            b2Transform pointTransform = new b2Transform(vec2Point, new b2Rot(0f));
            OverlapPoint(callback, vec2Point, pointTransform, childIndex);
        }

        internal void OverlapPoint(IPhysics2D.OverlapPointCallback callback, b2Vec2 point, b2Transform transform, int childIndex = 0)
        {
            bool hit = Fixture.TestPoint(point);
            callback.Invoke(hit, () => _Physics2D.GetDistance(_PointShape, 0, transform,
                                                              Fixture.GetShape(), childIndex, Fixture.GetBody().GetTransform()));
        }

        internal void OverlapShape(IPhysics2D.SingleOverlapShapeCallback callback, b2Shape shape, b2Transform transform, int childIndex = 0)
        {
            DistanceOutput distanceOutput = _Physics2D.GetDistance(shape, 0, transform,
                                                                   Fixture.GetShape(), childIndex, Fixture.GetBody().GetTransform());

            callback.Invoke(distanceOutput.Distance <= 0, distanceOutput);
        }

        public void OverlapBox(IPhysics2D.SingleOverlapShapeCallback callback, float width, float height, Vector2 position, float rotation, int childIndex = 0)
        {
            b2PolygonShape box = new b2PolygonShape();
            box.SetAsBox(width, height, new b2Vec2(0f, 0f), 0f);
            b2Transform transform = new b2Transform(Vector2.ConvertToB2Vec(position), new b2Rot(rotation));
            OverlapShape(callback, box, transform, childIndex);
        }

        public void OverlapCircle(IPhysics2D.SingleOverlapShapeCallback callback, float radius, Vector2 position, int childIndex = 0)
        {
            b2CircleShape circle    = new b2CircleShape {m_radius = radius};
            b2Transform   transform = new b2Transform(Vector2.ConvertToB2Vec(position), new b2Rot(0f));
            OverlapShape(callback, circle, transform, childIndex);
        }

        public void OverlapPolygon(IPhysics2D.SingleOverlapShapeCallback callback, Vector2[] vertices, Vector2 position, float rotation, int childIndex = 0)
        {
            b2PolygonShape shape = new b2PolygonShape();
            b2Vec2         array = Box2d.new_b2Vec2Array(vertices.Length);

            for (int i = 0; i < vertices.Length; ++i)
                Box2d.b2Vec2Array_setitem(array, i, Vector2.ConvertToB2Vec(vertices[i]));

            shape.Set(array, vertices.Length);
            b2Transform transform = new b2Transform(Vector2.ConvertToB2Vec(position), new b2Rot(rotation));
            OverlapShape(callback, shape, transform, childIndex);
        }

        internal void ShapeCast(IPhysics2D.SingleShapeCast callback, b2Shape shape, b2Transform transform, b2Vec2 translation, int childIndex = 0)
        {
            b2DistanceProxy proxyFixed = new b2DistanceProxy();
            proxyFixed.Set(Fixture.GetShape(), childIndex);
            b2DistanceProxy proxyMoving = new b2DistanceProxy();
            proxyMoving.Set(shape, 0);

            b2ShapeCastInput input = new b2ShapeCastInput
            {
                proxyA       = proxyFixed,
                proxyB       = proxyMoving,
                transformA   = Fixture.GetBody().GetTransform(),
                transformB   = transform,
                translationB = translation
            };

            b2ShapeCastOutput output  = new b2ShapeCastOutput();
            bool              success = Box2d.b2ShapeCast(output, input);
            callback.Invoke(success, Vector2.ConvertFromB2Vec(output.point), Vector2.ConvertFromB2Vec(output.normal), output.lambda);
        }

        public void CircleCast(IPhysics2D.SingleShapeCast callback, Vector2 origin, float radius, Vector2 direction, float distance, int childIndex = 0)
        {
            b2CircleShape circle      = new b2CircleShape {m_radius = radius};
            b2Transform   transform   = new b2Transform(Vector2.ConvertToB2Vec(origin), new b2Rot(0f));
            b2Vec2        translation = Vector2.ConvertToB2Vec(direction * distance);
            ShapeCast(callback, circle, transform, translation, childIndex);
        }

        public void CircleCast(IPhysics2D.SingleShapeCast callback, Vector2 origin, float radius, Vector2 end, int childIndex = 0)
        {
            Vector2 direction = end - origin;
            float   distance  = direction.magnitude;
            direction.Normalize();
            CircleCast(callback, origin, radius, direction, distance, childIndex);
        }

        public void BoxCast(IPhysics2D.SingleShapeCast callback, float width, float height, Vector2 origin, float rotation, Vector2 direction, float distance, int childIndex = 0)
        {
            b2PolygonShape box = new b2PolygonShape();
            box.SetAsBox(width, height, new b2Vec2(0f, 0f), 0f);
            b2Transform transform   = new b2Transform(Vector2.ConvertToB2Vec(origin), new b2Rot(rotation));
            b2Vec2      translation = Vector2.ConvertToB2Vec(direction * distance);
            ShapeCast(callback, box, transform, translation, childIndex);
        }

        public void BoxCast(IPhysics2D.SingleShapeCast callback, float width, float height, Vector2 origin, float rotation, Vector2 end, int childIndex = 0)
        {
            Vector2 direction = end - origin;
            float   distance  = direction.magnitude;
            direction.Normalize();
            BoxCast(callback, width, height, origin, rotation, direction, distance, childIndex);
        }

        public void PolygonCast(IPhysics2D.SingleShapeCast callback, Vector2[] vertices, Vector2 origin, float rotation, Vector2 direction, float distance, int childIndex = 0)
        {
            b2PolygonShape shape = new b2PolygonShape();
            b2Vec2         array = Box2d.new_b2Vec2Array(vertices.Length);

            for (int i = 0; i < vertices.Length; ++i)
                Box2d.b2Vec2Array_setitem(array, i, Vector2.ConvertToB2Vec(vertices[i]));

            shape.Set(array, vertices.Length);
            b2Transform transform = new b2Transform(Vector2.ConvertToB2Vec(origin), new b2Rot(rotation));
            
            b2Vec2 translation = Vector2.ConvertToB2Vec(direction * distance);
            ShapeCast(callback, shape, transform, translation, childIndex);
        }

        public void PolygonCast(IPhysics2D.SingleShapeCast callback, Vector2[] vertices, Vector2 origin, float rotation, Vector2 end, int childIndex = 0)
        {
            Vector2 direction = end - origin;
            float   distance  = direction.magnitude;
            direction.Normalize();
            PolygonCast(callback, vertices, origin, rotation, direction, distance, childIndex);
        }

        #endregion Public Methods

        #region Private Variables

        private readonly b2Shape   _PointShape;
        private readonly Physics2D _Physics2D;

        #endregion Private Variables
    }
}