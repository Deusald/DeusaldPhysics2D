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

// ReSharper disable InconsistentNaming
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace SharpBox2D
{
    using System;
    using System.Text;
    using Box2D;

    public struct Vector2 : IEquatable<Vector2>
    {
        #region Public Fields

        public float x;
        public float y;

        #endregion Public Fields

        #region Properties

        /// <summary>
        /// Vector (0, 0)
        /// </summary>
        public static Vector2 zero { get; } = new Vector2(0f, 0f);

        /// <summary>
        /// Vector (1, 1)
        /// </summary>
        public static Vector2 one { get; } = new Vector2(1f, 1f);

        /// <summary>
        /// Vector (0, 1)
        /// </summary>
        public static Vector2 up { get; } = new Vector2(0f, 1f);

        /// <summary>
        /// Vector (0, -1)
        /// </summary>
        public static Vector2 down { get; } = new Vector2(0f, -1f);

        /// <summary>
        /// Vector (-1, 0)
        /// </summary>
        public static Vector2 left { get; } = new Vector2(-1f, 0f);

        /// <summary>
        /// Vector (1, 0)
        /// </summary>
        public static Vector2 right { get; } = new Vector2(1f, 0f);

        public float magnitude    => Length();
        public float sqrMagnitude => LengthSquared();

        public Vector2 normalized
        {
            get
            {
                Vector2 normal = new Vector2(x, y);
                normal.Normalize();
                return normal;
            }
        }

        public Vector2 negated
        {
            get
            {
                Vector2 negate = new Vector2(x, y);
                negate.Negate();
                return negate;
            }
        }

        public bool isValid => float.IsFinite(x) && float.IsFinite(y);

        // Get the skew vector such that dot(skew_vec, other) == cross(vec, other)
        // https://brilliant.org/wiki/3d-coordinate-geometry-skew-lines/
        public Vector2 skew => new Vector2(-y, x);

        #endregion Properties

        #region Constructors

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(float value)
        {
            x = value;
            y = value;
        }

        public Vector2(Vector2 value)
        {
            x = value.x;
            y = value.y;
        }

        public Vector2(Vector3 value)
        {
            x = value.x;
            y = value.y;
        }

        #endregion Constructors

        #region Public Methods

        #region Setters

        public void Set(float newX, float newY)
        {
            x = newX;
            y = newY;
        }

        public void SetZero()
        {
            x = 0f;
            y = 0f;
        }

        #endregion Setters

        #region Basic Math

        public static Vector2 Add(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 Subtract(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 Multiply(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 Multiply(Vector2 a, float s)
        {
            return new Vector2(a.x * s, a.y * s);
        }

        public static Vector2 Divide(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public static Vector2 Divide(Vector2 a, float s)
        {
            float factor = 1f / s;
            return new Vector2(a.x * factor, a.y * factor);
        }

        public static Vector2 GetNegated(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        public void Negate()
        {
            x = -x;
            y = -y;
        }

        #endregion Basic Math

        #region Vector Math

        public float Length()
        {
            return MathF.Sqrt(DistanceSquared(this, zero));
        }

        public float LengthSquared()
        {
            return DistanceSquared(this, zero);
        }

        public void Normalize()
        {
            float length = Length();

            if (MathUtils.IsFloatZero(length)) return;

            float invLength = 1f / length;
            x *= invLength;
            y *= invLength;
        }

        public static Vector2 GetNormalized(Vector2 a)
        {
            Vector2 normalized = new Vector2(a.x, a.y);
            normalized.Normalize();
            return normalized;
        }

        public static float Cross(Vector2 a, Vector2 b)
        {
            return a.x * b.y - a.y * b.x;
        }

        public float Cross(Vector2 b)
        {
            return Cross(this, b);
        }

        public static Vector2 Cross(Vector2 a, float s)
        {
            return new Vector2(s * a.y, -s * a.x);
        }

        public static Vector2 Cross(float s, Vector2 a)
        {
            return new Vector2(-s * a.y, s * a.x);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return MathF.Sqrt(DistanceSquared(a, b));
        }

        public float Distance(Vector2 b)
        {
            return Distance(this, b);
        }

        public static float DistanceSquared(Vector2 a, Vector2 b)
        {
            Vector2 c = a - b;
            return Dot(c, c);
        }

        public float DistanceSquared(Vector2 b)
        {
            return DistanceSquared(this, b);
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public float Dot(Vector2 b)
        {
            return Dot(this, b);
        }

        public static Vector2 Reflect(Vector2 inDir, Vector2 normal)
        {
            float dot = Dot(inDir, normal);
            Vector2 result = new Vector2
            {
                x = inDir.x - ((2f * dot) * normal.x),
                y = inDir.y - ((2f * dot) * normal.y)
            };

            return result;
        }

        public Vector3 Reflect(Vector2 normal)
        {
            return Reflect(this, normal);
        }

        #endregion Vector Math

        #region Other Math

        public static Vector2 Clamp(Vector2 a, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Math.Clamp(a.x, min.x, max.x),
                Math.Clamp(a.y, min.y, max.y));
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(
                MathUtils.Lerp(a.x, b.x, t),
                MathUtils.Lerp(a.y, b.y, t));
        }

        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            return new Vector2(
                MathF.Max(a.x, b.x),
                MathF.Max(a.y, b.y));
        }

        public static Vector2 Min(Vector2 a, Vector2 b)
        {
            return new Vector2(
                MathF.Min(a.x, b.x),
                MathF.Min(a.y, b.y));
        }

        public static Vector2 Abs(Vector2 a)
        {
            return new Vector2(MathF.Abs(a.x), MathF.Abs(a.y));
        }

        public void RoundToDecimal(int decimalPoint)
        {
            float decimalPow = MathF.Pow(10f, decimalPoint);
            this =  this * decimalPow;
            x    =  MathF.Round(x);
            y    =  MathF.Round(y);
            this /= decimalPow;
        }

        /// <summary>
        /// Rotate a vector (angle in radians)
        /// </summary>
        public static Vector2 Rotate(float angle, Vector2 v)
        {
            float sin = MathF.Sin(angle);
            float cos = MathF.Cos(angle);
            return new Vector2(cos * v.x - sin * v.y, sin * v.x + cos * v.y);
        }

        /// <summary>
        /// Inverse rotate a vector (angle in radians)
        /// </summary>
        public static Vector2 RotateInverse(float angle, Vector2 v)
        {
            float sin = MathF.Sin(angle);
            float cos = MathF.Cos(angle);
            return new Vector2(cos * v.x + sin * v.y, -sin * v.x + cos * v.y);
        }

        /// <summary>
        /// Reduce length of vector by reductionLength value
        /// </summary>
        public void ShortenLength(float reductionLength)
        {
            if (magnitude <= reductionLength)
            {
                SetZero();
                return;
            }

            Vector2 shorter = normalized * reductionLength;
            this -= shorter;
        }

        #endregion Other Math

        #region Equals

        public bool Equals(Vector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        #endregion Equals

        #region Operators

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return MathUtils.AreFloatsEquals(a.x, b.x) && MathUtils.AreFloatsEquals(a.y, b.y);
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return !(a == b);
        }

        public static Vector2 operator -(Vector2 a)
        {
            return a.negated;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return Add(a, b);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return Subtract(a, b);
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return Multiply(a, b);
        }

        public static Vector2 operator *(Vector2 a, float s)
        {
            return Multiply(a, s);
        }

        public static Vector2 operator *(float s, Vector2 a)
        {
            return Multiply(a, s);
        }

        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return Divide(a, b);
        }

        public static Vector2 operator /(Vector2 a, float s)
        {
            return Divide(a, s);
        }

        public static implicit operator Vector2(Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        public float this[int key]
        {
            get
            {
                switch (key)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }

            set
            {
                switch (key)
                {
                    case 0:
                    {
                        x = value;
                        break;
                    }
                    case 1:
                    {
                        y = value;
                        break;
                    }
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion Operators

        internal static Vector2 ConvertFromB2Vec(b2Vec2 vec2)
        {
            return new Vector2(vec2.x, vec2.y);
        }

        internal static b2Vec2 ConvertToB2Vec(Vector2 vector2)
        {
            return new b2Vec2(vector2.x, vector2.y);
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(24);
            sb.Append("{X:");
            sb.Append(x);
            sb.Append(" Y:");
            sb.Append(y);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion Public Methods
    }
}