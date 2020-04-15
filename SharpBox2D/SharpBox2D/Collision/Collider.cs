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

        #endregion Public Methods
    }
}