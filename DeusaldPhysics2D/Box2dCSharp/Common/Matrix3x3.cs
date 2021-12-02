using Vector2 = DeusaldSharp.Vector2;
using Vector3 = DeusaldSharp.Vector3;

namespace Box2DSharp.Common
{
    internal struct Matrix3x3
    {
        public Vector3 Ex;

        public Vector3 Ey;

        public Vector3 Ez;

        /// Construct this matrix using columns.
        public Matrix3x3(in Vector3 c1, in Vector3 c2, in Vector3 c3)
        {
            Ex = c1;
            Ey = c2;
            Ez = c3;
        }

        /// Set this matrix to all zeros.
        public void SetZero()
        {
            Ex.SetZero();
            Ey.SetZero();
            Ez.SetZero();
        }

        /// Solve A * x = b, where b is a column vector. This is more efficient
        /// than computing the inverse in one-shot cases.
        public Vector3 Solve33(in Vector3 b)
        {
            var det = Vector3.Dot(Ex, Vector3.Cross(Ey, Ez));
            if (!det.Equals(0.0f))
            {
                det = 1.0f / det;
            }

            Vector3 x;
            x.x = det * Vector3.Dot(b, Vector3.Cross(Ey, Ez));
            x.y = det * Vector3.Dot(Ex, Vector3.Cross(b, Ez));
            x.z = det * Vector3.Dot(Ex, Vector3.Cross(Ey, b));
            return x;
        }

        /// Solve A * x = b, where b is a column vector. This is more efficient
        /// than computing the inverse in one-shot cases. Solve only the upper
        /// 2-by-2 matrix equation.
        public Vector2 Solve22(in Vector2 b)
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

            Vector2 x;
            x.x = det * (a22 * b.x - a12 * b.y);
            x.y = det * (a11 * b.y - a21 * b.x);
            return x;
        }

        /// Get the inverse of this matrix as a 2-by-2.
        /// Returns the zero matrix if singular.
        public void GetInverse22(ref Matrix3x3 matrix3x3)
        {
            float a = Ex.x, b = Ey.x, c = Ex.y, d = Ey.y;
            var det = a * d - b * c;
            if (!det.Equals(0.0f))
            {
                det = 1.0f / det;
            }

            matrix3x3.Ex.x = det * d;
            matrix3x3.Ey.x = -det * b;
            matrix3x3.Ex.z = 0.0f;
            matrix3x3.Ex.y = -det * c;
            matrix3x3.Ey.y = det * a;
            matrix3x3.Ey.z = 0.0f;
            matrix3x3.Ez.x = 0.0f;
            matrix3x3.Ez.y = 0.0f;
            matrix3x3.Ez.z = 0.0f;
        }

        /// Get the symmetric inverse of this matrix as a 3-by-3.
        /// Returns the zero matrix if singular.
        public void GetSymInverse33(ref Matrix3x3 matrix3x3)
        {
            var det = Vector3.Dot(Ex, Vector3.Cross(Ey, Ez));
            if (!det.Equals(0.0f))
            {
                det = 1.0f / det;
            }

            float a11 = Ex.x, a12 = Ey.x, a13 = Ez.x;
            float a22 = Ey.y, a23 = Ez.y;
            var a33 = Ez.z;

            matrix3x3.Ex.x = det * (a22 * a33 - a23 * a23);
            matrix3x3.Ex.y = det * (a13 * a23 - a12 * a33);
            matrix3x3.Ex.z = det * (a12 * a23 - a13 * a22);

            matrix3x3.Ey.x = matrix3x3.Ex.y;
            matrix3x3.Ey.y = det * (a11 * a33 - a13 * a13);
            matrix3x3.Ey.z = det * (a13 * a12 - a11 * a23);

            matrix3x3.Ez.x = matrix3x3.Ex.z;
            matrix3x3.Ez.y = matrix3x3.Ey.z;
            matrix3x3.Ez.z = det * (a11 * a22 - a12 * a12);
        }
    }
}