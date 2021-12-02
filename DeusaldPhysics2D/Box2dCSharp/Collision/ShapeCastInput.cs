using Vector2 = DeusaldSharp.Vector2;
using Box2DSharp.Common;

namespace Box2DSharp.Collision
{
    /// Input parameters for b2ShapeCast
    internal struct ShapeCastInput
    {
        public DistanceProxy ProxyA;

        public DistanceProxy ProxyB;

        public Transform TransformA;

        public Transform TransformB;

        public Vector2 TranslationB;
    }
}