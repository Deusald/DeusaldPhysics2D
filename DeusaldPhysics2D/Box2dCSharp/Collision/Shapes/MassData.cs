using Vector2 = DeusaldSharp.Vector2;

namespace Box2DSharp.Collision.Shapes
{
    internal struct MassData
    {
        /// The mass of the shape, usually in kilograms.
        public float Mass;

        /// The position of the shape's centroid relative to the shape's origin.
        public Vector2 Center;

        /// The rotational inertia of the shape about the local origin.
        public float RotationInertia;
    }
}