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
    /// <summary>
    /// Interface that describes basic interaction with physics engine
    /// </summary>
    public interface IPhysics2D
    {
        /// <summary>
        /// Create new input for overlap shape method
        /// </summary>
        IOverlapShapeInput GetNewOverlapShapeInput();

        /// <summary>
        /// Create new input for shape cast method
        /// </summary>
        IShapeCastInput GetNewShapeCastInput();
        
        /// <summary>
        /// Create new physics object in the world
        /// </summary>
        IPhysicsObject CreatePhysicsObject(BodyType bodyType, Vector2 position, float rotation);

        /// <summary>
        /// Destroy the physics object with given id
        /// </summary>
        void DestroyPhysicsObject(int objectId);

        /// <summary>
        /// This event will be triggered before the collision happen
        /// You have opportunity to disable the collision
        /// For example by making one side platform
        /// </summary>
        event Delegates.PreCollisionEvent PreCollision;

        /// <summary>
        /// This event will be triggered in the first frame of collision
        /// </summary>
        event Delegates.OnCollisionEvent OnCollisionEnter;

        /// <summary>
        /// This event will be triggered at the moment when the colliders stop colliding
        /// Will be also triggered when colliders were colliding and one of them has been destroyed
        /// </summary>
        event Delegates.OnCollisionEvent OnCollisionExit;

        /// <summary>
        /// Return the distance and closest points between two colliders
        /// </summary>
        DistanceOutput GetDistanceBetweenColliders(ICollider colliderA, ICollider colliderB, int childIndexA = 0, int childIndexB = 0);

        /// <summary>
        /// Raycast the world
        /// </summary>
        /// <param name="origin">The start point</param>
        /// <param name="end">The end point of ray</param>
        /// <param name="collisionMask">Mask for colliders</param>
        /// <param name="callback">The callback for returning result.
        /// return -1: ignore this collider and continue;
        /// return 0: terminate the ray cast;
        /// return fraction: clip the ray to this point;
        /// return 1: don't clip the ray and continue;
        /// </param>
        void RayCast(Delegates.RayCastCallback callback, Vector2 origin, Vector2 end, ushort collisionMask = 0xFFFF);

        /// <summary>
        /// Raycast the world
        /// </summary>
        /// <param name="origin">The start point</param>
        /// <param name="direction">Ray direction - should be normalized</param>
        /// <param name="distance">Distance of ray</param>
        /// <param name="collisionMask">Mask for colliders</param>
        /// <param name="callback">The callback for returning result.
        /// return -1: ignore this collider and continue;
        /// return 0: terminate the ray cast;
        /// return fraction: clip the ray to this point;
        /// return 1: don't clip the ray and continue;
        /// </param>
        void RayCast(Delegates.RayCastCallback callback, Vector2 origin, Vector2 direction, float distance, ushort collisionMask = 0xFFFF);

        /// <summary>
        /// Get colliders that are in defined area
        /// In callback return false to end the search
        /// </summary>
        void OverlapArea(Delegates.OverlapAreaCallback callback, Vector2 lowerBound, Vector2 upperBound, ushort collisionMask = 0xFFFF);

        /// <summary>
        /// Get all colliders that have given points inside their shape
        /// In callback return false to end the search
        /// </summary>
        void OverlapPoint(Delegates.OverlapAreaCallback callback, Vector2 point, ushort collisionMask = 0xFFFF);

        /// <summary>
        /// Get all colliders that would collide with defined shape
        /// In callback return false to end the search
        /// </summary>
        void OverlapShape(Delegates.OverlapShapeCallback callback, IOverlapShapeInput input, ushort collisionMask = 0xFFFF);

        /// <summary>
        /// Shoot a shape in given direction and return in callback all hit colliders
        /// </summary>
        void ShapeCast(Delegates.ShapeCastCallback callback, IShapeCastInput input, ushort collisionMask = 0xFFFF);

        /// <summary>
        /// Get AABB of given circle shape at given position
        /// </summary>
        AABB GetAABBOfCircleShape(float radius, Vector2 position);

        /// <summary>
        /// Get AABB of given box shape at given position and rotation
        /// </summary>
        AABB GetAABBOfBoxShape(float width, float height, Vector2 position, float rotation);

        /// <summary>
        /// Get AABB of given polygon shape at given position and rotation
        /// </summary>
        AABB GetAABBOfPolygonShape(Vector2[] vertices, Vector2 position, float rotation);
    }
}