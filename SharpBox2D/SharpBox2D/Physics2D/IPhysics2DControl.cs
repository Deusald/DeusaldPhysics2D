namespace SharpBox2D
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that describe control interactions over the physics world
    /// </summary>
    public interface IPhysics2DControl : IPhysics2D
    {
        public Dictionary<int, IPhysicsObject>.Enumerator PhysicsObjects { get; }
        Vector2                                           Gravity        { get; set; }
        void                                              Step();
    }
}