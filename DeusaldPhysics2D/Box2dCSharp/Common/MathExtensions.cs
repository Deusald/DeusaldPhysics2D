using Vector2 = DeusaldSharp.Vector2;
using Vector3 = DeusaldSharp.Vector3;
using System.Runtime.CompilerServices;

namespace Box2DSharp.Common
{
    internal static class MathExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsValid(in this Vector2 vector2)
        {
            return !float.IsInfinity(vector2.x) && !float.IsInfinity(vector2.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsValid(this float x)
        {
            return !float.IsInfinity(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetZero(ref this Vector2 vector2)
        {
            vector2.x = 0.0f;
            vector2.y = 0.0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Set(ref this Vector2 vector2, float x, float y)
        {
            vector2.x = x;
            vector2.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void SetZero(ref this Vector3 vector3)
        {
            vector3.x = 0.0f;
            vector3.y = 0.0f;
            vector3.z = 0.0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Set(ref this Vector3 vector3, float x, float y, float z)
        {
            vector3.x = x;
            vector3.y = y;
            vector3.z = z;
        }

        /// Convert this vector into a unit vector. Returns the length.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float Normalize(ref this Vector2 vector2)
        {
            var length = vector2.Length();
            if (length < Settings.Epsilon)
            {
                return 0.0f;
            }

            var invLength = 1.0f / length;
            vector2.x *= invLength;
            vector2.y *= invLength;

            return length;
        }

        /// <summary>
        ///  Get the skew vector such that dot(skew_vec, other) == cross(vec, other)
        /// </summary>
        /// <param name="vector2"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector2 Skew(ref this Vector2 vector2)
        {
            return new Vector2(-vector2.y, vector2.x);
        }
    }
}