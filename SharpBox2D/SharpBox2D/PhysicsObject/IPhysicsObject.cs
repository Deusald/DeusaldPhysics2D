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
    using System.Collections.Generic;

    public interface IPhysicsObject
    {
        #region Getters

        /// <summary>
        /// Unique Physics Object Id
        /// </summary>
        int PhysicsObjectId { get; }

        /// <summary>
        /// Is this a static body?
        /// A static body does not move under simulation and behaves as if it has infinite mass. Internally,
        /// Box2D stores zero for the mass and the inverse mass. Static bodies can be moved manually by the user.
        /// A static body has zero velocity. Static bodies do not collide with other static or kinematic bodies.
        /// </summary>
        bool IsStatic { get; }

        /// <summary>
        /// Is this a kinematic body?
        /// A kinematic body moves under simulation according to its velocity. Kinematic bodies do not respond to forces.
        /// They can be moved manually by the user, but normally a kinematic body is moved by setting its velocity.
        /// A kinematic body behaves as if it has infinite mass, however, Box2D stores zero for the mass and the inverse mass.
        /// Kinematic bodies do not collide with other kinematic or static bodies.
        /// </summary>
        bool IsKinematic { get; }

        /// <summary>
        /// Is this a dynamic body?
        /// A dynamic body is fully simulated. They can be moved manually by the user, but normally
        /// they move according to forces. A dynamic body can collide with all body types.
        /// A dynamic body always has finite, non-zero mass. If you try to set the mass of
        /// a dynamic body to zero, it will automatically acquire a mass of one kilogram and it won't rotate.
        /// </summary>
        bool IsDynamic { get; }

        /// <summary>
        /// Get the world position of the center of mass. 
        /// </summary>
        Vector2 WorldCenter { get; }

        /// <summary>
        /// Get the local position of the center of mass. 
        /// </summary>
        Vector2 LocalCenter { get; }

        /// <summary>
        /// Get the total mass of the body. (usually in kilograms (kg))
        /// </summary>
        float Mass { get; }

        /// <summary>
        /// Get the rotational inertia of the body about the local origin.
        /// Usually in kg-m^2. 
        /// </summary>
        float Inertia { get; }
        
        /// <summary>
        /// Get the enumerator for all colliders attached to this physical object
        /// </summary>
        Dictionary<int, ICollider>.Enumerator Colliders { get; }

        #endregion Getters

        #region Getters And Setters

        /// <summary>
        /// Get or set the position for the object. This is not a physics move so it can make unphysical behaviours happen!
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Get or set the rotation for the object. This is not a physics move so it can make unphysical behaviours happen!
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// Get or Set the linear velocity of the center of mass. 
        /// </summary>
        Vector2 LinearVelocity { get; set; }

        /// <summary>
        /// Get or Set the angular velocity. 
        /// </summary>
        float AngularVelocity { get; set; }

        /// <summary>
        /// Get or Set the linear damping of the body. 
        /// </summary>
        float LinearDamping { get; set; }

        /// <summary>
        /// Get or Set the angular damping of the body. 
        /// </summary>
        float AngularDamping { get; set; }

        /// <summary>
        /// Get or Set the gravity scale of the body. 
        /// </summary>
        float GravityScale { get; set; }

        /// <summary>
        /// Is this body treated like a bullet for continuous collision detection? (Very fast movement)
        /// </summary>
        bool IsBullet { get; set; }

        /// <summary>
        /// Is this body allowed to sleep. 
        /// </summary>
        bool SleepingAllowed { get; set; }

        /// <summary>
        /// Get or Set the sleep state of the body. A sleeping body has very low CPU cost.
        /// </summary>
        bool IsAwake { get; set; }

        /// <summary>
        /// Set the enabled state of the body. An inactive body is not simulated and cannot be collided with or woken up.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Does this body have fixed rotation? (Can't rotate)
        /// </summary>
        bool FixedRotation { get; set; }

        /// <summary>
        /// The application data for this Physics Object
        /// </summary>
        object UserData { get; set; }

        /// <summary>
        /// Get or set the Physics Object body type
        /// </summary>
        BodyType BodyType { get; set; }

        #endregion Getters And Setters

        #region Methods

        /// <summary>
        /// Destroy this Physics Object and remove it from the world
        /// </summary>
        void Destroy();

        /// <summary>
        /// Get collider attached to this physical object with given id
        /// </summary>
        ICollider GetCollider(int colliderId);

        #region Movement, Rotation and Position

        /// <summary>
        /// Set the position and rotation for this object. This is not a physics move so it can make unphysical behaviours happen!
        /// </summary>
        /// <param name="position">Position for object</param>
        /// <param name="angle">Rotation in radians for object</param>
        void SetTransform(Vector2 position, float angle);

        /// <summary>
        /// Move the object physically to given position using linear velocity.
        /// </summary>
        /// <param name="position"></param>
        void MovePosition(Vector2 position);

        /// <summary>
        /// Apply a force at a world point. If the force is not applied at the center of mass, it will generate a torque and affect the angular velocity. This wakes up the body. 
        /// </summary>
        void ApplyForce(Vector2 force, Vector2 point);

        /// <summary>
        /// Apply a force to the center of mass. This wakes up the body. 
        /// </summary>
        void ApplyForceToCenter(Vector2 force);

        /// <summary>
        /// Apply a torque. This affects the angular velocity without affecting the linear velocity of the center of mass. 
        /// </summary>
        void ApplyTorque(float torque);

        /// <summary>
        /// Apply an impulse at a point. This immediately modifies the velocity.
        /// It also modifies the angular velocity if the point of application is not at the center of mass. This wakes up the body. 
        /// </summary>
        void ApplyLinearImpulse(Vector2 impulse, Vector2 point);

        /// <summary>
        /// Apply an impulse to the center of mass. This immediately modifies the velocity. 
        /// </summary>
        void ApplyLinearImpulseToCenter(Vector2 impulse);

        /// <summary>
        /// Apply an angular impulse.
        /// </summary>
        /// <param name="impulse">The angular impulse in units of kg*m*m/s </param>
        void ApplyAngularImpulse(float impulse);

        /// <summary>
        /// Get the world coordinates of a point given the local coordinates. 
        /// </summary>
        /// <param name="localPoint">A point on the body measured relative the the body's origin. </param>
        Vector2 GetWorldPoint(Vector2 localPoint);

        /// <summary>
        /// Get the world coordinates of a vector given the local coordinates. 
        /// </summary>
        /// <param name="localVector">A vector fixed in the body. </param>
        Vector2 GetWorldVector(Vector2 localVector);

        /// <summary>
        /// Gets a local point relative to the body's origin given a world point. 
        /// </summary>
        /// <param name="worldPoint">A point in world coordinates. </param>
        Vector2 GetLocalPoint(Vector2 worldPoint);

        /// <summary>
        /// Gets a local vector given a world vector. 
        /// </summary>
        /// <param name="worldVector">A vector in world coordinates. </param>
        /// <returns></returns>
        Vector2 GetLocalVector(Vector2 worldVector);

        /// <summary>
        /// Get the world linear velocity of a world point attached to this body. 
        /// </summary>
        Vector2 GetLinearVelocityFromWorldPoint(Vector2 worldPoint);

        /// <summary>
        /// Get the world velocity of a local point. 
        /// </summary>
        /// <param name="localPoint"></param>
        /// <returns></returns>
        Vector2 GetLinearVelocityFromLocalPoint(Vector2 localPoint);

        #endregion Movement, Rotation and Position

        #region Add / Remove Colliders

        /// <summary>
        /// Destroy the attached collider with given Id
        /// </summary>
        void DestroyCollider(int colliderId);
        
        /// <summary>
        /// Attach Edge collider to this PhysicsShape
        /// </summary>
        ICollider AddEdgeCollider(Vector2 start, Vector2 end);

        /// <summary>
        /// Attach Chain collider to this PhysicsShape
        /// </summary>
        ICollider AddChainCollider(Vector2[] vertices, bool loop);

        /// <summary>
        /// Attach Box collider to this PhysicsShape
        /// </summary>
        ICollider AddBoxCollider(float width, float height, Vector2 offset, float angle, float density);

        /// <summary>
        /// Attach Box collider to this PhysicsShape
        /// </summary>
        ICollider AddBoxCollider(float width, float height, float density = 1f);

        /// <summary>
        /// Attach Circle collider to this PhysicsShape
        /// </summary>
        ICollider AddCircleCollider(float radius, Vector2 offset, float density);

        /// <summary>
        /// Attach Circle collider to this PhysicsShape
        /// </summary>
        ICollider AddCircleCollider(float radius, float density = 1f);

        /// <summary>
        /// Attach Polygon collider to this PhysicsShape
        /// </summary>
        ICollider AddPolygonCollider(Vector2[] vertices, float density);

        #endregion Add / Remove Colliders

        #endregion Methods
    }
}