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
    using Box2D;

    public class PhysicsObject : IPhysicsObject
    {
        #region Public Variables

        #region Getters

        public   int                                   PhysicsObjectId { get; }
        public   bool                                  IsStatic        => _Body.Type == b2BodyType.b2_staticBody;
        public   bool                                  IsKinematic     => _Body.Type == b2BodyType.b2_kinematicBody;
        public   bool                                  IsDynamic       => _Body.Type == b2BodyType.b2_dynamicBody;
        public   Vector2                               WorldCenter     => Vector2.ConvertFromB2Vec(_Body.GetWorldCenter());
        public   Vector2                               LocalCenter     => Vector2.ConvertFromB2Vec(_Body.GetLocalCenter());
        public   float                                 Mass            => _Body.GetMass();
        public   float                                 Inertia         => _Body.GetInertia();
        public   Dictionary<int, ICollider>.Enumerator Colliders       => _Colliders.GetEnumerator();
        internal b2Body                                Body            => _Body;

        #endregion Getters

        #region Getters And Setters

        public Vector2 Position
        {
            get => Vector2.ConvertFromB2Vec(_Body.GetPosition());
            set => _Body.SetTransform(Vector2.ConvertToB2Vec(value), _Body.GetAngle());
        }

        public float Rotation
        {
            get => _Body.GetAngle();
            set => _Body.SetTransform(_Body.GetPosition(), value);
        }

        public Vector2 LinearVelocity
        {
            get => _MovePosition ? _PreviousLinearVelocity : Vector2.ConvertFromB2Vec(_Body.GetLinearVelocity());
            set
            {
                if (_MovePosition)
                    _PreviousLinearVelocity = value;
                else
                    _Body.SetLinearVelocity(Vector2.ConvertToB2Vec(value));
            }
        }

        public float AngularVelocity
        {
            get => _Body.GetAngularVelocity();
            set => _Body.SetAngularVelocity(value);
        }

        public float LinearDamping
        {
            get => _Body.GetLinearDamping();
            set => _Body.SetLinearDamping(value);
        }

        public float AngularDamping
        {
            get => _Body.GetAngularDamping();
            set => _Body.SetAngularDamping(value);
        }

        public float GravityScale
        {
            get => _Body.GetGravityScale();
            set => _Body.SetGravityScale(value);
        }

        public bool IsBullet
        {
            get => _Body.IsBullet();
            set => _Body.SetBullet(value);
        }

        public bool SleepingAllowed
        {
            get => _Body.IsSleepingAllowed();
            set => _Body.SetSleepingAllowed(value);
        }

        public bool IsAwake
        {
            get => _Body.IsAwake();
            set => _Body.SetAwake(value);
        }

        public bool Enabled
        {
            get => _Body.IsEnabled();
            set => _Body.SetEnabled(value);
        }

        public bool FixedRotation
        {
            get => _Body.IsFixedRotation();
            set => _Body.SetFixedRotation(value);
        }

        public object UserData { get; set; }

        public BodyType BodyType
        {
            get => (BodyType) _Body.Type;
            set => _Body.Type = (b2BodyType) value;
        }

        #endregion Getters And Setters

        #endregion Public Variables

        #region Public Methods

        internal PhysicsObject(Physics2D physics2D, b2Body body, int myId, uint physicsStepsPerSec)
        {
            _Physics2D          = physics2D;
            _Body               = body;
            PhysicsObjectId     = myId;
            _PhysicsStepsPerSec = physicsStepsPerSec;
            _NextColliderId     = 1;
            _Colliders          = new Dictionary<int, ICollider>();
        }

        public void Destroy()
        {
            _Physics2D.DestroyPhysicsObject(PhysicsObjectId);
        }

        public ICollider GetCollider(int colliderId)
        {
            return !_Colliders.ContainsKey(colliderId) ? null : _Colliders[colliderId];
        }

        #region Movement, Rotation and Position

        public void SetTransform(Vector2 position, float angle)
        {
            _Body.SetTransform(Vector2.ConvertToB2Vec(position), angle);
        }

        public void MovePosition(Vector2 position)
        {
            _MovePositionTarget = position;

            if (_MovePosition) return;

            _PreviousLinearVelocity = Vector2.ConvertFromB2Vec(_Body.GetLinearVelocity());
            _MovePosition           = true;
        }

        public void ApplyForce(Vector2 force, Vector2 point)
        {
            _Body.ApplyForce(Vector2.ConvertToB2Vec(force), Vector2.ConvertToB2Vec(point), true);
        }

        public void ApplyForceToCenter(Vector2 force)
        {
            _Body.ApplyForceToCenter(Vector2.ConvertToB2Vec(force), true);
        }

        public void ApplyTorque(float torque)
        {
            _Body.ApplyTorque(torque, true);
        }

        public void ApplyLinearImpulse(Vector2 impulse, Vector2 point)
        {
            _Body.ApplyLinearImpulse(Vector2.ConvertToB2Vec(impulse), Vector2.ConvertToB2Vec(point), true);
        }

        public void ApplyLinearImpulseToCenter(Vector2 impulse)
        {
            _Body.ApplyLinearImpulseToCenter(Vector2.ConvertToB2Vec(impulse), true);
        }

        public void ApplyAngularImpulse(float impulse)
        {
            _Body.ApplyAngularImpulse(impulse, true);
        }

        public Vector2 GetWorldPoint(Vector2 localPoint)
        {
            return Vector2.ConvertFromB2Vec(_Body.GetWorldPoint(Vector2.ConvertToB2Vec(localPoint)));
        }

        public Vector2 GetWorldVector(Vector2 localVector)
        {
            return Vector2.ConvertFromB2Vec(_Body.GetWorldVector(Vector2.ConvertToB2Vec(localVector)));
        }

        public Vector2 GetLocalPoint(Vector2 worldPoint)
        {
            return Vector2.ConvertFromB2Vec(_Body.GetLocalPoint(Vector2.ConvertToB2Vec(worldPoint)));
        }

        public Vector2 GetLocalVector(Vector2 worldVector)
        {
            return Vector2.ConvertFromB2Vec(_Body.GetLocalVector(Vector2.ConvertToB2Vec(worldVector)));
        }

        public Vector2 GetLinearVelocityFromWorldPoint(Vector2 worldPoint)
        {
            return Vector2.ConvertFromB2Vec(_Body.GetLinearVelocityFromWorldPoint(Vector2.ConvertToB2Vec(worldPoint)));
        }

        public Vector2 GetLinearVelocityFromLocalPoint(Vector2 localPoint)
        {
            return Vector2.ConvertFromB2Vec(_Body.GetLinearVelocityFromWorldPoint(Vector2.ConvertToB2Vec(localPoint)));
        }

        internal void UpdateLinearVelocity()
        {
            if (!_MovePosition) return;

            Vector2 position = Position;

            if (position == _MovePositionTarget)
            {
                _MovePosition = false;
                _Body.SetLinearVelocity(Vector2.ConvertToB2Vec(_PreviousLinearVelocity));
                return;
            }

            Vector2 moveToPointVelocity = _MovePositionTarget - position;
            moveToPointVelocity *= _PhysicsStepsPerSec;
            _Body.SetLinearVelocity(Vector2.ConvertToB2Vec(moveToPointVelocity));
        }

        #endregion Movement, Rotation and Position

        #region Add / Remove Colliders

        public void DestroyCollider(int colliderId)
        {
            if (!_Colliders.ContainsKey(colliderId)) return;

            Collider collider = (Collider) _Colliders[colliderId];
            _Body.DestroyFixture(collider.Fixture);
            _Colliders.Remove(colliderId);
        }

        public ICollider AddEdgeCollider(Vector2 start, Vector2 end)
        {
            b2EdgeShape shape = new b2EdgeShape();
            shape.Set(Vector2.ConvertToB2Vec(start), Vector2.ConvertToB2Vec(end));
            b2Fixture fixture    = _Body.CreateFixture(shape, 1f);
            int       colliderId = _NextColliderId++;
            fixture.SetUserData(new IntPtr(colliderId));
            ICollider collider = new Collider(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddChainCollider(Vector2[] vertices, bool loop)
        {
            b2ChainShape shape = new b2ChainShape();
            b2Vec2       array = Box2d.new_b2Vec2Array(vertices.Length);

            for (int i = 0; i < vertices.Length; ++i)
                Box2d.b2Vec2Array_setitem(array, i, Vector2.ConvertToB2Vec(vertices[i]));

            if (loop)
                shape.CreateLoop(array, vertices.Length);
            else
                shape.CreateChain(array, vertices.Length);

            b2Fixture fixture    = _Body.CreateFixture(shape, 1f);
            int       colliderId = _NextColliderId++;
            fixture.SetUserData(new IntPtr(colliderId));
            ICollider collider = new Collider(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddBoxCollider(float width, float height, Vector2 offset, float angle, float density)
        {
            b2Shape   shape      = Physics2D.GetBoxShape(width, height, offset, angle);
            b2Fixture fixture    = _Body.CreateFixture(shape, density);
            int       colliderId = _NextColliderId++;
            fixture.SetUserData(new IntPtr(colliderId));
            ICollider collider = new Collider(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddBoxCollider(float width, float height, float density = 1f)
        {
            return AddBoxCollider(width, height, Vector2.zero, 0f, density);
        }

        public ICollider AddCircleCollider(float radius, Vector2 offset, float density)
        {
            b2Shape   shape      = Physics2D.GetCircleShape(radius, offset);
            b2Fixture fixture    = _Body.CreateFixture(shape, density);
            int       colliderId = _NextColliderId++;
            fixture.SetUserData(new IntPtr(colliderId));
            ICollider collider = new Collider(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddCircleCollider(float radius, float density = 1f)
        {
            return AddCircleCollider(radius, Vector2.zero, density);
        }

        public ICollider AddPolygonCollider(Vector2[] vertices, float density)
        {
            b2Shape   shape      = Physics2D.GetPolygonShape(vertices);
            b2Fixture fixture    = _Body.CreateFixture(shape, density);
            int       colliderId = _NextColliderId++;
            fixture.SetUserData(new IntPtr(colliderId));
            ICollider collider = new Collider(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        #endregion Add / Remove Colliders

        #endregion Public Methods

        #region Private Variables

        private Vector2 _MovePositionTarget;
        private bool    _MovePosition;
        private Vector2 _PreviousLinearVelocity;
        private int     _NextColliderId;

        private readonly Physics2D                  _Physics2D;
        private readonly b2Body                     _Body;
        private readonly uint                       _PhysicsStepsPerSec;
        private readonly Dictionary<int, ICollider> _Colliders;

        #endregion Private Variables
    }
}