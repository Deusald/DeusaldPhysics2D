using DeusaldSharp;

namespace DeusaldPhysics2D
{
    public static class Delegates
    {
        public delegate float RayCastCallback(ICollider collider, Vector2 point, Vector2 normal, float fraction);

        public delegate void SingleRayCastCallback(bool hit, Vector2 point, Vector2 normal, float fraction);

        public delegate bool OverlapAreaCallback(ICollider collider);

        public delegate DistanceOutput CalculateDistanceCallback();

        public delegate void OverlapPointCallback(bool hit, CalculateDistanceCallback distanceCallback);

        public delegate bool OverlapShapeCallback(ICollider collider, int childIndex);

        public delegate void SingleOverlapShapeCallback(bool hit, DistanceOutput distanceOutput);

        public delegate bool ShapeCastCallback(ICollider collider, Vector2 point, Vector2 normal, float t, int childIndex);

        public delegate void SingleShapeCastCallback(bool hit, Vector2 point, Vector2 normal, float t);

        public delegate void PreCollisionEvent(ICollisionDataExtend collisionData);

        public delegate void OnCollisionEvent(ICollisionData collisionData);
    }
}