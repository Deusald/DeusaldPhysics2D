namespace SharpBox2D
{
    using System;
    using System.Collections.Generic;
    using Box2D;

    public class Physics2D : IPhysics2D
    {
        #region Public Methods

        public IPhysicsObject CreatePhysicsObject(BodyType bodyType, Vector2 position, float rotation)
        {
            int newId = _NextPhysicsObjectId++;
            _BodyDef.type     = (b2BodyType) bodyType;
            _BodyDef.position = Vector2.ConvertToB2Vec(position);
            _BodyDef.angle    = rotation;
            _BodyDef.userData = new IntPtr(newId);

            b2Body        body      = __World.CreateBody(_BodyDef);
            PhysicsObject newObject = new PhysicsObject(this, body, newId, _PhysicsStepsPerSec);
            __UpdateLinearVelocity += newObject.UpdateLinearVelocity;
            __PhysicsObjects.Add(newId, newObject);
            return newObject;
        }

        public void DestroyPhysicsObject(int objectId)
        {
            if (!__PhysicsObjects.ContainsKey(objectId)) return;

            PhysicsObject physicsObject = (PhysicsObject) __PhysicsObjects[objectId];
            __UpdateLinearVelocity -= physicsObject.UpdateLinearVelocity;
            __World.DestroyBody(physicsObject.Body);
            __PhysicsObjects.Remove(objectId);
        }

        #endregion Public Methods

        #region Private Variables

        private int _NextPhysicsObjectId;

        private readonly b2BodyDef _BodyDef;
        private readonly uint      _PhysicsStepsPerSec;

        // ReSharper disable InconsistentNaming

        private protected Action __UpdateLinearVelocity;

        private protected readonly b2World                         __World;
        private protected readonly Dictionary<int, IPhysicsObject> __PhysicsObjects;

        // ReSharper restore InconsistentNaming

        #endregion Private Variables

        #region Private Methods

        protected Physics2D(uint physicsStepsPerSec, Vector2 gravity)
        {
            __World              = new b2World(new b2Vec2(gravity.x, gravity.y));
            __PhysicsObjects     = new Dictionary<int, IPhysicsObject>();
            _PhysicsStepsPerSec  = physicsStepsPerSec;
            _NextPhysicsObjectId = 1;

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

        #endregion Private Methods
    }
}