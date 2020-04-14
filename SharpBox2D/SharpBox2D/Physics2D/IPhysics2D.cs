namespace SharpBox2D
{
    /// <summary>
    /// Interface that describes basic interaction with physics engine
    /// </summary>
    public interface IPhysics2D
    {
        /// <summary>
        /// Create new physics object in the world
        /// </summary>
        IPhysicsObject CreatePhysicsObject(BodyType bodyType, Vector2 position, float rotation);

        /// <summary>
        /// Destroy the physics object with given id
        /// </summary>
        void DestroyPhysicsObject(int objectId);

        #region Overlap Tests



        #endregion Overlap Tests
    }
}