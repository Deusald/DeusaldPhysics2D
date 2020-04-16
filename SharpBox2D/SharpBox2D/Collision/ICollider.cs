// MIT License

// SharpBox2D:
// Copyright (c) 2020 Adam "Deusald" Orliński

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

namespace SharpBox2D
{
    public interface ICollider
    {
        /// <summary>
        /// Get the ShapeType of this collider
        /// </summary>
        ShapeType ShapeType { get; }

        /// <summary>
        /// Get the PhysicsObject that this collider is attached to
        /// </summary>
        IPhysicsObject PhysicsObject { get; }

        /// <summary>
        /// Get the unique collider Id (uniques in the sphere of the attached PhysicsObject)
        /// </summary>
        int ColliderId { get; }

        /// <summary>
        /// Get the amount of child shapes
        /// (Collider shapes like chain contains child shapes as edges)
        /// </summary>
        int ChildCount { get; }

        /// <summary>
        /// Is this collider a sensor
        /// Sensor is a collider that have no collision response (it's like a ghost, that detects other shapes)
        /// </summary>
        bool IsSensor { get; set; }

        /// <summary>
        /// Binary flag in which category this collider is in
        /// Box2D supports 16 collision categories. For each fixture you can specify which category it belongs to.
        /// You also specify what other categories this fixture can collide with.
        /// </summary>
        ushort CategoryBits { get; set; }

        /// <summary>
        /// Binary flags with which categories this collider can collide
        /// Box2D supports 16 collision categories. For each fixture you can specify which category it belongs to.
        /// You also specify what other categories this fixture can collide with.
        /// </summary>
        ushort MaskBits { get; set; }

        /// <summary>
        /// Collision groups let you specify an integral group index. You can have all fixtures with the same group
        /// index always collide (positive index) or never collide (negative index).
        /// </summary>
        short GroupIndex { get; set; }

        /// <summary>
        /// The density of the collider
        /// </summary>
        float Density { get; set; }

        /// <summary>
        /// The friction of the collider
        /// </summary>
        float Friction { get; set; }

        /// <summary>
        /// The restitution of the collider
        /// </summary>
        float Restitution { get; set; }

        /// <summary>
        /// The application data for this Collider
        /// </summary>
        object UserData { get; set; }

        /// <summary>
        /// Destroy this collider
        /// </summary>
        void Destroy();

        /// <summary>
        /// This event will be triggered in the first frame of collision
        /// The ColliderA is always this collider
        /// </summary>
        event IPhysics2D.OnCollisionEvent OnCollisionEnter;

        /// <summary>
        /// This event will be triggered at the moment when the colliders stop colliding
        /// Will be also triggered when colliders were colliding and one of them has been destroyed
        /// The ColliderA is always this collider
        /// </summary>
        event IPhysics2D.OnCollisionEvent OnCollisionExit;

        /// <summary>
        /// RayCast this collider
        /// </summary>
        void RayCast(IPhysics2D.SingleRayCastCallback callback, Vector2 origin, Vector2 end, int childIndex = 0);

        /// <summary>
        /// RayCast this collider
        /// </summary>
        void RayCast(IPhysics2D.SingleRayCastCallback callback, Vector2 origin, Vector2 direction, float distance, int childIndex = 0);

        /// <summary>
        /// Test if this collider aabb overlap with given aabb
        /// </summary>
        bool OverlapArea(Vector2 lowerBound, Vector2 upperBound, int childIndex = 0);

        /// <summary>
        /// Test if given point is inside the collider
        /// CalculateDistanceCallback should be called only when there was no hit
        /// </summary>
        void OverlapPoint(IPhysics2D.OverlapPointCallback callback, Vector2 point, int childIndex = 0);

        /// <summary>
        /// Test if given collider would overlap with given box shape
        /// CalculateDistanceCallback should be called only when there was no hit
        /// </summary>
        void OverlapBox(IPhysics2D.SingleOverlapShapeCallback callback, float width, float height, Vector2 position, float rotation, int childIndex = 0);

        /// <summary>
        /// Test if given collider would overlap with given circle shape
        /// CalculateDistanceCallback should be called only when there was no hit
        /// </summary>
        void OverlapCircle(IPhysics2D.SingleOverlapShapeCallback callback, float radius, Vector2 position, int childIndex = 0);

        /// <summary>
        /// Test if given collider would overlap with given polygon shape
        /// CalculateDistanceCallback should be called only when there was no hit
        /// </summary>
        void OverlapPolygon(IPhysics2D.SingleOverlapShapeCallback callback, Vector2[] vertices, Vector2 position, float rotation, int childIndex = 0);

        /// <summary>
        /// Shoot a circle in given direction and return if it would collide with this collider
        /// </summary>
        void CircleCast(IPhysics2D.SingleShapeCast callback, Vector2 origin, float radius, Vector2 direction, float distance, int childIndex = 0);

        /// <summary>
        /// Shoot a circle that will move from origin to end and return if it would collide with this collider
        /// </summary>
        void CircleCast(IPhysics2D.SingleShapeCast callback, Vector2 origin, float radius, Vector2 end, int childIndex = 0);

        /// <summary>
        /// Shoot a box in given direction and return if it would collide with this collider
        /// </summary>
        void BoxCast(IPhysics2D.SingleShapeCast callback, float width, float height, Vector2 origin, float rotation, Vector2 direction, float distance, int childIndex = 0);

        /// <summary>
        /// Shoot a box that will move from origin to end and return if it would collide with this collider
        /// </summary>
        void BoxCast(IPhysics2D.SingleShapeCast callback, float width, float height, Vector2 origin, float rotation, Vector2 end, int childIndex = 0);

        /// <summary>
        /// Shoot a polygon in given direction and return if it would collide with this collider
        /// </summary>
        void PolygonCast(IPhysics2D.SingleShapeCast callback, Vector2[] vertices, Vector2 origin, float rotation, Vector2 direction, float distance, int childIndex = 0);
        
        /// <summary>
        /// Shoot a polygon that will move from origin to end and return if it would collide with this collider
        /// </summary>
        void PolygonCast(IPhysics2D.SingleShapeCast callback, Vector2[] vertices, Vector2 origin, float rotation, Vector2 end, int childIndex = 0);
    }
}