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

    public struct Vector3 : IEquatable<Vector3>
    {
        #region Public Fields

        public float x;
        public float y;
        public float z;

        #endregion Public Fields

        #region Properties

        /// <summary>
        /// Vector (0, 0, 0)
        /// </summary>
        public static Vector3 zero { get; } = new Vector3(0f, 0f, 0f);

        /// <summary>
        /// Vector (1, 1, 1)
        /// </summary>
        public static Vector3 one { get; } = new Vector3(1f, 1f, 1f);

        /// <summary>
        /// Vector (1, 0, 0)
        /// </summary>
        public static Vector3 unitX { get; } = new Vector3(1f, 0f, 0f);

        /// <summary>
        /// Vector (0, 1, 0)
        /// </summary>
        public static Vector3 unitY { get; } = new Vector3(0f, 1f, 0f);

        /// <summary>
        /// Vector (0, 0, 1)
        /// </summary>
        public static Vector3 unitZ { get; } = new Vector3(0f, 0f, 1f);

        /// <summary>
        /// Vector (0, 1, 0)
        /// </summary>
        public static Vector3 up { get; } = new Vector3(0f, 1f, 0f);

        /// <summary>
        /// Vector (0, -1, 0)
        /// </summary>
        public static Vector3 down { get; } = new Vector3(0f, -1f, 0f);

        /// <summary>
        /// Vector (1, 0, 0)
        /// </summary>
        public static Vector3 right { get; } = new Vector3(1f, 0f, 0f);

        /// <summary>
        /// Vector (-1, 0, 0)
        /// </summary>
        public static Vector3 left { get; } = new Vector3(-1f, 0f, 0f);

        /// <summary>
        /// Vector (0, 0, -1)
        /// </summary>
        public static Vector3 forward { get; } = new Vector3(0f, 0f, -1f);

        /// <summary>
        /// Vector (0, 0, 1)
        /// </summary>
        public static Vector3 backward { get; } = new Vector3(0f, 0f, 1f);

        public float magnitude    => Length();
        public float sqrMagnitude => LengthSquared();

        public Vector3 normalized
        {
            get
            {
                Vector3 normal = new Vector3(x, y, z);
                normal.Normalize();
                return normal;
            }
        }

        public Vector3 negated
        {
            get
            {
                Vector3 negate = new Vector3(x, y, z);
                negate.Negate();
                return negate;
            }
        }

        public bool isValid => !float.IsInfinity(x) && !float.IsInfinity(y) && !float.IsInfinity(z);

        #endregion Properties

        #region Constructors

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(float value)
        {
            x = value;
            y = value;
            z = value;
        }

        public Vector3(Vector2 value, float z)
        {
            x      = value.x;
            y      = value.y;
            this.z = z;
        }

        public Vector3(Vector2 value)
        {
            x = value.x;
            y = value.y;
            z = 0f;
        }

        public Vector3(Vector3 value)
        {
            x = value.x;
            y = value.y;
            z = value.z;
        }

        #endregion Constructors

        #region Public Methods

        #region Setters

        public void Set(float newX, float newY, float newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }

        public void SetZero()
        {
            x = 0f;
            y = 0f;
            z = 0f;
        }

        #endregion Setters

        #region Basic Math

        public static Vector3 Add(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 Subtract(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 Multiply(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 Multiply(Vector3 a, float s)
        {
            return new Vector3(a.x * s, a.y * s, a.z * s);
        }

        public static Vector3 Divide(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector3 Divide(Vector3 a, float s)
        {
            float factor = 1f / s;
            return new Vector3(a.x * factor, a.y * factor, a.z * factor);
        }

        public static Vector3 GetNegated(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        public void Negate()
        {
            x = -x;
            y = -y;
            z = -z;
        }

        #endregion Basic Math

        #region Vector Math

        public float Length()
        {
            return MathUtils.Sqrt(DistanceSquared(this, zero));
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
            z *= invLength;
        }

        public static Vector3 GetNormalized(Vector3 a)
        {
            Vector3 normalized = new Vector3(a);
            normalized.Normalize();
            return normalized;
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            float x = a.y * b.z - a.z * b.y;
            float y = a.z * b.x - a.x * b.z;
            float z = a.x * b.y - a.y * b.x;
            
            return new Vector3(x, y, z);
        }

        public Vector3 Cross(Vector3 b)
        {
            return Cross(this, b);
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            return MathUtils.Sqrt(DistanceSquared(a, b));
        }

        public float Distance(Vector3 vector1)
        {
            return Distance(this, vector1);
        }

        public static float DistanceSquared(Vector3 a, Vector3 b)
        {
            Vector3 c = a - b;
            return Dot(c, c);
        }

        public float DistanceSquared(Vector3 b)
        {
            return DistanceSquared(this, b);
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public float Dot(Vector3 b)
        {
            return Dot(this, b);
        }

        public static Vector3 Reflect(Vector3 inDir, Vector3 normal)
        {
            float dot = Dot(inDir, normal);
            Vector3 result = new Vector3
            {
                x = inDir.x - ((2f * dot) * normal.x),
                y = inDir.y - ((2f * dot) * normal.y),
                z = inDir.z - ((2f * dot) * normal.z)
            };

            return result;
        }

        public Vector3 Reflect(Vector3 normal)
        {
            return Reflect(this, normal);
        }

        #endregion Vector Math

        #region Other Math

        public static Vector3 Clamp(Vector3 a, Vector3 min, Vector3 max)
        {
            return new Vector3(
                MathUtils.Clamp(a.x, min.x, max.x),
                MathUtils.Clamp(a.y, min.y, max.y),
                MathUtils.Clamp(a.z, min.z, max.z));
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(
                MathUtils.Lerp(a.x, b.x, t),
                MathUtils.Lerp(a.y, b.y, t),
                MathUtils.Lerp(a.z, b.z, t));
        }

        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(
                MathUtils.Max(a.x, b.x),
                MathUtils.Max(a.y, b.y),
                MathUtils.Max(a.z, b.z));
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(
                MathUtils.Min(a.x, b.x),
                MathUtils.Min(a.y, b.y),
                MathUtils.Min(a.z, b.z));
        }

        #endregion Other Math

        #region Equals

        public bool Equals(Vector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(x, y, z).GetHashCode();
        }

        #endregion Equals

        #region Operators

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return MathUtils.AreFloatsEquals(a.x, b.x)
                   && MathUtils.AreFloatsEquals(a.y, b.y)
                   && MathUtils.AreFloatsEquals(a.z, b.z);
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        public static Vector3 operator -(Vector3 a)
        {
            return a.negated;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return Add(a, b);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return Subtract(a, b);
        }

        public static Vector3 operator *(Vector3 a, Vector3 b)
        {
            return Multiply(a, b);
        }

        public static Vector3 operator *(Vector3 a, float s)
        {
            return Multiply(a, s);
        }

        public static Vector3 operator *(float s, Vector3 a)
        {
            return Multiply(a, s);
        }

        public static Vector3 operator /(Vector3 a, Vector3 b)
        {
            return Divide(a, b);
        }

        public static Vector3 operator /(Vector3 a, float s)
        {
            return Divide(a, s);
        }

        public static implicit operator Vector3(Vector2 vector2)
        {
            return new Vector3(vector2.x, vector2.y, 0f);
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
                    case 2:
                        return z;
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
                    case 2:
                    {
                        z = value;
                        break;
                    }
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion Operators

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("{X:");
            sb.Append(x);
            sb.Append(" Y:");
            sb.Append(y);
            sb.Append(" Z:");
            sb.Append(z);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion Public methods
    }
}