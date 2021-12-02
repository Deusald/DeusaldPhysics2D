using Vector2 = DeusaldSharp.Vector2;

namespace Box2DSharp.Collision.Collider
{
    /// Used for computing contact manifolds.
    internal struct ClipVertex
    {
        public Vector2 Vector;

        public ContactId Id;
    }
}