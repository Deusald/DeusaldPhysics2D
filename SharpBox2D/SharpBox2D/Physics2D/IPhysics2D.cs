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
    /// <summary>
    /// Interface that describes basic interaction with physics engine
    /// </summary>
    public interface IPhysics2D
    {
        public delegate void PreCollisionEvent(ICollisionDataExtend collisionData);

        public delegate void OnCollisionEvent(ICollisionData collisionData);

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
        event PreCollisionEvent PreCollision;
        
        /// <summary>
        /// This event will be triggered in the first frame of collision
        /// </summary>
        event OnCollisionEvent  OnCollisionEnter;
        
        /// <summary>
        /// This event will be triggered at the moment when the colliders stop colliding
        /// Will be also triggered when colliders were colliding and one of them has been destroyed
        /// </summary>
        event OnCollisionEvent  OnCollisionExit;

        #region Overlap Tests

        #endregion Overlap Tests
    }
}