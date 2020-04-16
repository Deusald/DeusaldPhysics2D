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

    public static class MathUtils
    {
        #region Time

        public static float MinutesToSeconds(float minutes)
        {
            return minutes * 60f;
        }

        #endregion Time

        #region Float Utils

        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
        
        public static float InverseLerp(float a, float b, float value)
        {
            if (Math.Abs(b - a) < float.Epsilon)
                return a;

            return (value - a) / (b - a);
        }

        public static float RoundToDecimal(float value, int decimalPoint)
        {
            float decimalPow = MathF.Pow(10f, decimalPoint);
            value =  value * decimalPow;
            value =  MathF.Round(value);
            value /= decimalPow;
            return value;
        }

        public static bool IsFloatZero(float value)
        {
            return AreFloatsEquals(value, 0f);
        }

        public static bool AreFloatsEquals(float one, float two)
        {
            return Math.Abs(one - two) < float.Epsilon;
        }

        #endregion Float Utils

        #region Consts

        public const float Pi       = (float) Math.PI;
        public const float DegToRad = MathF.PI / 180f;
        public const float RadToDeg = 180f / MathF.PI;

        public const float EpsilonSquare = float.Epsilon * float.Epsilon;

        #endregion Consts
        
        #region Bits

        public static uint NumberOfSetBits(uint mask)
        {
            uint count = 0;

            while (mask > 0)
            {
                count +=  mask & 1;
                mask  >>= 1;
            }

            return count;
        }

        #endregion Bits
    }
}