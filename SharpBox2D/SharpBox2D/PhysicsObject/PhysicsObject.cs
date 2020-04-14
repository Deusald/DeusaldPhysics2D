namespace SharpBox2D
{
    using System;
    using Box2D;

    public class PhysicsObject : IPhysicsObject
    {
        #region Public Variables

        #region Getters

        public   int     PhysicsObjectId { get; }
        public   bool    IsStatic        => _Body.Type == b2BodyType.b2_staticBody;
        public   bool    IsKinematic     => _Body.Type == b2BodyType.b2_kinematicBody;
        public   bool    IsDynamic       => _Body.Type == b2BodyType.b2_dynamicBody;
        public   Vector2 WorldCenter     => Vector2.ConvertFromB2Vec(_Body.GetWorldCenter());
        public   Vector2 LocalCenter     => Vector2.ConvertFromB2Vec(_Body.GetLocalCenter());
        public   float   Mass            => _Body.GetMass();
        public   float   Inertia         => _Body.GetInertia();
        internal b2Body  Body            => _Body;

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
        }

        public void Destroy()
        {
            _Physics2D.DestroyPhysicsObject(PhysicsObjectId);
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

        #endregion Public Methods

        #region Private Variables

        private Vector2 _MovePositionTarget;
        private bool    _MovePosition;
        private Vector2 _PreviousLinearVelocity;

        private readonly Physics2D _Physics2D;
        private readonly b2Body    _Body;
        private readonly uint      _PhysicsStepsPerSec;

        #endregion Private Variables
    }
}