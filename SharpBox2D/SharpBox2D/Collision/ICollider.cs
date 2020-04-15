namespace SharpBox2D
{
    public interface ICollider
    {
        ShapeType      ShapeType     { get; }
        IPhysicsObject PhysicsObject { get; }
        int            ColliderId    { get; }
        int            ChildCount    { get; }
        bool           IsSensor      { get; set; }
        ushort         CategoryBits  { get; set; }
        ushort         MaskBits      { get; set; }
        short          GroupIndex    { get; set; }
        float          Density       { get; set; }
        float          Friction      { get; set; }
        float          Restitution   { get; set; }
        object         UserData      { get; set; }

        void Destroy();
    }
}