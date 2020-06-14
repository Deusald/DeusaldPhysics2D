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

using DeusaldSharp;

namespace SharpBox2D
{
    public readonly struct DistanceOutput
    {
        /// <summary>
        /// Distance between shapes
        /// </summary>
        public readonly float distance;

        /// <summary>
        /// Closest point on shape A
        /// </summary>
        public readonly Vector2 pointA;

        /// <summary>
        /// Closest point on shape B
        /// </summary>
        public readonly Vector2 pointB;

        public DistanceOutput(float distance, Vector2 pointA, Vector2 pointB)
        {
            this.distance = distance;
            this.pointA   = pointA;
            this.pointB   = pointB;
        }
    }
}