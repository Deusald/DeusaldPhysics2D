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

using System;
using System.Collections.Generic;
using Box2DSharp.Collision;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    // ReSharper disable once InconsistentNaming
    internal class Physics2DCSharp : IPhysics2DControl
    {
        #region Properties

        public Vector2 Gravity
        {
            get
            {
                Vector2 gravity = _World.Gravity;
                return new Vector2(gravity.x, gravity.y);
            }
            set => _World.Gravity = new Vector2(value.x, value.y);
        }

        public Dictionary<int, IPhysicsObject>.Enumerator PhysicsObjects => _PhysicsObjects.GetEnumerator();

        #endregion Properties

        #region Variables

        public event Delegates.PreCollisionEvent PreCollision;
        public event Delegates.OnCollisionEvent  OnCollisionEnter;
        public event Delegates.OnCollisionEvent  OnCollisionExit;

        private int     _NextPhysicsObjectId;
        private BodyDef _BodyDef;

        private readonly float _PhysicsTimeStep;

        private const int _VelocityIterations = 8;
        private const int _PositionIterations = 3;

        private Action _UpdateLinearVelocity;

        private readonly World                           _World;
        private readonly Dictionary<int, IPhysicsObject> _PhysicsObjects;
        private readonly uint                            _PhysicsStepsPerSec;
        private readonly Vector2                         _PointExtends;

        #endregion Variables

        #region Init Methods

        internal Physics2DCSharp(uint physicsStepsPerSec, Vector2 gravity)
        {
            _World               = new World(new Vector2(gravity.x, gravity.y));
            _PhysicsObjects      = new Dictionary<int, IPhysicsObject>();
            _PhysicsStepsPerSec  = physicsStepsPerSec;
            _NextPhysicsObjectId = 1;
            _PointExtends        = new Vector2(Settings.LinearSlop, Settings.LinearSlop);
            _PhysicsTimeStep     = 1f / physicsStepsPerSec;
            _World.SetContactListener(new CollisionListenerCSharp(this));

            _BodyDef = new BodyDef
            {
                LinearVelocity  = new Vector2(0f, 0f),
                AngularVelocity = 0f,
                LinearDamping   = 0f,
                AngularDamping  = 0f,
                AllowSleep      = true,
                Awake           = true,
                FixedRotation   = false,
                Bullet          = false,
                Enabled         = true,
                GravityScale    = 1f
            };
        }

        #endregion Init Methods

        #region Public Methods

        public IOverlapShapeInput GetNewOverlapShapeInput()
        {
            return new OverlapShapeInputCSharp();
        }

        public IShapeCastInput GetNewShapeCastInput()
        {
            return new ShapeCastInputCSharp();
        }

        public void Step()
        {
            _UpdateLinearVelocity?.Invoke();
            _World.Step(_PhysicsTimeStep, _VelocityIterations, _PositionIterations);
        }

        public IPhysicsObject GetPhysicsObject(int physicsObjectId)
        {
            return !_PhysicsObjects.ContainsKey(physicsObjectId) ? null : _PhysicsObjects[physicsObjectId];
        }

        public IPhysicsObject CreatePhysicsObject(BodyType bodyType, Vector2 position, float rotation)
        {
            int newId = _NextPhysicsObjectId++;
            _BodyDef.BodyType = (Box2DSharp.Dynamics.BodyType)bodyType;
            _BodyDef.Position = position;
            _BodyDef.Angle    = rotation;
            _BodyDef.UserData = newId;

            Body                body      = _World.CreateBody(_BodyDef);
            PhysicsObjectCSharp newObject = new PhysicsObjectCSharp(this, body, newId, _PhysicsStepsPerSec);
            _UpdateLinearVelocity += newObject.UpdateLinearVelocity;
            _PhysicsObjects.Add(newId, newObject);
            return newObject;
        }

        internal static Transform GetNewTransform(Vector2 position, float rotation)
        {
            return new Transform(position, new Rotation(rotation));
        }

        internal static Shape GetCircleShape(float radius, Vector2 offset)
        {
            return new CircleShape
            {
                Position = offset,
                Radius   = radius
            };
        }

        internal static Shape GetBoxShape(float width, float height, Vector2 offset, float rotation)
        {
            PolygonShape shape = new PolygonShape();
            shape.SetAsBox(width, height, offset, rotation);
            return shape;
        }

        internal static Shape GetPolygonShape(Vector2[] vertices)
        {
            PolygonShape shape = new PolygonShape();
            shape.Set(vertices, vertices.Length);
            return shape;
        }

        public void DestroyPhysicsObject(int objectId)
        {
            if (!_PhysicsObjects.ContainsKey(objectId)) return;

            PhysicsObjectCSharp physicsObject = (PhysicsObjectCSharp)_PhysicsObjects[objectId];
            _UpdateLinearVelocity -= physicsObject.UpdateLinearVelocity;
            _World.DestroyBody(physicsObject.Body);
            _PhysicsObjects.Remove(objectId);
        }

        internal void ExecutePreCollision(ICollisionDataExtend collisionDataExtend)
        {
            PreCollision?.Invoke(collisionDataExtend);
        }

        internal void ExecuteOnCollisionEnter(ICollisionData collisionData)
        {
            OnCollisionEnter?.Invoke(collisionData);
        }

        internal void ExecuteOnCollisionExit(ICollisionData collisionData)
        {
            OnCollisionExit?.Invoke(collisionData);
        }

        public DistanceOutput GetDistanceBetweenColliders(ICollider colliderA, ICollider colliderB, int childIndexA = 0, int childIndexB = 0)
        {
            ColliderCSharp colliderACast = (ColliderCSharp)colliderA;
            ColliderCSharp colliderBCast = (ColliderCSharp)colliderB;
            return GetDistance(colliderACast.Fixture.Shape, childIndexA, colliderACast.Fixture.Body.GetTransform(),
                colliderBCast.Fixture.Shape, childIndexB, colliderBCast.Fixture.Body.GetTransform());
        }

        internal DistanceOutput GetDistance(Shape shapeA, int childIndexA, Transform transformA, Shape shapeB, int childIndexB, Transform transformB)
        {
            SimplexCache   cache  = new SimplexCache { Count = 0 };
            DistanceProxy  proxyA = new DistanceProxy();
            proxyA.Set(shapeA, childIndexA);
            DistanceProxy proxyB = new DistanceProxy();
            proxyB.Set(shapeB, childIndexB);

            DistanceInput input = new DistanceInput
            {
                ProxyA     = proxyA,
                ProxyB     = proxyB,
                TransformA = transformA,
                TransformB = transformB,
                UseRadii   = true
            };

            DistanceAlgorithm.Distance(out Box2DSharp.Collision.DistanceOutput output, ref cache, input);
            DistanceOutput finalOutput =
                new DistanceOutput(output.Distance, output.PointA, output.PointB);
            return finalOutput;
        }

        public void RayCast(Delegates.RayCastCallback callback, Vector2 origin, Vector2 end, ushort collisionMask = 0xFFFF)
        {
            RaycastCallbackCSharp raycastCallback = new RaycastCallbackCSharp(this, callback, collisionMask);
            _World.RayCast(raycastCallback, origin, end);
        }

        public void RayCast(Delegates.RayCastCallback callback, Vector2 origin, Vector2 direction, float distance, ushort collisionMask = 0xFFFF)
        {
            Vector2 endPoint = origin + direction * distance;
            RayCast(callback, origin, endPoint, collisionMask);
        }

        public void OverlapArea(Delegates.OverlapAreaCallback callback, Vector2 lowerBound, Vector2 upperBound, ushort collisionMask = 0xFFFF)
        {
            OverlapAreaInternal(callback, lowerBound, upperBound, collisionMask);
        }

        public void OverlapPoint(Delegates.OverlapAreaCallback callback, Vector2 point, ushort collisionMask = 0xFFFF)
        {
            Transform pointTransform = new Transform(point, new Rotation(0f));

            OverlapArea(delegate(ICollider collider)
            {
                bool goNext = true;

                ((ColliderCSharp)collider).OverlapPoint(delegate(bool hit, Delegates.CalculateDistanceCallback distanceCallback)
                {
                    if (hit)
                        goNext = callback.Invoke(collider);
                }, point, pointTransform);

                return goNext;
            }, point - _PointExtends, point + _PointExtends, collisionMask);
        }

        public void OverlapShape(Delegates.OverlapShapeCallback callback, IOverlapShapeInput input, ushort collisionMask = 0xFFFF)
        {
            OverlapShapeInputCSharp overlapShapeInputUnpacked = (OverlapShapeInputCSharp)input;

            OverlapAreaInternal(delegate(ICollider collider)
            {
                bool goNext = true;

                for (int i = 0; i < collider.ChildCount; ++i)
                {
                    int innerI = i;
                    ((ColliderCSharp)collider).OverlapShape(delegate(bool hit, DistanceOutput output)
                    {
                        if (hit)
                            goNext = goNext && callback.Invoke(collider, innerI);
                    }, input, i);

                    if (!goNext) break;
                }

                return goNext;
            }, overlapShapeInputUnpacked.aabb.LowerBound, overlapShapeInputUnpacked.aabb.UpperBound, collisionMask);
        }

        public void ShapeCast(Delegates.ShapeCastCallback callback, IShapeCastInput input, ushort collisionMask = 0xFFFF)
        {
            ShapeCastInputCSharp overlapShapeInputUnpacked = (ShapeCastInputCSharp)input;

            OverlapAreaInternal(delegate(ICollider collider)
            {
                bool goNext = true;

                for (int i = 0; i < collider.ChildCount; ++i)
                {
                    int innerI = i;

                    ((ColliderCSharp)collider).ShapeCast(delegate(bool hit, Vector2 point, Vector2 normal, float t)
                    {
                        if (hit)
                            goNext = goNext && callback.Invoke(collider, point, normal, t, innerI);
                    }, input, i);

                    if (!goNext) break;
                }

                return goNext;
            }, overlapShapeInputUnpacked.aabb.LowerBound, overlapShapeInputUnpacked.aabb.UpperBound, collisionMask);
        }

        #endregion Public Methods

        #region Private Methods

        private void OverlapAreaInternal(Delegates.OverlapAreaCallback callback, Vector2 lowerBound, Vector2 upperBound, ushort collisionMask = 0xFFFF)
        {
            OverlapCallbackCSharp overlapCallback = new OverlapCallbackCSharp(this, callback, collisionMask);
            AABB aabb = new AABB
            {
                LowerBound = lowerBound,
                UpperBound = upperBound
            };
            _World.QueryAABB(overlapCallback, aabb);
        }

        #endregion Private Methods
    }
}