namespace Box2DSharp.Dynamics
{
    /// Profiling data. Times are in milliseconds.
    internal struct Profile
    {
        public float Step;

        public float Collide;

        public float Solve;

        public float SolveInit;

        public float SolveVelocity;

        public float SolvePosition;

        public float Broadphase;

        public float SolveTOI;
    }
}