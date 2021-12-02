using Vector2 = DeusaldSharp.Vector2;

namespace Box2DSharp.Collision.Collider
{
    /// Ray-cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1).
    internal struct RayCastInput
    {
        public Vector2 P1;

        public Vector2 P2;

        public float MaxFraction;
    }
}