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

    /// <summary>
    /// Interface that describe control interactions over the physics world
    /// </summary>
    public interface IPhysics2DControl : IPhysics2D
    {
        /// <summary>
        /// Get the enumerator for all PhysicsObjects that are present in the world
        /// </summary>
        Dictionary<int, IPhysicsObject>.Enumerator PhysicsObjects { get; }
        
        /// <summary>
        /// Get or Set the world gravity
        /// </summary>
        Vector2                                    Gravity        { get; set; }
        
        /// <summary>
        /// Simulate next physics step
        /// </summary>
        void                                       Step();
        
        /// <summary>
        /// Get the PhysicsObject with given Id
        /// </summary>
        IPhysicsObject                             GetPhysicsObject(int physicsObjectId);
    }
}