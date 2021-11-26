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

using System;
using Box2D;
using DeusaldSharp;

namespace DeusaldPhysics2D
{
    public static class DeusaldPhysics2D
    {
        public static bool IsInitialized { get; internal set; }
        
        public static readonly Version Version = new Version(0, 9, 1);

        public static IPhysics2DControl CreateNewPhysics(uint physicsStepsPerSec, Vector2 gravity)
        {
            if (!IsInitialized)
            {
                throw new Exception("Can't create new physics if plugin is not initialized. Execute Box2dNativeLoader method first.");
            }

            return new Physics2D(physicsStepsPerSec, gravity);
        }

        internal static Vector2 ToVector2(this b2Vec2 vec2)
        {
            return new Vector2(vec2.x, vec2.y);
        }

        internal static b2Vec2 ToB2Vec2(this Vector2 vector2)
        {
            return new b2Vec2(vector2.x, vector2.y);
        }
    }
}