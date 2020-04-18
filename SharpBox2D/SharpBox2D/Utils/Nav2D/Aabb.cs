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
    internal class Aabb
    {
        #region Public Variables

        public Vector2 LowerBound;
        public Vector2 UpperBound;
        public Vector2 Center;

        #endregion Public Variables

        #region Public Methods

        public Aabb(Vector2 lowerBound, Vector2 upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            RecalculateCenter();
        }
        
        public void RecalculateCenter()
        {
            Center.x = (LowerBound.x + UpperBound.x) / 2f;
            Center.y = (LowerBound.y + UpperBound.y) / 2f;
        }

        #endregion Public Methods
    }
}