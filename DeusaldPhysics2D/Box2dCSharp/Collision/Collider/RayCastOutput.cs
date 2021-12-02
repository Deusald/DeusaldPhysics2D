using Vector2 = DeusaldSharp.Vector2;

namespace Box2DSharp.Collision.Collider
{
    /// Ray-cast output data. The ray hits at p1 + fraction * (p2 - p1), where p1 and p2
    /// come from b2RayCastInput.
    internal struct RayCastOutput
    {
        public Vector2 Normal;

        public float Fraction;
    }
}