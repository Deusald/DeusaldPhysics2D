using Vector2 = DeusaldSharp.Vector2;
using System.Runtime.CompilerServices;

namespace Box2DSharp.Common
{
    internal struct Matrix2x2
    {
        public Vector2 Ex;

        public Vector2 Ey;

        /// The default constructor does nothing (for performance).
        /// Construct this matrix using columns.
        public Matrix2x2(in Vector2 c1, in Vector2 c2)
        {
            Ex = c1;
            Ey = c2;
        }

        /// Construct this matrix using scalars.
        public Matrix2x2(float a11, float a12, float a21, float a22)
        {
            Ex.x = a11;
            Ex.y = a21;
            Ey.x = a12;
            Ey.y = a22;
        }

        /// Initialize this matrix using columns.
        public void Set(in Vector2 c1, in Vector2 c2)
        {
            Ex = c1;
            Ey = c2;
        }

        /// Set this to the identity matrix.
        public void SetIdentity()
        {
            Ex.x = 1.0f;
            Ey.x = 0.0f;
            Ex.y = 0.0f;
            Ey.y = 1.0f;
        }

        /// Set this matrix to all zeros.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetZero()
        {
            Ex.x = 0.0f;
            Ey.x = 0.0f;
            Ex.y = 0.0f;
            Ey.y = 0.0f;
        }

        public Matrix2x2 GetInverse()
        {
            var a = Ex.x;
            var b = Ey.x;
            var c = Ex.y;
            var d = Ey.y;

            var det = a * d - b * c;
            if (!det.Equals(0.0f))
            {
                det = 1.0f / det;
            }

            var B = new Matrix2x2();
            B.Ex.x = det * d;
            B.Ey.x = -det * b;
            B.Ex.y = -det * c;
            B.Ey.y = det * a;
            return B;
        }

        /// Solve A * x = b, where b is a column vector. This is more efficient
        /// than computing the inverse in one-shot cases.
        public Vector2 Solve(in Vector2 b)
        {
            var a11 = Ex.x;
            var a12 = Ey.x;
            var a21 = Ex.y;
            var a22 = Ey.y;
            var det = a11 * a22 - a12 * a21;
            if (!det.Equals(0.0f))
            {
                det = 1.0f / det;
            }

            var x = new Vector2 {x = det * (a22 * b.x - a12 * b.y), y = det * (a11 * b.y - a21 * b.x)};
            return x;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix2x2 operator +(in Matrix2x2 A, in Matrix2x2 B)
        {
            return new Matrix2x2(A.Ex + B.Ex, A.Ey + B.Ey);
        }
    }
}