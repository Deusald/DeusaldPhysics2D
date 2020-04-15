// MIT License

// SharpBox2D:
// Copyright (c) 2020 Adam "Deusald" Orliński

// Box2D:
// Copyright (c) 2019 Erin Catto

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
    public enum BodyType
    {
        /// <summary>
        /// A static body does not move under simulation and behaves as if it has infinite mass. Internally,
        /// Box2D stores zero for the mass and the inverse mass. Static bodies can be moved manually by the user.
        /// A static body has zero velocity. Static bodies do not collide with other static or kinematic bodies.
        /// </summary>
        Static,
        
        /// <summary>
        /// A kinematic body moves under simulation according to its velocity. Kinematic bodies do not respond to forces.
        /// They can be moved manually by the user, but normally a kinematic body is moved by setting its velocity.
        /// A kinematic body behaves as if it has infinite mass, however, Box2D stores zero for the mass and the inverse mass.
        /// Kinematic bodies do not collide with other kinematic or static bodies.
        /// </summary>
        Kinematic,
        
        /// <summary>
        /// A dynamic body is fully simulated. They can be moved manually by the user, but normally
        /// they move according to forces. A dynamic body can collide with all body types.
        /// A dynamic body always has finite, non-zero mass. If you try to set the mass of
        /// a dynamic body to zero, it will automatically acquire a mass of one kilogram and it won't rotate.
        /// </summary>
        Dynamic
    }
}