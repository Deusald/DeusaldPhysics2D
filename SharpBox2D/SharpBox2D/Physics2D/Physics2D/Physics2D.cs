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

using System;
using System.Collections.Generic;
using Box2D;
using DeusaldSharp;

namespace SharpBox2D
{
    public class Physics2D : IPhysics2D
    {
        #region Types

        public delegate float RayCastCallback(ICollider collider, Vector2 point, Vector2 normal, float fraction);

        public delegate void SingleRayCastCallback(bool hit, Vector2 point, Vector2 normal, float fraction);

        public delegate bool OverlapAreaCallback(ICollider collider);

        public delegate DistanceOutput CalculateDistanceCallback();

        public delegate void OverlapPointCallback(bool hit, CalculateDistanceCallback distanceCallback);

        public delegate bool OverlapShapeCallback(ICollider collider, int childIndex);

        public delegate void SingleOverlapShapeCallback(bool hit, DistanceOutput distanceOutput);

        public delegate bool ShapeCastCallback(ICollider collider, Vector2 point, Vector2 normal, float t, int childIndex);

        public delegate void SingleShapeCastCallback(bool hit, Vector2 point, Vector2 normal, float t);

        public delegate void PreCollisionEvent(ICollisionDataExtend collisionData);

        public delegate void OnCollisionEvent(ICollisionData collisionData);

        #endregion Types

        #region Variables

        public event PreCollisionEvent PreCollision;
        public event OnCollisionEvent  OnCollisionEnter;
        public event OnCollisionEvent  OnCollisionExit;

        private int _NextPhysicsObjectId;

        // ReSharper disable InconsistentNaming

        private protected          Action                          _UpdateLinearVelocity;
        private protected readonly b2World                         _World;
        private protected readonly Dictionary<int, IPhysicsObject> _PhysicsObjects;

        // ReSharper restore InconsistentNaming

        private readonly b2BodyDef _BodyDef;
        private readonly uint      _PhysicsStepsPerSec;
        private readonly Vector2   _PointExtends;

        #endregion Variables

        #region Init Methods

        protected Physics2D(uint physicsStepsPerSec, Vector2 gravity)
        {
            _World               = new b2World(new b2Vec2(gravity.x, gravity.y));
            _PhysicsObjects      = new Dictionary<int, IPhysicsObject>();
            _PhysicsStepsPerSec  = physicsStepsPerSec;
            _NextPhysicsObjectId = 1;
            _PointExtends        = new Vector2((float) Box2d.b2_linearSlop, (float) Box2d.b2_linearSlop);

            _BodyDef = new b2BodyDef
            {
                linearVelocity  = new b2Vec2(0f, 0f),
                angularVelocity = 0f,
                linearDamping   = 0f,
                angularDamping  = 0f,
                allowSleep      = true,
                awake           = true,
                fixedRotation   = false,
                bullet          = false,
                enabled         = true,
                gravityScale    = 1f
            };
        }

        #endregion Init Methods

        #region Public Methods

        public IPhysicsObject CreatePhysicsObject(BodyType bodyType, Vector2 position, float rotation)
        {
            int newId = _NextPhysicsObjectId++;
            _BodyDef.type     = (b2BodyType) bodyType;
            _BodyDef.position = SharpBoxUtils.ConvertToB2Vec(position);
            _BodyDef.angle    = rotation;
            _BodyDef.userData = new IntPtr(newId);

            b2Body        body      = _World.CreateBody(_BodyDef);
            PhysicsObject newObject = new PhysicsObject(this, body, newId, _PhysicsStepsPerSec);
            _UpdateLinearVelocity += newObject.UpdateLinearVelocity;
            _PhysicsObjects.Add(newId, newObject);
            return newObject;
        }

        internal static b2Transform GetNewTransform(Vector2 position, float rotation)
        {
            return new b2Transform(SharpBoxUtils.ConvertToB2Vec(position), new b2Rot(rotation));
        }

        internal static b2Shape GetCircleShape(float radius, Vector2 offset)
        {
            return new b2CircleShape
            {
                m_p      = SharpBoxUtils.ConvertToB2Vec(offset),
                m_radius = radius
            };
        }

        internal static b2Shape GetBoxShape(float width, float height, Vector2 offset, float rotation)
        {
            b2PolygonShape shape = new b2PolygonShape();
            shape.SetAsBox(width, height, SharpBoxUtils.ConvertToB2Vec(offset), rotation);
            return shape;
        }

        internal static b2Shape GetPolygonShape(Vector2[] vertices)
        {
            b2PolygonShape shape = new b2PolygonShape();
            b2Vec2         array = Box2d.new_b2Vec2Array(vertices.Length);

            for (int i = 0; i < vertices.Length; ++i)
                Box2d.b2Vec2Array_setitem(array, i, SharpBoxUtils.ConvertToB2Vec(vertices[i]));

            shape.Set(array, vertices.Length);
            return shape;
        }

