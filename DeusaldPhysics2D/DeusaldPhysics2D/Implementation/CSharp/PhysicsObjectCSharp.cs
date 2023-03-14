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

using System.Collections.Generic;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Dynamics;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    internal class PhysicsObjectCSharp : IPhysicsObject
    {
        #region Properties

        #region Getters

        public   int                                 PhysicsObjectId { get; }
        public   bool                                IsStatic        => _Body.BodyType == Box2DSharp.Dynamics.BodyType.StaticBody;
        public   bool                                IsKinematic     => _Body.BodyType == Box2DSharp.Dynamics.BodyType.KinematicBody;
        public   bool                                IsDynamic       => _Body.BodyType == Box2DSharp.Dynamics.BodyType.DynamicBody;
        public   Vector2                             WorldCenter     => _Body.GetWorldCenter();
        public   Vector2                             LocalCenter     => _Body.GetLocalCenter();
        public   float                               Mass            => _Body.Mass;
        public   float                               Inertia         => _Body.Inertia;
        public   IReadOnlyDictionary<int, ICollider> Colliders       => _Colliders;
        internal Body                                Body            => _Body;

        #endregion Getters

        #region Getters And Setters

        public Vector2 Position
        {
            get => _Body.GetPosition();
            set => _Body.SetTransform(value, _Body.GetAngle());
        }

        public float Rotation
        {
            get => _Body.GetAngle();
            set => _Body.SetTransform(_Body.GetPosition(), value);
        }

        public Vector2 LinearVelocity
        {
            get => _MovePosition ? _PreviousLinearVelocity : _Body.LinearVelocity;
            set
            {
                if (_MovePosition)
                    _PreviousLinearVelocity = value;
                else
                    _Body.SetLinearVelocity(value);
            }
        }

        public float AngularVelocity
        {
            get => _Body.AngularVelocity;
            set => _Body.SetAngularVelocity(value);
        }

        public float LinearDamping
        {
            get => _Body.LinearDamping;
            set => _Body.LinearDamping = value;
        }

        public float AngularDamping
        {
            get => _Body.AngularDamping;
            set => _Body.AngularDamping = value;
        }

        public float GravityScale
        {
            get => _Body.GravityScale;
            set => _Body.GravityScale = value;
        }

        public bool IsBullet
        {
            get => _Body.IsBullet;
            set => _Body.IsBullet = value;
        }

        public bool SleepingAllowed
        {
            get => _Body.IsSleepingAllowed;
            set => _Body.IsSleepingAllowed = value;
        }

        public bool IsAwake
        {
            get => _Body.IsAwake;
            set => _Body.IsAwake = value;
        }

        public bool Enabled
        {
            get => _Body.IsEnabled;
            set => _Body.IsEnabled = value;
        }

        public bool FixedRotation
        {
            get => _Body.IsFixedRotation;
            set => _Body.IsFixedRotation = value;
        }

        public object UserData { get; set; }

        public BodyType BodyType
        {
            get => (BodyType)_Body.BodyType;
            set => _Body.BodyType = (Box2DSharp.Dynamics.BodyType)value;
        }

        #endregion Getters And Setters

        #endregion Properties

        #region Variables

        private Vector2 _MovePositionTarget;
        private bool    _MovePosition;
        private Vector2 _PreviousLinearVelocity;
        private int     _NextColliderId;

        private readonly Physics2DCSharp            _Physics2D;
        private readonly Body                       _Body;
        private readonly uint                       _PhysicsStepsPerSec;
        private readonly Dictionary<int, ICollider> _Colliders;

        #endregion Variables

        #region Init Methods

        internal PhysicsObjectCSharp(Physics2DCSharp physics2D, Body body, int myId, uint physicsStepsPerSec)
        {
            _Physics2D          = physics2D;
            _Body               = body;
            PhysicsObjectId     = myId;
            _PhysicsStepsPerSec = physicsStepsPerSec;
            _NextColliderId     = 1;
            _Colliders          = new Dictionary<int, ICollider>();
        }

        #endregion Init Methods

        #region Public Methods

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
            _Body.SetTransform(position, angle);
        }

        public void MovePosition(Vector2 position)
        {
            _MovePositionTarget = position;

            if (_MovePosition) return;

            _PreviousLinearVelocity = _Body.LinearVelocity;
            _MovePosition           = true;
        }

        public void ApplyForce(Vector2 force, Vector2 point)
        {
            _Body.ApplyForce(force, point, true);
        }

        public void ApplyForceToCenter(Vector2 force)
        {
            _Body.ApplyForceToCenter(force, true);
        }

        public void ApplyTorque(float torque)
        {
            _Body.ApplyTorque(torque, true);
        }

        public void ApplyLinearImpulse(Vector2 impulse, Vector2 point)
        {
            _Body.ApplyLinearImpulse(impulse, point, true);
        }

        public void ApplyLinearImpulseToCenter(Vector2 impulse)
        {
            _Body.ApplyLinearImpulseToCenter(impulse, true);
        }

        public void ApplyAngularImpulse(float impulse)
        {
            _Body.ApplyAngularImpulse(impulse, true);
        }

        public Vector2 GetWorldPoint(Vector2 localPoint)
        {
            return _Body.GetWorldPoint(localPoint);
        }

        public Vector2 GetWorldVector(Vector2 localVector)
        {
            return _Body.GetWorldVector(localVector);
        }

        public Vector2 GetLocalPoint(Vector2 worldPoint)
        {
            return _Body.GetLocalPoint(worldPoint);
        }

        public Vector2 GetLocalVector(Vector2 worldVector)
        {
            return _Body.GetLocalVector(worldVector);
        }

        public Vector2 GetLinearVelocityFromWorldPoint(Vector2 worldPoint)
        {
            return _Body.GetLinearVelocityFromWorldPoint(worldPoint);
        }

        public Vector2 GetLinearVelocityFromLocalPoint(Vector2 localPoint)
        {
            return _Body.GetLinearVelocityFromWorldPoint(localPoint);
        }

        internal void UpdateLinearVelocity()
        {
            if (!_MovePosition) return;

            Vector2 position = Position;

            if (position == _MovePositionTarget)
            {
                _MovePosition = false;
                _Body.SetLinearVelocity(_PreviousLinearVelocity);
                return;
            }

            Vector2 moveToPointVelocity = _MovePositionTarget - position;
            moveToPointVelocity *= _PhysicsStepsPerSec;
            _Body.SetLinearVelocity(moveToPointVelocity);
        }

        #endregion Movement, Rotation and Position

        #region Add / Remove Colliders

        public void DestroyCollider(int colliderId)
        {
            if (!_Colliders.ContainsKey(colliderId)) return;

            ColliderCSharp collider = (ColliderCSharp)_Colliders[colliderId];
            _Body.DestroyFixture(collider.Fixture);
            _Colliders.Remove(colliderId);
        }

        public ICollider AddEdgeCollider(Vector2 start, Vector2 end)
        {
            EdgeShape shape = new EdgeShape();
            shape.SetTwoSided(start, end);
            Fixture fixture    = _Body.CreateFixture(shape, 1f);
            int     colliderId = _NextColliderId++;
            fixture.UserData = colliderId;
            ICollider collider = new ColliderCSharp(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddChainCollider(Vector2[] vertices, bool loop)
        {
            ChainShape shape = new ChainShape();

            if (loop)
                shape.CreateLoop(vertices, vertices.Length);
            else
                shape.CreateChain(vertices, vertices.Length, vertices[0], vertices[vertices.Length - 1]);

            Fixture fixture    = _Body.CreateFixture(shape, 1f);
            int     colliderId = _NextColliderId++;
            fixture.UserData = colliderId;
            ICollider collider = new ColliderCSharp(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddBoxCollider(float width, float height, Vector2 offset, float angle, float density)
        {
            Shape   shape      = Physics2DCSharp.GetBoxShape(width, height, offset, angle);
            Fixture fixture    = _Body.CreateFixture(shape, density);
            int     colliderId = _NextColliderId++;
            fixture.UserData = colliderId;
            ICollider collider = new ColliderCSharp(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddBoxCollider(float width, float height, float density = 1f)
        {
            return AddBoxCollider(width, height, Vector2.Zero, 0f, density);
        }

        public ICollider AddCircleCollider(float radius, Vector2 offset, float density)
        {
            Shape   shape      = Physics2DCSharp.GetCircleShape(radius, offset);
            Fixture fixture    = _Body.CreateFixture(shape, density);
            int     colliderId = _NextColliderId++;
            fixture.UserData = colliderId;
            ICollider collider = new ColliderCSharp(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        public ICollider AddCircleCollider(float radius, float density = 1f)
        {
            return AddCircleCollider(radius, Vector2.Zero, density);
        }

        public ICollider AddPolygonCollider(Vector2[] vertices, float density)
        {
            Shape   shape      = Physics2DCSharp.GetPolygonShape(vertices);
            Fixture fixture    = _Body.CreateFixture(shape, density);
            int     colliderId = _NextColliderId++;
            fixture.UserData = colliderId;
            ICollider collider = new ColliderCSharp(fixture, this, colliderId, _Physics2D);
            _Colliders.Add(colliderId, collider);
            return collider;
        }

        #endregion Add / Remove Colliders

        #endregion Public Methods
    }
}