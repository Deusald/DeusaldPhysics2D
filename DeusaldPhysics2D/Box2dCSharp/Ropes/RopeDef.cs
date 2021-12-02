using Vector2 = DeusaldSharp.Vector2;

namespace Box2DSharp.Ropes
{
    /// 
    internal struct RopeDef
    {
        public Vector2 Position;

        public Vector2[] Vertices;

        public int Count;

        public float[] Masses;

        public Vector2 Gravity;

        public RopeTuning Tuning;
    };
}