        public void DestroyPhysicsObject(int objectId)
        {
            if (!_PhysicsObjects.ContainsKey(objectId)) return;

            PhysicsObject physicsObject = (PhysicsObject) _PhysicsObjects[objectId];
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
            Collider colliderACast = (Collider) colliderA;
            Collider colliderBCast = (Collider) colliderB;
            return GetDistance(colliderACast.Fixture.GetShape(), childIndexA, colliderACast.Fixture.GetBody().GetTransform(),
                colliderBCast.Fixture.GetShape(),                childIndexB, colliderBCast.Fixture.GetBody().GetTransform());
        }

        internal DistanceOutput GetDistance(b2Shape shapeA, int childIndexA, b2Transform transformA, b2Shape shapeB, int childIndexB, b2Transform transformB)
        {
            b2DistanceOutput output = new b2DistanceOutput();
            b2SimplexCache   cache  = new b2SimplexCache {count = 0};
            b2DistanceProxy  proxyA = new b2DistanceProxy();
            proxyA.Set(shapeA, childIndexA);
            b2DistanceProxy proxyB = new b2DistanceProxy();
            proxyB.Set(shapeB, childIndexB);

            b2DistanceInput input = new b2DistanceInput
            {
                proxyA     = proxyA,
                proxyB     = proxyB,
                transformA = transformA,
                transformB = transformB,
                useRadii   = true
            };

            Box2d.b2Distance(output, cache, input);
            DistanceOutput finalOutput =
                new DistanceOutput(output.distance, SharpBoxUtils.ConvertFromB2Vec(output.pointA), SharpBoxUtils.ConvertFromB2Vec(output.pointB));
            return finalOutput;
        }

        public void RayCast(RayCastCallback callback, Vector2 origin, Vector2 end, ushort collisionMask = 0xFFFF)
        {
            RaycastCallback raycastCallback = new RaycastCallback((IPhysics2DControl) this, callback, collisionMask);
            _World.RayCast(raycastCallback, SharpBoxUtils.ConvertToB2Vec(origin), SharpBoxUtils.ConvertToB2Vec(end));
        }

        public void RayCast(RayCastCallback callback, Vector2 origin, Vector2 direction, float distance, ushort collisionMask = 0xFFFF)
        {
            Vector2 endPoint = origin + direction * distance;
            RayCast(callback, origin, endPoint, collisionMask);
        }

        public void OverlapArea(OverlapAreaCallback callback, Vector2 lowerBound, Vector2 upperBound, ushort collisionMask = 0xFFFF)
        {
            OverlapArea(callback, SharpBoxUtils.ConvertToB2Vec(lowerBound), SharpBoxUtils.ConvertToB2Vec(upperBound), collisionMask);
        }

        public void OverlapPoint(OverlapAreaCallback callback, Vector2 point, ushort collisionMask = 0xFFFF)
        {
            b2Vec2      vec2Point      = SharpBoxUtils.ConvertToB2Vec(point);
            b2Transform pointTransform = new b2Transform(vec2Point, new b2Rot(0f));

            OverlapArea(delegate(ICollider collider)
            {
                bool goNext = true;

                ((Collider) collider).OverlapPoint(delegate(bool hit, CalculateDistanceCallback distanceCallback)
                {
                    if (hit)
                        goNext = callback.Invoke(collider);
                }, vec2Point, pointTransform);

                return goNext;
            }, point - _PointExtends, point + _PointExtends, collisionMask);
        }

        public void OverlapShape(OverlapShapeCallback callback, OverlapShapeInput input, ushort collisionMask = 0xFFFF)
        {
            OverlapArea(delegate(ICollider collider)
            {
                bool goNext = true;

                for (int i = 0; i < collider.ChildCount; ++i)
                {
                    int innerI = i;
                    ((Collider) collider).OverlapShape(delegate(bool hit, DistanceOutput output)
                    {
                        if (hit)
                            goNext = goNext && callback.Invoke(collider, innerI);
                    }, input, i);

                    if (!goNext) break;
                }

                return goNext;
            }, input.aabb.lowerBound, input.aabb.upperBound, collisionMask);
        }

        public void ShapeCast(ShapeCastCallback callback, ShapeCastInput input, ushort collisionMask = 0xFFFF)
        {
            OverlapArea(delegate(ICollider collider)
            {
                bool goNext = true;

                for (int i = 0; i < collider.ChildCount; ++i)
                {
                    int innerI = i;

                    ((Collider) collider).ShapeCast(delegate(bool hit, Vector2 point, Vector2 normal, float t)
                    {
                        if (hit)
                            goNext = goNext && callback.Invoke(collider, point, normal, t, innerI);
                    }, input, i);

                    if (!goNext) break;
                }

                return goNext;
            }, input.aabb.lowerBound, input.aabb.upperBound, collisionMask);
        }

        #endregion Public Methods

        #region Private Methods

        private void OverlapArea(OverlapAreaCallback callback, b2Vec2 lowerBound, b2Vec2 upperBound, ushort collisionMask = 0xFFFF)
        {
            OverlapCallback overlapCallback = new OverlapCallback((IPhysics2DControl) this, callback, collisionMask);
            b2AABB aabb = new b2AABB
            {
                lowerBound = lowerBound,
                upperBound = upperBound
            };
            _World.QueryAABB(overlapCallback, aabb);
        }

        #endregion Private Methods
    }
}