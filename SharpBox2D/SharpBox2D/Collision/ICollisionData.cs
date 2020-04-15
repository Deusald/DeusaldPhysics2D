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
    public interface ICollisionData
    {
        /// <summary>
        /// First object that participates in collision
        /// </summary>
        IPhysicsObject PhysicsObjectA { get; }

        /// <summary>
        /// First collider that participates in collision
        /// Belongs to PhysicsObjectA
        /// </summary>
        ICollider ColliderA { get; }

        /// <summary>
        /// Child index of shape from colliderA that participates in collision
        /// </summary>
        int ChildIndexA { get; }

        /// <summary>
        /// Second object that participates in collision
        /// </summary>
        IPhysicsObject PhysicsObjectB { get; }

        /// <summary>
        /// Second collider that participates in collision
        /// Belongs to PhysicsObjectB
        /// </summary>
        ICollider ColliderB { get; }

        /// <summary>
        /// Child index of shape from colliderA that participates in collision
        /// </summary>
        int ChildIndexB { get; }

        /// <summary>
        /// Points where colliders have contact
        /// </summary>
        TypeVector2<Vector2> ContactPoints { get; }
        
        /// <summary>
        /// Normal vector of collision form ColliderA to ColliderB
        /// </summary>
        Vector2       Normal        { get; }
    }
}