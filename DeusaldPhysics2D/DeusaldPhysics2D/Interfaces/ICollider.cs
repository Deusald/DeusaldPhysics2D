// MIT License

// DeusaldPhysics2D:
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

using DeusaldSharp;

namespace DeusaldPhysics2D
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
        event Delegates.OnCollisionEvent OnCollisionEnter;

        /// <summary>
        /// This event will be triggered at the moment when the colliders stop colliding
        /// Will be also triggered when colliders were colliding and one of them has been destroyed
        /// The ColliderA is always this collider
        /// </summary>
        event Delegates.OnCollisionEvent OnCollisionExit;

        /// <summary>
        /// RayCast this collider
        /// </summary>
        void RayCast(Delegates.SingleRayCastCallback callback, Vector2 origin, Vector2 end, int childIndex = 0);

        /// <summary>
        /// RayCast this collider
        /// </summary>
        void RayCast(Delegates.SingleRayCastCallback callback, Vector2 origin, Vector2 direction, float distance, int childIndex = 0);

        /// <summary>
        /// Test if this collider aabb overlap with given aabb
        /// </summary>
        bool OverlapArea(Vector2 lowerBound, Vector2 upperBound, int childIndex = 0);

        /// <summary>
        /// Test if given point is inside the collider
        /// CalculateDistanceCallback should be called only when there was no hit
        /// </summary>
        void OverlapPoint(Delegates.OverlapPointCallback callback, Vector2 point, int childIndex = 0);

        /// <summary>
        /// Test if given collider would overlap with given shape
        /// CalculateDistanceCallback should be called only when there was no hit
        /// </summary>
        void OverlapShape(Delegates.SingleOverlapShapeCallback callback, IOverlapShapeInput input, int childIndex = 0);

        /// <summary>
        /// Shoot a shape in given direction and return if it would collide with this collider
        /// </summary>
        void ShapeCast(Delegates.SingleShapeCastCallback callback, IShapeCastInput input, int childIndex = 0);
    }
}