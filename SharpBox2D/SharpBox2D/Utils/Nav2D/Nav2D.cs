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
    using System;

    public class Nav2D
    {
        #region Public Methods

        /// <summary>
        /// Create new nav2d world
        /// </summary>
        /// <param name="lowerWorldBound">Bottom left boundary of the world</param>
        /// <param name="upperWorldBound">Top right boundary of the world</param>
        /// <param name="polygonAccuracy">How many sub-pieces should polygons approximation have. It must be an odd number</param>
        /// <param name="agentsRadius"></param>
        public Nav2D(Vector2 lowerWorldBound, Vector2 upperWorldBound, int polygonAccuracy, float[] agentsRadius)
        {
            if (polygonAccuracy % 2 != 0)
                throw new Exception("Polygon accuracy must be an odd number!");
            
            _World = new Aabb(lowerWorldBound, upperWorldBound);
        }

        #endregion Public Methods
        
        #region Private Variables

        private Aabb _World;

        #endregion Private Variables
    }
}