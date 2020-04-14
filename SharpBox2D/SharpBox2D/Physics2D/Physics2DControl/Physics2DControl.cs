namespace SharpBox2D
{
    using System.Collections.Generic;
    using Box2D;

    public class Physics2DControl : Physics2D, IPhysics2DControl
    {
        #region Public Variables

        public Vector2 Gravity
        {
            get
            {
                b2Vec2 gravity = __World.GetGravity();
                return new Vector2(gravity.x, gravity.y);
            }
            set => __World.SetGravity(new b2Vec2(value.x, value.y));
        }

        public Dictionary<int, IPhysicsObject>.Enumerator PhysicsObjects => __PhysicsObjects.GetEnumerator();

        #endregion Public Variables

        #region Public Methods

        public Physics2DControl(uint physicsStepsPerSec, Vector2 gravity) : base(physicsStepsPerSec, gravity)
        {
            _PhysicsTimeStep = 1f / physicsStepsPerSec;
        }

        public void Step()
        {
            __UpdateLinearVelocity?.Invoke();
            __World.Step(_PhysicsTimeStep, _VelocityIterations, _PositionIterations);
        }

        #endregion Public Methods

        #region Private Variables

        private readonly float _PhysicsTimeStep;

        private const int _VelocityIterations = 8;
        private const int _PositionIterations = 3;

        #endregion Private Variables
    }
}