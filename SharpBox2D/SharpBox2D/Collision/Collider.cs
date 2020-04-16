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

        internal Collider(b2Fixture fixture, IPhysicsObject physicsObject, int colliderId)
        {
            ShapeType     = (ShapeType) fixture.GetShapeType();
            PhysicsObject = physicsObject;
            ColliderId    = colliderId;
            ChildCount    = fixture.GetShape().GetChildCount();
            Fixture      = fixture;
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
                p1 = Vector2.ConvertToB2Vec(origin),
                p2 = Vector2.ConvertToB2Vec(end),
                maxFraction = 1f
            };
            
            b2RayCastOutput output = new b2RayCastOutput();
            bool result = Fixture.RayCast(output, input, childIndex);

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

        public bool OverlapPoint(Vector2 point)
        {
            return Fixture.TestPoint(Vector2.ConvertToB2Vec(point));
        }

        #endregion Public Methods
    }
}