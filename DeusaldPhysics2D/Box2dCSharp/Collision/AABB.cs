using System;
using System.Diagnostics.Contracts;
using Vector2 = DeusaldSharp.Vector2;
using System.Runtime.CompilerServices;
using Box2DSharp.Collision.Collider;
using Box2DSharp.Common;

namespace Box2DSharp.Collision
{
    /// <summary>
    ///     An axis aligned bounding box.
    /// </summary>
    internal struct AABB
    {
        /// <summary>
        ///     the lower vertex
        /// </summary>
        public Vector2 LowerBound;

        /// <summary>
        ///     the upper vertex
        /// </summary>
        public Vector2 UpperBound;

        public AABB(in Vector2 lowerBound, in Vector2 upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        /// <summary>
        ///     Verify that the bounds are sorted.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Pure]
        public bool IsValid()
        {
            var d = UpperBound - LowerBound;
            var valid = d.x >= 0.0f && d.y >= 0.0f;
            valid = valid && LowerBound.IsValid() && UpperBound.IsValid();
            return valid;
        }

        /// <summary>
        ///     Get the center of the AABB.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Pure]
        public Vector2 GetCenter()
        {
            return 0.5f * (LowerBound + UpperBound);
        }

        /// <summary>
        ///     Get the extents of the AABB (half-widths).
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Pure]
        public Vector2 GetExtents()
        {
            return 0.5f * (UpperBound - LowerBound);
        }

        /// <summary>
        ///     Get the perimeter length
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Pure]
        public float GetPerimeter()
        {
            var wx = UpperBound.x - LowerBound.x;
            var wy = UpperBound.y - LowerBound.y;
            return wx + wx + wy + wy;
        }

        public bool RayCast(out RayCastOutput output, in RayCastInput input)
        {
            output = default;
            var tmin = -Settings.MaxFloat;
            var tmax = Settings.MaxFloat;

            var p = input.P1;
            var d = input.P2 - input.P1;
            var absD = Vector2.Abs(d);

            var normal = new Vector2();

            {
                if (absD.x < Settings.Epsilon)
                {
                    // Parallel.
                    if (p.x < LowerBound.x || UpperBound.x < p.x)
                    {
                        return false;
                    }
                }
                else
                {
                    var invD = 1.0f / d.x;
                    var t1 = (LowerBound.x - p.x) * invD;
                    var t2 = (UpperBound.x - p.x) * invD;

                    // Sign of the normal vector.
                    var s = -1.0f;

                    if (t1 > t2)
                    {
                        MathUtils.Swap(ref t1, ref t2);
                        s = 1.0f;
                    }

                    // Push the min up
                    if (t1 > tmin)
                    {
                        normal.SetZero();
                        normal.x = s;
                        tmin = t1;
                    }

                    // Pull the max down
                    tmax = Math.Min(tmax, t2);

                    if (tmin > tmax)
                    {
                        return false;
                    }
                }
            }
            {
                if (absD.y < Settings.Epsilon)
                {
                    // Parallel.
                    if (p.y < LowerBound.y || UpperBound.y < p.y)
                    {
                        return false;
                    }
                }
                else
                {
                    var invD = 1.0f / d.y;
                    var t1 = (LowerBound.y - p.y) * invD;
                    var t2 = (UpperBound.y - p.y) * invD;

                    // Sign of the normal vector.
                    var s = -1.0f;

                    if (t1 > t2)
                    {
                        MathUtils.Swap(ref t1, ref t2);
                        s = 1.0f;
                    }

                    // Push the min up
                    if (t1 > tmin)
                    {
                        normal.SetZero();
                        normal.y = s;
                        tmin = t1;
                    }

                    // Pull the max down
                    tmax = Math.Min(tmax, t2);

                    if (tmin > tmax)
                    {
                        return false;
                    }
                }
            }

            // Does the ray start inside the box?
            // Does the ray intersect beyond the max fraction?
            if (tmin < 0.0f || input.MaxFraction < tmin)
            {
                return false;
            }

            // Intersection.
            output = new RayCastOutput {Fraction = tmin, Normal = normal};

            return true;
        }

        public static void Combine(in AABB left, in AABB right, out AABB aabb)
        {
            aabb = new AABB(
                Vector2.Min(left.LowerBound, right.LowerBound),
                Vector2.Max(left.UpperBound, right.UpperBound));
        }

        /// <summary>
        ///     Combine an AABB into this one.
        /// </summary>
        /// <param name="aabb"></param>
        public void Combine(in AABB aabb)
        {
            LowerBound = Vector2.Min(LowerBound, aabb.LowerBound);
            UpperBound = Vector2.Max(UpperBound, aabb.UpperBound);
        }

        /// <summary>
        ///     Combine two AABBs into this one.
        /// </summary>
        /// <param name="aabb1"></param>
        /// <param name="aabb2"></param>
        public void Combine(in AABB aabb1, in AABB aabb2)
        {
            LowerBound = Vector2.Min(aabb1.LowerBound, aabb2.LowerBound);
            UpperBound = Vector2.Max(aabb1.UpperBound, aabb2.UpperBound);
        }

        /// <summary>
        ///     Does this aabb contain the provided AABB.
        /// </summary>
        /// <param name="aabb">the provided AABB</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Pure]
        public bool Contains(in AABB aabb)
        {
            return LowerBound.x <= aabb.LowerBound.x
                && LowerBound.y <= aabb.LowerBound.y
                && aabb.UpperBound.x <= UpperBound.x
                && aabb.UpperBound.y <= UpperBound.y;
        }
    }
}